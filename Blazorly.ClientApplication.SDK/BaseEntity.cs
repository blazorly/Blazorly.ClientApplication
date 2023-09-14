using Blazorly.ClientApplication.SDK.Attributes;
using Blazorly.ClientApplication.SDK.System;
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
		public string Id { get; set; } = Nanoid.Generate();

		[FieldDef]
		public SystemUser CreatedBy { get; set; }

		[FieldDef]
		public DateTime CreatedDate { get; set; }

		[FieldDef]
		public SystemUser UpdatedBy { get; set; }

		[FieldDef]
		public DateTime UpdatedDate { get; set; }
    }
}
