using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorly.ClientApplication.SDK
{
	public class PageAction
	{
		public string Name { get; set; }

		public string? Label { get; set; }

		public PageActionAlignment Alignment { get; set; }

		public PageActionStyle Style { get; set; }

		public PageActionVariant Variant { get; set; }

		public string? Icon { get; set; }

		public Func<object, Task> OnClick { get; set; }

		public PageAction(string name, string label, Func<object, Task> onClick, string icon = null, PageActionAlignment alignment = PageActionAlignment.Left, 
						PageActionStyle style= PageActionStyle.Primary, PageActionVariant variant = PageActionVariant.Filled)
		{
			Name = name;
			Label = label;
			Alignment = alignment;
			Style = style;
			Variant = variant;
			OnClick = onClick;
			Icon = icon;
		}
	}
}
