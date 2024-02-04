namespace Blazorly.ClientApplication.Backend.DB
{
    public class BlazorlyRole
    {
        public Guid id { get; set; }

        public string name { get; set; }

        public string? description { get; set; }

        public string? icon { get; set; }

        public string? ip_access { get; set; }

        public bool enforce_tfa { get; set; }

        public bool admin_access { get; set; }

        public bool app_access { get; set; }
    }
}
