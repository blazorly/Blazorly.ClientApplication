using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorly.ClientApplication.SDK.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class EntityRefAttribute : Attribute
    {
        public string RefEntity { get; set; }

        public bool Required { get; set; }

        public EntityRefAttribute(string refEntity)
        {
            RefEntity = refEntity;
        }
    }
}
