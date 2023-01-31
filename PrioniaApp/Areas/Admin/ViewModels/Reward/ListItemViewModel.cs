namespace PrioniaApp.Areas.Admin.ViewModels.Reward
{
    public class ListItemViewModel
    {
        public ListItemViewModel(int id, string? imageUrl, DateTime createdAt)
        {
            Id = id;
            ImageUrl = imageUrl;
            CreatedAt = createdAt;
        }

        public int Id { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
