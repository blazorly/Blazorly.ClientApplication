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
		public string Id { get; set; } = Nanoid.Generate();

		public string CreatedBy { get; set; }

		public string CreatedByName { get; set; }

		public DateTime CreatedDate { get; set; }

		public string UpdatedBy { get; set; }

		public string UpdatedByName { get; set; }

		public DateTime UpdatedDate { get; set; }
    }
}
