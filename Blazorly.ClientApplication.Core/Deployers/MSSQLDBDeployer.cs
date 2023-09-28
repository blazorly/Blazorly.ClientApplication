using Blazorly.ClientApplication.Core.DB;
using Blazorly.ClientApplication.Core.DBFactory;
using Blazorly.ClientApplication.SDK;
using Blazorly.ClientApplication.SDK.Attributes;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Blazorly.ClientApplication.Core.Deployers
{
    public class MSSQLDBDeployer : BaseDBDeployer
	{
        public MSSQLDBDeployer(string connString) : base(new MSSQLDBFactory(connString, 300))
        {

        }

        internal override string CreateTableScript(Type entity)
        {
            StringBuilder script = new StringBuilder();
            var properties = entity.GetProperties();
            script.AppendLine($"CREATE TABLE \"{entity.Name}\"(");
            script.AppendLine("\"Id\" VARCHAR(32) PRIMARY KEY");
            script.AppendLine(",\"CreatedDate\" datetime NOT NULL");
            script.AppendLine(",\"UpdatedDate\" datetime NOT NULL");
            script.AppendLine(",\"CreatedBy\" VARCHAR(32) NOT NULL");
            script.AppendLine(",\"UpdatedBy\" VARCHAR(32) NOT NULL");

            foreach (var prop in properties)
            {
                if (ignoreFields.Contains(prop.Name))
                    continue;
                var fieldRef = prop.GetCustomAttribute<FieldDefAttribute>();
                if (fieldRef == null)
                    continue;

                if (prop.PropertyType.BaseType.Name != "BaseEntity")
                {
                    if (prop.PropertyType.BaseType.Name != "Enum")
                        script.AppendLine($",\"{prop.Name}\" {GetDType(prop.PropertyType.Name, fieldRef.Length, fieldRef.Digits, fieldRef.Decimals)}");
                    else
                        script.AppendLine($",\"{prop.Name}\" {GetDType("enum", fieldRef.Length, fieldRef.Digits, fieldRef.Decimals)}");
                }
                else
                {
                    script.AppendLine($",\"{prop.Name}\" VARCHAR(32)");
                }
            }

            script.AppendLine(")");

            return script.ToString();
        }

		internal override string AlterTableScript(Type entity)
        {
            StringBuilder script = new StringBuilder();
            var entityDef = entity.GetCustomAttribute<EntityDefAttribute>();
            var tableSchema = GetTableSchema(entityDef.Name);
            int alterCount = 0;

            var properties = entity.GetProperties();
            script.AppendLine($"ALTER TABLE \"{entity.Name}\"");
            foreach (var col in tableSchema.Fields)
            {
                var prop = properties.Where(x => x.Name.ToUpper() == col.Name.ToUpper()).FirstOrDefault();
                if (ignoreFields.Contains(prop.Name))
                    continue;
                if (prop != null)
                {
                    var fieldRef = prop.GetCustomAttribute<FieldDefAttribute>();
                    if (fieldRef != null && fieldRef.Length != col.MaxLength && prop.PropertyType.BaseType.Name == "Object")
                    {
                        script.AppendLine($"ALTER COLUMN \"{prop.Name}\" TYPE {GetDType(prop.PropertyType.Name, fieldRef.Length, fieldRef.Digits, fieldRef.Decimals)},");
                        alterCount++;
                    }
                }
            }

            foreach (var prop in properties)
            {
                if (ignoreFields.Contains(prop.Name))
                    continue;
                var col = tableSchema.Fields.Where(x => x.Name.ToUpper() == prop.Name.ToUpper()).FirstOrDefault();
                if (col == null)
                {
                    var fieldRef = prop.GetCustomAttribute<FieldDefAttribute>();
                    if (fieldRef == null)
                        continue;

                    if (prop.PropertyType.BaseType.Name != "BaseEntity")
                    {
                        script.AppendLine($"ADD \"{prop.Name}\" {GetDType(prop.PropertyType.Name, fieldRef.Length, fieldRef.Digits, fieldRef.Decimals)},");
                        alterCount++;
                    }
                    else
                    {
                        script.AppendLine($"ADD \"{prop.Name}\" VARCHAR(32),");
                        alterCount++;
                    }
                }
            }

            if (alterCount == 0)
                return "";
            var s = script.ToString().Remove(script.ToString().LastIndexOf(',')) + ";";
            return s;
        }

		internal override string GetDType(string ftype, int length, int digits, int decimals)
        {
            string dtype = "";
            string lenText;
            if (length == -1)
                lenText = "MAX";
            else
                lenText = length > 0 ? length.ToString() : "255";
            switch (ftype.ToLower())
            {
                case "string":
                    dtype = $"VARCHAR({lenText})";
                    break;
                case "enum":
                    dtype = "SMALLINT";
                    break;
                case "int32":
                    dtype = "INT";
                    break;
                case "int64":
                    dtype = "BIGINT";
                    break;
                case "double":
                case "decimal":
                case "float":
                    dtype = $"DECIMAL({digits}, {decimals})";
                    break;
                case "datetime":
                    dtype = "DATETIME";
                    break;
                case "boolean":
                    dtype = "BIT";
                    break;
                default:
                    break;
            }

            return dtype;
        }

		internal override void ProcessForeignKeys(Type entity)
        {
            var entityDef = entity.GetCustomAttribute<EntityDefAttribute>();
            var properties = entity.GetProperties();
            foreach (var prop in properties)
            {
                if (ignoreFields.Contains(prop.Name))
                    continue;
                var fieldRef = prop.GetCustomAttribute<FieldDefAttribute>();

                if (fieldRef == null)
                    continue;

                if (prop.PropertyType.BaseType.Name == "BaseEntity")
                {

                    string fnName = $"FK_{entityDef.Name}_{prop.Name}";
                    var entityRefDef = prop.PropertyType.GetCustomAttribute<EntityDefAttribute>();
                    var columnConstraint = GetExistingConstraint(entityDef.Name, prop.Name);
                    if (columnConstraint?.ConstraintName.ToUpper() != fnName.ToUpper())
                        factory.Connection.Execute($"ALTER TABLE [{entityDef.Name}] WITH CHECK ADD  CONSTRAINT [FK_{entityDef.Name}_{prop.Name}] FOREIGN KEY([{prop.Name}])\r\nREFERENCES [{entityRefDef.Name}] ([Id])");
                    else if (columnConstraint?.RefTableName.ToUpper() != entityRefDef.Name.ToUpper())
                    {
                        factory.Connection.Execute($"ALTER TABLE [{entityDef.Name}] DROP CONSTRAINT [{fnName}]");
                        factory.Connection.Execute($"ALTER TABLE [{entityDef.Name}] WITH CHECK ADD  CONSTRAINT [FK_{entityDef.Name}_{prop.Name}] FOREIGN KEY([{prop.Name}])\r\nREFERENCES [{entityRefDef.Name}] ([Id])");
                    }
                }
            }
        }
    }
}
