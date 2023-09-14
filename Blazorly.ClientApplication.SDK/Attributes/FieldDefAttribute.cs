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
        public int Length { get; set; } = 255;

		public int Digits { get; set; } = 18;

		public int Decimals { get; set; } = 2;

        public bool Required { get; set; } = false;

        public double MinValue { get; set; }

		public double MaxValue { get; set; }

		public FieldDefAttribute()
        {

        }
    }
}
