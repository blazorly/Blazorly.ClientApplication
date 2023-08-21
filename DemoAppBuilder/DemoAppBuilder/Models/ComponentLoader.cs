using DemoAppBuilder.Builder;
using Scriban;
using Scriban.Runtime;

namespace DemoAppBuilder.Models
{
    public class ComponentLoader
    {
        public ComponentLoader(string id, object data = null)
        {
            this.Id = id;
            this.Data = data;
        }

        public string Id { get; set; }

        public object Data { get; set; }

        public string GetHtml()
        {
            string templateHtml = File.ReadAllText($"AppData/Components/{Id}.html");
            var scriptObject1 = new BuilderFunctions();
            var template = Template.Parse(templateHtml);
            var context = new TemplateContext();

            scriptObject1.Add("data", Data);
            context.PushGlobal(scriptObject1);

            var result = template.Render(context);
            return result;
        }
    }
}
