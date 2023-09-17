using Blazorly.ClientApplication.CoreModules.Components.Forms;
using Blazorly.ClientApplication.CoreModules.Entities;
using Blazorly.ClientApplication.SDK;
using Blazorly.ClientApplication.SDK.Attributes;
using Blazorly.ClientApplication.SDK.BuiltIn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorly.ClientApplication.CoreModules.Pages
{
	[PageDef("/users/create")]
	public class CreateUser : PageResource
	{
        public CreateUser() : base(typeof(SystemUser))
        {
        }

        public override void Build()
		{
            this.Add(new PageAction("Save", "Save", SaveClick));

            this.Add(new PageBlock("Heading", new HtmlComponent("<h1>Create New User</h1>")));
			this.Add(new PageBlock("CreateUserForm", new UserForm()));
		}

		private async Task SaveClick(object e)
		{
			var form = GetBlock<FormComponent>("CreateUserForm");
			var user = form.GetEntity<SystemUser>();
			await EntityContext.Create(user);
		}
	}
}
