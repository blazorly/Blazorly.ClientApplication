using NanoidDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorly.ClientApplication.SDK
{
	public abstract class FormComponent<T> : BaseResource where T : BaseEntity
	{
		public List<BaseResource> Controls { get; set; } = new List<BaseResource>();

		public override abstract void Build();
	}
}
