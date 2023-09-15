using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorly.ClientApplication.SDK.Attributes
{
	[AttributeUsage(AttributeTargets.Class)]
	public class PageDefAttribute : Attribute
	{
		public string Path { get; set; }

		public PageDefAttribute(string path)
		{
			Path = path;
		}
	}
}
