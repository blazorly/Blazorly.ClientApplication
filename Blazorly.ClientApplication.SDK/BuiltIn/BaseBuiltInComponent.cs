using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorly.ClientApplication.SDK.BuiltIn
{
	public class BaseBuiltInComponent : BaseResource
	{
		public FormControlType ControlType { get; set; }

		public string Prop { get; set; }

		public string? Label { get; set; }

		public object? DefaultValue { get; set; } = null;

		public bool Required { get; set; }

		public int Span { get; set; } = 12;

		public override void Build()
		{
		}
	}
}
