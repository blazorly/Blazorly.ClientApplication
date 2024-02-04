namespace Blazorly.ClientApplication.Backend.DB
{
    public class BlazorlyField
    {
        public int id { get; set; }

        public string collection { get; set; }

        public string field { get; set; }

        public string? note { get; set; }

        public string? special { get; set; }

        public string? @interface { get; set; }

        public string? options { get; set; }

        public string? display { get; set; }

        public string? display_options { get; set; }

        public bool @readonly {get;set;}

        public bool hidden { get; set; }

        public string? sort { get; set; }

        public string? width { get; set; }

        public bool required { get; set; }
    }
}
