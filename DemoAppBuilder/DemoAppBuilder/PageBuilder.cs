using DemoAppBuilder.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoAppBuilder
{
    public class PageBuilder : Controller
    {
        public static PageBuilder Instance { get; set; } = new PageBuilder();

        public PageBuilder()
        {
            //if(Instance == null)
            //    Instance = new PageBuilder();
        }

        public IActionResult LoadComponent(string name, object model = null)
        {
            return PartialView(name, model);
        }
    }
}
