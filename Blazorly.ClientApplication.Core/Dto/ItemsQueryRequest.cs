using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Blazorly.ClientApplication.Core.Dto
{
    public class ItemsQueryRequest
    {
        [JsonPropertyName("entity")]
        public string? Entity { get; set; }

        [JsonPropertyName("fields")]
        public JsonDocument? Fields { get; set; }

        [JsonPropertyName("query")]
        public JsonDocument? Filter { get; set; }

        [JsonPropertyName("sort")]
        public JsonDocument? Sort { get; set; }
    }
}
