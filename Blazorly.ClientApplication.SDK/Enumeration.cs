using Blazorly.ClientApplication.SDK.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorly.ClientApplication.SDK
{
	public enum LoginMethod
	{
		[OptionDisplay("Password")]
		Password,
		[OptionDisplay("Azure AD")]
		AzureAD,
		[OptionDisplay("Auth0")]
		Auth0,
		[OptionDisplay("Facebook")]
		Facebook,
		[OptionDisplay("Google")]
		Google,
		[OptionDisplay("Github")]
		Github
	}
}
