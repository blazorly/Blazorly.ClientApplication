namespace Blazorly.ClientApplication.Backend.DB
{
    public class BlazorlyUser
    {
        public Guid id { get; set; }

        public string first_name { get; set; }

        public string last_name { get; set; }

        public string email { get; set; }

        public string? password { get; set; }

        public string? location { get; set; }

        public string? title { get; set; }

        public string? description { get; set; }

        public string? tags { get; set; }

        public string? avatar { get; set; }

        public string? language { get; set; }

        public string? theme { get; set; }

        public string? tfa_secret { get; set; }

        public string status { get; set; }

        public Guid? role { get; set; }

        public string? token { get; set; }

        public DateTime? last_access { get; set; }

        public string? last_page { get; set; }

        public string? provider { get; set; }

        public string? external_identifier { get; set; }

        public string? auth_data { get; set; }

        public bool email_notifications { get; set; }
    }
}
