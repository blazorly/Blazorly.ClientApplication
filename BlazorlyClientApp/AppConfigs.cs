using Blazorly.ClientApplication.Core.DB;

namespace BlazorlyClientApp
{
    public class AppConfigs
    {
        public static string? DatabaseType { get; set; }

        public static string? DatabaseConnectionString { get; set; }

        public static int DBTimeout { get; set; } = 30;

        public static Schema Schema { get; set; }
    }
}
