namespace Blazorly.ClientApplication.Backend.DB
{
    public class BlazorlyPermission
    {
        public int id { get; set; }

        public Guid role { get; set; }

        public string collection { get; set; }

        public string action { get; set; }

        public string? permissions { get; set; }

        public string? permissions_design { get; set; }

        public string? validation { get; set; }

        public string? validation_design { get; set; }

        public string? fields { get; set; } 
    }
}
