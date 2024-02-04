namespace Blazorly.ClientApplication.Backend.DB
{
    public class BlazorlyResource
    {
        public Guid id {  get; set; }

        public string name { get; set; }

        public string? description { get; set; }

        public string res_type { get; set; }

        public string? collection { get; set; }

        public string? route { get; set; }

        public string? metadata { get; set; }

        public string? design { get; set; }

        public string? scripts { get; set; }

        public string? css_style { get; set; }

        public bool compiled { get; set; }

        public DateTime? timestamp { get; set; }

        public Guid? application { get; set; }

        public bool published { get; set; }
    }
}
