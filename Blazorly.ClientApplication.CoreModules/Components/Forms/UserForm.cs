using Blazorly.ClientApplication.CoreModules.Entities;
using Blazorly.ClientApplication.SDK;
using Blazorly.ClientApplication.SDK.BuiltIn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorly.ClientApplication.CoreModules.Components.Forms
{
	public class UserForm : FormComponent
	{
        public override void Build()
		{
            this.Controls.Add(new TextBoxComponent("FirstName", "First Name", true, 6));
            this.Controls.Add(new TextBoxComponent("LastName", "Last Name", true, 6));
            this.Controls.Add(new TextBoxComponent("Email", "Email", true, 6));
			this.Controls.Add(new TextBoxComponent("JobTitle", "Job Title", false, 6));
		}
	}
}
