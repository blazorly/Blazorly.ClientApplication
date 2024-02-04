namespace Blazorly.ClientApplication.Backend.DB
{
    public class BlazorlyCollection
    {
        public string collection { get; set; }

        public string? note { get; set; }

        public string? display_template { get; set; }

        public bool singleton { get; set; }

        public string? accountability { get; set; }

        public string? icon { get; set; }

        public string? color { get; set; }

        public string? item_duplication_fields { get; set; }

        public string collapse { get; set; } = "open";
    }
}
