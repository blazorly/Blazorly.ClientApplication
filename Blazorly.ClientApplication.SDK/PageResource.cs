using NanoidDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorly.ClientApplication.SDK
{
	public abstract class PageResource<T> : BaseResource where T : BaseEntity
	{
		public List<BaseResource> Components { get; set; } = new List<BaseResource>();

		public override abstract void Build();
	}
}
