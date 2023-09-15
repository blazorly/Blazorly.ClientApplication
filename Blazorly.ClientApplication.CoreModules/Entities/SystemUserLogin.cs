using Blazorly.ClientApplication.SDK;
using Blazorly.ClientApplication.SDK.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorly.ClientApplication.CoreModules.Entities
{
	[EntityDef("SystemUserLogin")]
	public class SystemUserLogin : BaseEntity
	{
		[FieldDef(Required = true)]
		public SystemUser User { get; set; }

		[FieldDef(Required = true)]
		public LoginMethod LoginMethod { get; set; }

		[FieldDef(Length = 200, Required = true)]
		public string Password { get; set; }

		[FieldDef(Required = true)]
		public bool Locked { get; set; }

		[FieldDef(Length = 200)]
		public string? ExternalId { get; set; }
	}
}
