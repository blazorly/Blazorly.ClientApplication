using Blazorly.ClientApplication.Core.Exceptions;
using Blazorly.ClientApplication.Core.Properties;
using Blazorly.ClientApplication.SDK;
using Blazorly.ClientApplication.SDK.Dto;
using Dapper;
using Microsoft.Data.Sqlite;
using MySql.Data.MySqlClient;
using Npgsql;
using SqlKata.Compilers;
using SqlKata.Execution;
using System.Data.SqlClient;
using System.Dynamic;

namespace Blazorly.ClientApplication.Core.DBFactory
{
    public abstract class BaseDBFactory
    {
        public QueryFactory factory = null;

        internal Schema schema = null;

        public BaseDBFactory(string dbType, string connectionString, int timeout = 30, Schema schema = null)
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

        public async Task Delete(string collection, string key, object value)
        {
            var count = await factory.Query(collection).Where(key, value).DeleteAsync();
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
            if (queryRequest.Filter != null)
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

        public abstract Schema GetSchema();
    }
}
