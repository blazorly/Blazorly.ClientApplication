using Blazorly.ClientApplication.SDK;
using Blazorly.ClientApplication.SDK.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorly.ClientApplication.CoreModules.Entities
{
	[EntityDef("SystemUser")]
	public class SystemUser : BaseEntity
	{
		[FieldDef(Length = 50, Required = true)]
		public string FirstName { get; set; }

		[FieldDef(Length = 50, Required = true)]
		public string LastName { get; set; }

		[FieldDef(Length = 100, Required = true)]
		public string Email { get; set; }

		[FieldDef(Length = 20)]
		public string? Mobile { get; set; }

		[FieldDef(Length = 50)]
		public string? JobTitle { get; set; }

		[FieldDef()]
		public SystemRole? Role { get; set; }

		[FieldDef(Required = true)]
		public bool IsActive { get; set; }
	}
}
