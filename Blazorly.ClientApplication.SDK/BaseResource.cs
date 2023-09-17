using NanoidDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorly.ClientApplication.SDK
{
	public abstract class BaseResource
	{
		public string Id { get; set; }

        public string BlzControl { get; set; }

        public Dictionary<string, string> Props { get; set; } = new Dictionary<string, string>();

		public BaseResource(string id = null)
		{
			if (id == null)
				id = Nanoid.Generate();

			Id = id;
			this.Build();
		}

		public abstract void Build();

        public override string ToString()
        {
			return this.BlzControl;
        }
    }
}
