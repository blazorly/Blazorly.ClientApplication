namespace Blazorly.ClientApplication.Backend.DB
{
    public class BlazorlyApplication
    {
        public Guid id { get; set; }

        public string name { get; set; }

        public string? description { get; set; }

        public string? domain { get; set; }

        public string? logo { get; set; }

        public string? environment { get; set; }

        public string? app_meta { get; set; }

        public int? version { get; set; }

        public bool under_maintenance { get; set; }

        public DateTime last_released { get; set; }
    }
}
