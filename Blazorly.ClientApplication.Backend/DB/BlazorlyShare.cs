namespace Blazorly.ClientApplication.Backend.DB
{
    public class BlazorlyShare
    {
        public Guid id { get; set; }

        public string? name { get; set; }

        public string? collection { get; set; }

        public Guid? item { get; set; }

        public Guid? role { get; set; }

        public string? password { get; set; }

        public DateTime? date_start { get; set; }

        public DateTime? date_end { get; set; }

        public int? times_used { get; set; }

        public int? max_uses { get; set; }

        public Guid user_created { get; set; }

        public DateTime date_created { get; set; }
    }
}
