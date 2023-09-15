using NanoidDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorly.ClientApplication.SDK
{
	public abstract class DataTableComponent : BaseResource
	{
		public object Query { get; set; }

		public DataTableComponent(string id) : base(id)
		{
		}

		public override abstract void Build();
	}
}
