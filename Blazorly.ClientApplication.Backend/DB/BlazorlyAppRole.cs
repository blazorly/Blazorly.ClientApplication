namespace Blazorly.ClientApplication.Backend.DB
{
    public class BlazorlyAppRole
    {
        public int id { get; set; }

        public Guid application { get; set; }

        public Guid role { get; set; }

        public bool is_default { get; set; }

        public bool is_admin { get; set; }

        public string? menu_def { get; set; }
    }
}
