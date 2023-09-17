using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorly.ClientApplication.SDK.BuiltIn
{
	public class HtmlComponent : BaseBuiltInComponent
	{
		public HtmlComponent(string html, int span = 12)
			: base("BlzHtmlComponent")
        {
			this.Span = span;
			this.Value = html;
		}
	}
}
