namespace Blazorly.ClientApplication.Backend.DB
{
    public class BlazorlyFile
    {
        public Guid id { get; set; }

        public string name { get; set; }

        public string? file_type { get; set; }

        public bool is_folder { get; set; }

        public Guid uploaded_by { get; set; }

        public DateTime uploaded_on { get; set; }

        public string? tags { get; set; }

        public string? metadata { get; set; }
    }
}
