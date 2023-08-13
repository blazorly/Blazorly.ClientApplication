using Blazorly.ClientApplication.Core.DB;

namespace Blazorly.ClientApplication
{
    public class AppConfigs
    {
        public static string? DatabaseType { get; set; }

        public static string? DatabaseConnectionString { get; set; }

        public static int DBTimeout { get; set; } = 30;

        public static Schema Schema { get; set; }
    }
}
