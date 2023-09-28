using Blazorly.ClientApplication.SDK;
using Blazorly.ClientApplication.SDK.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorly.ClientApplication.CoreModules.Entities
{
	[EntityDef("SystemRolePermission")]
	public class SystemRolePermission : BaseEntity
	{
		[FieldDef]
		public SystemRole Role { get; set; }

		[FieldDef]
		public string Entity { get; set; }

		[FieldDef]
		public PermissionLevel Read { get; set; }

		[FieldDef]
		public PermissionLevel Create { get; set; }

		[FieldDef]
		public PermissionLevel Update { get; set; }

		[FieldDef]
		public PermissionLevel Delete { get; set; }

        [FieldDef]
        public string RestrictMeta { get; set; }
	}
}
