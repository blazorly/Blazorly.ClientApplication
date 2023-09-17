using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorly.ClientApplication.SDK.BuiltIn
{
	public class BaseBuiltInComponent : BaseResource
	{
		public BaseBuiltInComponent(string blzControl) 
		{
			this.BlzControl = blzControl;
		}

		public string Prop { get; set; }

		public string? Label { get; set; }

		public object? Value { get; set; } = null;

		public bool Required { get; set; }

		public int Span { get; set; } = 12;

		public bool Visible { get; set; }

		public override void Build()
		{
		}
    }
}
