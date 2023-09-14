using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorly.ClientApplication.SDK.Attributes
{
	[AttributeUsage(AttributeTargets.Field)]
	public class OptionDisplayAttribute : Attribute
	{
		public string Label { get; set; }

		public OptionDisplayAttribute(string label) 
		{
			this.Label = label;
		}
	}
}
