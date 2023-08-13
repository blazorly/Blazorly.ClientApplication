using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Blazorly.ClientApplication.Core.DB
{
    public class Schema
    {
        public List<TableSchema> Collections { get; set; }

        public ColumnConstraint GetRef(string collection, string field)
        {
            var table = Collections.Where(x=>x.Name.ToUpperInvariant() == collection.ToUpperInvariant()).FirstOrDefault();
            if (table == null)
                return null;
            var column = table.Fields.Where(x=>x.Name.ToUpperInvariant() ==  field.ToUpperInvariant()).FirstOrDefault();
            if (column == null)
                return null;

            return column.Reference;
        }
    }

    public class TableSchema
    {
        public string Name { get; set; }

        public string Schema { get; set; }

        public List<TableColumn> Fields { get; set; } = new List<TableColumn>();
    }

    public class TableColumn
    {
        [JsonIgnore]
        public string TableName { get; set; }

        [JsonIgnore]
        public string TableSchema { get; set; }

        public string Name { get; set; }

        public int Position { get; set; }

        public string? Default { get; set; }

        [JsonPropertyName("data_type")]
        public string DataType { get; set; }

        [JsonPropertyName("max_length")]
        public int? MaxLength { get; set; }

        [JsonPropertyName("is_nullable")]
        public string? IsNullable { get; set; }

        [JsonPropertyName("primary_key")]
        public bool IsPrimaryKey { get; set; }

        [JsonPropertyName("foreign_key")]
        public bool IsForeignKey { get; set; }

        public ColumnConstraint Reference { get; set; }
    }

    public class ColumnConstraint
    {
        [JsonIgnore]
        public string ConstraintType { get; set; }

        [JsonIgnore]
        public string ConstraintName { get; set; }

        [JsonIgnore]
        public string TableSchema { get; set; }

        [JsonIgnore]
        public string TableName { get; set; }

        [JsonIgnore]
        public string ColumnName { get; set; }

        [JsonPropertyName("schema")]
        public string RefTableSchema { get; set; }

        [JsonPropertyName("table")]
        public string RefTableName { get; set; }

        [JsonPropertyName("column")]
        public string RefColumnName { get; set; }
    }
}
