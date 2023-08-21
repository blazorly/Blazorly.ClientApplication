using Scriban.Runtime;

namespace DemoAppBuilder.Builder
{
    public class BuilderFunctions : ScriptObject
    {
        public static string Hello(string name)
        {
            return $"Hello, {name}";
        }

    }
}
