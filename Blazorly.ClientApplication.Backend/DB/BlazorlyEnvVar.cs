namespace Blazorly.ClientApplication.Backend.DB
{
    public class BlazorlyEnvVar
    {
        public string env_key { get; set; }

        public string? category { get; set; }

        public bool is_json { get; set; }

        public string? env_value { get; set; }

        public bool enabled { get; set; }
    }
}
