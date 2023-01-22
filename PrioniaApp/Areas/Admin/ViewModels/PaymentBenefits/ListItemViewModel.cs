namespace PrioniaApp.Areas.Admin.ViewModels.PaymentBenefits
{
    public class ListItemViewModel
    {
        public ListItemViewModel(int id, string title, string content,string backgroundİmageInFileSystem, DateTime createdAt, DateTime updateAt)
        {
            Id = id;
            Title = title;
            Content = content;
            BackgroundİmageInFileSystem = backgroundİmageInFileSystem;
            CreatedAt = createdAt;
            UpdateAt = updateAt;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string BackgroundİmageInFileSystem { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
