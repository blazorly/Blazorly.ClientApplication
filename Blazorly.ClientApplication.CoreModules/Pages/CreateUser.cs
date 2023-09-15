using Blazorly.ClientApplication.CoreModules.Components.Forms;
using Blazorly.ClientApplication.CoreModules.Entities;
using Blazorly.ClientApplication.SDK;
using Blazorly.ClientApplication.SDK.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorly.ClientApplication.CoreModules.Pages
{
	[PageDef("/users/create")]
	public class CreateUser : PageResource<SystemUser>
	{
		public override void Build()
		{
			this.Components.Add(new UserForm());
		}
	}
}
