using Blazorly.ClientApplication.Core.DB;
using Blazorly.ClientApplication.Core.Dto;
using SqlKata;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Blazorly.ClientApplication.Core
{
    public class DBUtils
    {
        public static void ParseSelectFields(Schema schema, string collection, ItemsQueryRequest queryRequest, Query query)
        {
            List<string> calculatedFields = new List<string>();
            if (queryRequest.Fields == null)
                calculatedFields.Add("*");

            query = query.From($"{collection} AS t0");
            if (queryRequest.Fields != null)
            {
                int tCount = 0;
                foreach (var field in queryRequest.Fields.RootElement.EnumerateObject())
                {
                    if (field.Value.ValueKind == JsonValueKind.Object)
                    {
                        tCount++;
                        var table_ref = schema.GetRef(collection, field.Name);
                        query = query.Join($"{table_ref.RefTableName} AS t{tCount}", $"t0.{field.Name}", $"t{tCount}.{table_ref.RefColumnName}");

                        foreach (var inn_field in field.Value.EnumerateObject())
                        {
                            calculatedFields.Add($"t{tCount}.{inn_field.Name} AS {field.Name}.{GetFieldAlias(inn_field)}");
                        }
                    }
                    else
                    {
                        calculatedFields.Add($"t0.{field.Name} AS {GetFieldAlias(field)}");
                    }
                }
            }

            query = query.Select(calculatedFields);
        }

        private static string GetFieldAlias(JsonProperty f)
        {
            if (f.Value.ValueKind == JsonValueKind.Null)
                return f.Name;

            if(f.Value.ValueKind == JsonValueKind.String && string.IsNullOrWhiteSpace(f.Value.GetString()))
                return f.Name;

            return f.Value.ToString();
        }

        public static void ParseQuery(string collection, JsonElement element, Query query, string clause_cond = "_and")
        {
            if (element.ValueKind == System.Text.Json.JsonValueKind.Object)
            {
                foreach (var prop in element.EnumerateObject())
                {
                    if (prop.Value.ValueKind == System.Text.Json.JsonValueKind.Object)
                    {
                        if (prop.Name == "_or")
                        {
                            ParseQuery(collection, prop.Value, query, "_or");
                        }
                        else if (prop.Name == "_and")
                        {
                            ParseQuery(collection, prop.Value, query);
                        }
                        else
                        {
                            ParseInnerQuery(prop.Value, query, $"{collection}.{prop.Name}", clause_cond);
                        }
                    }
                    else
                    {
                        SetValue(query, $"{collection}.{prop.Name}", "=", prop.Value, clause_cond);
                    }
                }
            }
            else if (element.ValueKind == System.Text.Json.JsonValueKind.Array)
            {
                foreach (var item in element.EnumerateArray())
                {
                    ParseQuery(collection, item, query);
                }
            }
        }

        private static void ParseInnerQuery(JsonElement element, Query query, string col, string clause_cond = "_and")
        {
            foreach (var prop in element.EnumerateObject())
            {
                switch (prop.Name)
                {
                    case "_in":
                    case "_nin":
                    case "_between":
                    case "_nbetween":
                        SetArrayValue(query, col, prop.Name, prop.Value, clause_cond);
                        break;
                    case "_null":
                        query = clause_cond == "_or" ? query.OrWhereNull(col) : query.WhereNull(col);
                        break;
                    case "_nnull":
                        query = clause_cond == "_or" ? query.OrWhereNotNull(col) : query.WhereNotNull(col);
                        break;
                    case "_contains":
                        query = clause_cond == "_or" ? query.OrWhereContains(col, prop.Value.ToString()) : query.WhereContains(col, prop.Value.ToString());
                        break;
                    case "_ncontains":
                        query = clause_cond == "_or" ? query.OrWhereNotContains(col, prop.Value.ToString()) : query.WhereNotContains(col, prop.Value.ToString());
                        break;
                    case "_starts_with":
                        query = clause_cond == "_or" ? query.OrWhereStarts(col, prop.Value.ToString()) : query.WhereStarts(col, prop.Value.ToString());
                        break;
                    case "_nstarts_with":
                        query = clause_cond == "_or" ? query.OrWhereNotStarts(col, prop.Value.ToString()) : query.WhereNotStarts(col, prop.Value.ToString());
                        break;
                    case "_ends_with":
                        query = clause_cond == "_or" ? query.OrWhereEnds(col, prop.Value.ToString()) : query.WhereEnds(col, prop.Value.ToString());
                        break;
                    case "_nends_with":
                        query = clause_cond == "_or" ? query.OrWhereNotEnds(col, prop.Value.ToString()) : query.WhereNotEnds(col, prop.Value.ToString());
                        break;
                    default:
                        SetValue(query, col, DBUtils.GetOp(prop.Name), prop.Value, clause_cond);
                        break;
                }
            }
        }

        private static void SetValue(Query query, string col, string op, JsonElement value, string clause_cond = "_and")
        {
            if (value.ValueKind == JsonValueKind.String)
            {
                query = clause_cond == "_or" ? query.OrWhere(col, op, value.GetString()) : query.Where(col, op, value.GetString());
            }
            else if (value.ValueKind == JsonValueKind.Number)
            {
                query = clause_cond == "_or" ? query.OrWhere(col, op, value.ToString()) : query.Where(col, op, value.ToString());
            }
            else if (value.ValueKind == JsonValueKind.Null)
            {
                if (op == "=")
                    query = clause_cond == "_or" ? query.OrWhereNull(col) : query.WhereNull(col);
                if (op == "<>")
                    query = clause_cond == "_or" ? query.OrWhereNotNull(col) : query.WhereNotNull(col);
            }
            else if (value.ValueKind == JsonValueKind.True)
            {
                query = clause_cond == "_or" ? query.OrWhereTrue(col) : query.WhereTrue(col);
            }
            else if (value.ValueKind == JsonValueKind.False)
            {
                query = clause_cond == "_or" ? query.OrWhereFalse(col) : query.WhereFalse(col);
            }
        }

        private static void SetArrayValue(Query query, string col, string op, JsonElement value, string clause_cond = "_and")
        {
            List<object> values = new List<object>();
            foreach (var item in value.EnumerateArray())
            {
                var type = item.GetType();
                if (item.ValueKind == JsonValueKind.String || item.ValueKind == JsonValueKind.Number)
                    values.Add(item.ToString());
                else if (item.ValueKind == JsonValueKind.True)
                    values.Add(1);
                else if (item.ValueKind == JsonValueKind.False)
                    values.Add(0);
                else if (item.ValueKind == JsonValueKind.Null)
                    values.Add(null);
            }

            switch (op)
            {
                case "_in":
                    query = clause_cond == "_or" ? query.OrWhereIn(col, values) : query.WhereIn(col, values);
                    break;
                case "_nin":
                    query = clause_cond == "_or" ? query.OrWhereNotIn(col, values) : query.WhereNotIn(col, values);
                    break;
                case "_between":
                    if (values.Count == 2)
                        query = clause_cond == "_or" ? query.OrWhereBetween(col, values[0], values[1]) : query.WhereBetween(col, values[0], values[1]);
                    break;
                case "_nbetween":
                    if (values.Count == 2)
                        query = clause_cond == "_or" ? query.OrWhereNotBetween(col, values[0], values[1]) : query.WhereNotBetween(col, values[0], values[1]);
                    break;
            }
        }

        public static string GetOp(string op)
        {
            switch (op)
            {
                case "_eq": return "=";
                case "_neq": return "<>";
                case "_lt": return "<";
                case "_lte": return "<=";
                case "_gt": return ">";
                case "_gte": return ">=";
            }

            return "=";
        }

        public static void ParseSort(JsonDocument? sortDoc, Query query)
        {
            if (sortDoc == null)
                return;

            foreach (var item in sortDoc.RootElement.EnumerateObject())
            {
                if(item.Value.ValueKind == JsonValueKind.Null)
                {
                    query.OrderBy(item.Name);
                    continue;
                }

                if(item.Value.GetBoolean())
                    query.OrderByDesc(item.Name);
                else
                    query.OrderBy(item.Name);
            }
        }

        public static List<ExpandoObject> ParseQueryResult(List<dynamic> data)
        {
            var result = new List<ExpandoObject>();
            foreach (var rec in data)
            {
                var record = (IDictionary<string, object>)rec;
                ExpandoObject keyValuePairs = new ExpandoObject();
                foreach (var item in record)
                {
                    if(item.Key.Contains("."))
                    {
                        var split = item.Key.Split(".");
                        var existing = keyValuePairs.Where(x => x.Key == split[0]).ToList();
                        if(existing.Count == 0)
                        {
                            var obj = new ExpandoObject();
                            obj.TryAdd(split[1], item.Value);
                            keyValuePairs.TryAdd(split[0], obj);
                        }
                        else
                        {
                            if(existing[0].Value.GetType() != typeof(ExpandoObject))
                            {
                                var obj1 = new ExpandoObject();
                                obj1.TryAdd(existing[0].Key, existing[0].Value);
                                existing[0] = new KeyValuePair<string, object?>(existing[0].Key, obj1);
                            }
                            
                            ((ExpandoObject)existing[0].Value).TryAdd(split[1], item.Value);
                        }
                    }
                    else
                    {
                        keyValuePairs.TryAdd(item.Key, item.Value);
                    }
                }

                result.Add(keyValuePairs);
            }

            return result;
        }
    }
}
