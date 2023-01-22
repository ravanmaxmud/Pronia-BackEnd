namespace PrioniaApp.Areas.Admin.ViewModels.Navbars
{
    public class ListItemViewModel
    {
        public ListItemViewModel(int id, string? name, string? toURL, int order, DateTime createdAt)
        {
            Id = id;
            Name = name;
            ToURL = toURL;
            Order = order;
            CreatedAt = createdAt;
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? ToURL { get; set; }
        public int Order { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
