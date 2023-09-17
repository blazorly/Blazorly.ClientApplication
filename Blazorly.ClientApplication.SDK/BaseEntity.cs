using Blazorly.ClientApplication.SDK.Attributes;
using NanoidDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Blazorly.ClientApplication.SDK
{
    public class BaseEntity
    {
		[FieldDef]
		public string Id { get; set; }

        [FieldDef]
        public string CreatedBy { get; set; }

		public string? CreatedByName { get; set; }

        [FieldDef]
        public DateTime CreatedDate { get; set; }

        [FieldDef]
        public string UpdatedBy { get; set; }

		public string? UpdatedByName { get; set; }

        [FieldDef]
        public DateTime UpdatedDate { get; set; }
    }
}
