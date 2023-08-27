using Blazorly.ClientApplication.SDK;
using Blazorly.ClientApplication.SDK.Attributes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorly.Modules.CRM.Leads.Entities
{
    public class Lead : BaseEntity
    {
        public Lead() : base("Lead")
        {
        }

        [FieldDef(Length=50, MinVal = 0)]
        public string FirstName { get; set; }
    }
}
