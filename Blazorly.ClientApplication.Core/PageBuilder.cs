using Blazorly.ClientApplication.SDK;
using Blazorly.ClientApplication.SDK.Attributes;
using Org.BouncyCastle.Asn1.Mozilla;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Blazorly.ClientApplication.Core
{
    public class PageBuilder
    {
        public Dictionary<string, BaseResource> Resources { get; set; } = new Dictionary<string, BaseResource>();

        public Dictionary<string, PageResource> Pages { get; set; } = new Dictionary<string, PageResource>();

        public void LoadModuleLibrary(string moduleLibrary)
        {
            var path = Assembly.GetExecutingAssembly().Location;
            path = path.Replace("Blazorly.ClientApplication.Core.dll", moduleLibrary + ".dll");
            var assembly = Assembly.LoadFile(path);
            var resources = assembly.GetTypes().Where(x => x.BaseType.Name == "FormComponent" || x.BaseType.Name == "DataTableComponent").ToList();
            var pages = assembly.GetTypes().Where(x => x.BaseType.Name == "PageResource").ToList();
            foreach (var item in pages)
            {
                AddPage((PageResource)assembly.CreateInstance(item.FullName));
            }

            foreach (var item in resources)
            {
                AddResource((BaseResource)assembly.CreateInstance(item.FullName));
            }
        }

        public void AddPage(PageResource page)
        {
            var pageDef = page.GetType().GetCustomAttribute<PageDefAttribute>();
            if(pageDef == null) 
                throw new Exception($"Missing PageDef attribute for {page.GetType().FullName}");

            var route = pageDef.Path;
            if (!route.StartsWith("/"))
                route = $"/{route.ToLowerInvariant()}";

            if (Pages.ContainsKey(route) && !pageDef.OverrideExisting)
                throw new Exception($"Duplicate routing defined in multiple classes: {route}");

            if(!Pages.ContainsKey(route))
                Pages.Add(route, page);
            else
                Pages[route] = page;
        }

        public void AddResource(BaseResource resource)
        {
            if (Resources.ContainsKey(resource.Id))
                throw new Exception($"Duplicate Id defined in multiple classes: {resource.Id}");

            if (!Pages.ContainsKey(resource.Id))
                Resources.Add(resource.Id, resource);
            else
                Resources[resource.Id] = resource;
        }

        public PageResource GetPage(string route)
        {
            route = route.StartsWith("/") ? route : $"/{route}";
            if (Pages.ContainsKey(route))
                return Pages[route];

            return null;
        }
    }
}
