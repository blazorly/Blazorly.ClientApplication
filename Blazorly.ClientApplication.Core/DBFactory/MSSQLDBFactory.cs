using Blazorly.ClientApplication.Core.DB;
using Blazorly.ClientApplication.Core.Properties;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorly.ClientApplication.Core.DBFactory
{
    public class MSSQLDBFactory : BaseDBFactory
    {
        public MSSQLDBFactory(string connectionString, int timeout = 30, Schema schema = null) : base("MSSQL", connectionString, timeout, schema)
        {
        }

        public override Schema GetSchema()
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
                var constraint = constraints.Where(x => x.TableSchema.ToUpperInvariant() == c.TableSchema.ToUpperInvariant()
                                    && x.TableName.ToUpperInvariant() == c.TableName.ToUpperInvariant() && x.ColumnName.ToUpperInvariant() == c.Name.ToUpperInvariant())
                                    .FirstOrDefault();
                if (constraint == null) continue;

                c.IsPrimaryKey = constraint.ConstraintType == "PRIMARY KEY";
                c.IsForeignKey = constraint.ConstraintType == "FOREIGN KEY";
                if (c.IsForeignKey)
                {
                    c.Reference = foreignRefs.Where(x => x.ConstraintName == constraint.ConstraintName).FirstOrDefault();
                }
            }

            foreach (var t in schema.Collections)
            {
                t.Fields = columns.Where(x => x.TableName.ToUpperInvariant() == t.Name.ToUpperInvariant() && x.TableSchema.ToUpperInvariant() == t.Schema.ToUpperInvariant()).ToList();
            }

            return schema;
        }
    }
}
