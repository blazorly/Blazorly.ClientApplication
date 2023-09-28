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
    public abstract class BaseDBDeployer
    {
        internal string[] ignoreFields = new string[] { "Id", "CreatedBy", "CreatedDate", "UpdatedBy", "UpdatedDate" };
        internal QueryFactory factory;
        internal BaseDBFactory db;
		internal Schema schema;
        public BaseDBDeployer(BaseDBFactory db)
        {
            this.db = db;
            factory = db.factory;
        }

        public void Deploy(string modulePath)
        {
            schema = db.GetSchema();
            var assembly = Assembly.LoadFrom(modulePath + ".dll");
            var entities = assembly.GetTypes().Where(x => x.BaseType.Name == "BaseEntity").ToList();
            foreach (var entity in entities)
            {
                var entityDef = entity.GetCustomAttribute<EntityDefAttribute>();
                bool exist = CheckExist(entityDef.Name);
                string script = "";
                if (!exist)
                {
                    script = CreateTableScript(entity);
                }
                else
                {
                    script = AlterTableScript(entity);
                }

                long retCode = 0;
                if (!string.IsNullOrWhiteSpace(script))
                    retCode = factory.Connection.Execute(script);
            }


            schema = db.GetSchema();
            foreach (var entity in entities)
            {
                ProcessForeignKeys(entity);
            }
        }

        internal bool CheckExist(string name)
        {
            return schema.Collections.Where(x => x.Name.ToUpper() == name.ToUpper()).Any();
        }

		internal TableSchema GetTableSchema(string name)
        {
            return schema.Collections.Where(x => x.Name.ToUpper() == name.ToUpper()).FirstOrDefault();
        }

		internal ColumnConstraint GetExistingConstraint(string entityName, string fieldName)
		{
			var tableSchema = GetTableSchema(entityName);

			var fieldSchema = tableSchema.Fields.Where(x => x.Name.ToUpper() == fieldName.ToUpper()).FirstOrDefault();
			return fieldSchema.Reference;
		}

		internal abstract string CreateTableScript(Type entity);

        internal abstract string AlterTableScript(Type entity);

        internal abstract string GetDType(string ftype, int length, int digits, int decimals);

        internal abstract void ProcessForeignKeys(Type entity);

        
    }
}
