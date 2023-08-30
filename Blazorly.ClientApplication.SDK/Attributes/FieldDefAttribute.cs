using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorly.ClientApplication.SDK.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FieldDefAttribute : Attribute
    {
        public int Length { get; set; }

        public bool Required { get; set; }

        public double MinValue { get; set; }
    }
}
