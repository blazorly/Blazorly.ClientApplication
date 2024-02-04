namespace Blazorly.ClientApplication.Backend.DB
{
    public class BlazorlyRelation
    {
        public int id {  get; set; }

        public string many_collection { get; set; }

        public string? many_field { get; set; }

        public string? one_collection { get; set; }

        public string? one_field { get; set; }

        public string? one_collection_field { get; set; }

        public string? one_allowed_collections { get; set; }

        public string? junction_field { get; set; }

        public string? sort_field { get; set; }

        public string? one_deselect_action { get; set; }
    }
}
