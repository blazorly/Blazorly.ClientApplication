using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorly.ClientApplication.SDK.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class EntityDefAttribute : Attribute
    {
        public string Name { get; set; }

		public EntityDefAttribute(string name)
        {
            this.Name = name;
        }
    }
}
