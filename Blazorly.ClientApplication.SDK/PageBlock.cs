using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorly.ClientApplication.SDK
{
	public class PageBlock
	{
		public string Name { get; set; }

		public BaseResource Resource { get; set; }

		public int Span { get; set; }

		public PageBlock(string name, BaseResource resource, int span = 12) 
		{
			Name = name;
			Resource = resource;
			Span = span;
		}	
	}
}
