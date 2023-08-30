using Blazorly.ClientApplication.Core.DB;
using Blazorly.ClientApplication.Core.Dto;
using Blazorly.ClientApplication.Core.Exceptions;
using Blazorly.ClientApplication.Core.Properties;
using Dapper;
using Microsoft.Data.Sqlite;
using MySql.Data.MySqlClient;
using Npgsql;
using SqlKata;
using SqlKata.Compilers;
using SqlKata.Execution;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Dynamic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Blazorly.ClientApplication.Core
{
    public class DBFactory
    {
        private QueryFactory factory = null;

        private Schema schema = null;

        public DBFactory(string dbType, string connectionString, int timeout = 30, Schema schema = null) 
        {
            BuildFactory(dbType, connectionString, timeout);
            this.schema = schema;
        }

        private void BuildFactory(string dbType, string connectionString, int timeout)
        {
            switch (dbType)
            {
                case "MSSQL":
                    var mssqlconn = new SqlConnection(connectionString);
                    factory = new QueryFactory(mssqlconn, new SqlServerCompiler(), timeout);
                    break;
                case "MYSQL":
                    var mysqlconn = new MySqlConnection(connectionString);
                    factory = new QueryFactory(mysqlconn, new MySqlCompiler(), timeout);
                    break;
                case "POSTGRES":
                    var postgresconn = new NpgsqlConnection(connectionString);
                    factory = new QueryFactory(postgresconn, new PostgresCompiler(), timeout);
                    break;
                case "SQLITE":
                    var sqliteconn = new SqliteConnection(connectionString);
                    factory = new QueryFactory(sqliteconn, new SqliteCompiler(), timeout);
                    break;
                default:
                    throw new NotSupportedException("Unsupported database");
            }
        }

        public async Task Insert(string collection, ExpandoObject data)
        {
            await factory.Query(collection).InsertAsync(data);
        }

        public async Task Update(string collection, string key, object value, ExpandoObject data)
        {
            var count = await factory.Query(collection).Where(key, value).UpdateAsync(data);
            if (count == 0)
                throw new Exception("Update failed");
        }

        public async Task<dynamic> Read(string collection, string key, object value)
        {
            var query = factory.Query(collection).Where(key, value);
            
            return await query.FirstOrDefaultAsync();
        }

        public async Task<PaginationResult<dynamic>> Query(string collection, ItemsQueryRequest queryRequest, int? page, int? count)
        {
            if (page == null)
                page = 1;
            if (count == null)
                count = 100;

            var query = factory.Query();
            DBUtils.ParseSelectFields(schema, collection, queryRequest, query);
            if(queryRequest.Filter != null)
                DBUtils.ParseQuery("t0", queryRequest.Filter.RootElement, query);
            DBUtils.ParseSort(queryRequest.Sort, query);

            return await query.PaginateAsync(page.Value, count.Value);
        }

        public async Task CheckRecordAccess(string collection, string key, object value)
        {
            var count = await factory.Query(collection).Where(key, value).CountAsync<int>(new string[] { "*" });

            if (count == 0)
                throw new RecordNotFoundException(collection);
        }

        public Schema GetSchema()
        {
            schema = new Schema();

            schema.Collections = factory.Connection.Query<TableSchema>("SELECT TABLE_SCHEMA AS [Schema], TABLE_NAME AS [Name] FROM INFORMATION_SCHEMA.TABLES").ToList();
            var columns = factory.Connection.Query<TableColumn>("SELECT TABLE_SCHEMA AS [TableSchema], TABLE_NAME AS [TableName], COLUMN_NAME AS [Name], ORDINAL_POSITION AS Position, " +
                                                                "IS_NULLABLE AS IsNullable, DATA_TYPE AS DataType, CHARACTER_MAXIMUM_LENGTH AS [MaxLength], COLUMN_DEFAULT AS [Default] " +
                                                                "FROM INFORMATION_SCHEMA.COLUMNS").ToList();

            var constraints = factory.Connection.Query<ColumnConstraint>("SELECT CCU.CONSTRAINT_NAME ConstraintName, CCU.TABLE_SCHEMA TableSchema, CCU.TABLE_NAME TableName, " +
                                                                        "CCU.COLUMN_NAME ColumnName, TC.CONSTRAINT_TYPE ConstraintType  " +
                                                                        "FROM INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE CCU " +
                                                                        "INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS TC ON TC.CONSTRAINT_NAME = CCU.CONSTRAINT_NAME");

            var foreignRefs = factory.Connection.Query<ColumnConstraint>(Resources.MSSQL_ForeignKeys);

            foreach (var c in columns)
            {
                var constraint = constraints.Where(x=>x.TableSchema.ToUpperInvariant() ==  c.TableSchema.ToUpperInvariant() 
                                    && x.TableName.ToUpperInvariant() == c.TableName.ToUpperInvariant() && x.ColumnName.ToUpperInvariant() == c.Name.ToUpperInvariant())
                                    .FirstOrDefault();
                if(constraint == null) continue;

                c.IsPrimaryKey = constraint.ConstraintType == "PRIMARY KEY";
                c.IsForeignKey = constraint.ConstraintType == "FOREIGN KEY";
                if (c.IsForeignKey)
                {
                    c.Reference = foreignRefs.Where(x=>x.ConstraintName == constraint.ConstraintName).FirstOrDefault();
                }
            }

            foreach (var t in schema.Collections)
            {
                t.Fields = columns.Where(x=>x.TableName.ToUpperInvariant() == t.Name.ToUpperInvariant() && x.TableSchema.ToUpperInvariant() == t.Schema.ToUpperInvariant()).ToList();
            }

            return schema;
        }
    }
}
