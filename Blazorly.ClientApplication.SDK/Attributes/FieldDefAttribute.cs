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

        public decimal MinVal { get; set; }

        public decimal MaxVal { get; set; }

    }
}
