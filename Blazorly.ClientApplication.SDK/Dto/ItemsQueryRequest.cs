using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Blazorly.ClientApplication.SDK.Dto
{
    public class ItemsQueryRequest
    {
        [JsonPropertyName("fields")]
        public JsonDocument? Fields { get; set; }

        [JsonPropertyName("query")]
        public JsonDocument? Filter { get; set; }

        [JsonPropertyName("sort")]
        public JsonDocument? Sort { get; set; }

        [JsonIgnore]
        public Dictionary<string, object> MetaQuery { get; set; }
    }
}
