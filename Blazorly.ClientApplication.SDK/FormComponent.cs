using Blazorly.ClientApplication.SDK.BuiltIn;
using NanoidDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace Blazorly.ClientApplication.SDK
{
	public abstract class FormComponent : BaseResource
	{
		public List<BaseBuiltInComponent> Controls { get; set; } = new List<BaseBuiltInComponent>();

		public Type EntityType { get; set; }

		public FormComponent(Type entityType)
		{
            BlzControl = "BlzFormComponent";
            EntityType = entityType;
        }

        public BaseBuiltInComponent this[string name]
        {
            get
            {
                var comp = Controls.Where(x => x.Prop.ToUpperInvariant() == name.ToUpperInvariant()).FirstOrDefault();
                if (comp == null)
                    throw new Exception("Property not found");

                return comp;
            }
        }

        public override abstract void Build();

        public T GetEntity<T>()
        {
            var constructors = EntityType.GetConstructors();
            var entityInstance = constructors[0].Invoke(null);
            var properties = EntityType.GetProperties();
            foreach ( var ctl in Controls)
            {
                var property = properties.Where(x=>x.Name.ToUpperInvariant() == ctl.Prop.ToUpperInvariant()).FirstOrDefault();
                if(property == null)
                    continue;

                property.SetValue(entityInstance, ctl.Value);
            }

            return (T)entityInstance;
        }
    }
}
