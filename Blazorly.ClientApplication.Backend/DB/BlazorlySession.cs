namespace Blazorly.ClientApplication.Backend.DB
{
    public class BlazorlySession
    {
        public string token { get; set; }

        public Guid user { get; set; }

        public DateTime expires { get; set; }

        public string? ip { get; set; }

        public string? user_agent { get; set; }

        public string? origin { get; set; }

        public Guid? share { get; set; }
    }
}
