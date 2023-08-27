using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Blazorly.ClientApplication.Core.DB
{
    public class BaseEntity
    {
        public string EntityId { get; set; }

        public BaseEntity(string id)
        {
            this.EntityId = id;
        }

        private Dictionary<string, object> data = new Dictionary<string, object>();

        public string Id => (string)this["Id"];

        public string CreatedBy => (string)this["CreatedBy"];

        public DateTime CreatedDate => (DateTime)this["CreatedDate"];

        public string UpdatedBy => (string)this["UpdatedBy"];

        public DateTime UpdatedDate => (DateTime)this["UpdatedDate"];

        public object this[string key]
        {
            get
            {
                return data[key.ToLower()];
            }
            set
            {
                if (data.ContainsKey(key.ToLower()))
                    data.Add(key.ToLower(), value);
                else
                    data[key.ToLower()] = value;
            }
        }
    }
}
