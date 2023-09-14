using Blazorly.ClientApplication.SDK.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorly.ClientApplication.SDK.System
{
	[EntityDef("SystemRole")]
	public class SystemRole : BaseEntity
	{
		[FieldDef(Length = 100, Required = true)]
		public string Name { get; set; }

		[FieldDef(Length = 1000)]
		public string? Description { get; set; }
	}
}
