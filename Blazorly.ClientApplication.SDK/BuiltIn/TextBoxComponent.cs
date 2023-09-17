using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorly.ClientApplication.SDK.BuiltIn
{
	public class TextBoxComponent : BaseBuiltInComponent
	{
		public TextBoxComponent(string prop, string? label = null, bool required = false, int span = 12, object? defaultValue = null)
            : base("BlzTextBoxComponent")
        {
			this.Prop = prop;
			this.Label = label == null ? this.Label : label;
			this.Required = required;
			this.Span = span;
			this.Value = defaultValue;
		}
	}
}
