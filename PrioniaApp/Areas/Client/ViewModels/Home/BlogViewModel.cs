namespace PrioniaApp.Areas.Client.ViewModels.Home
{
    public class BlogViewModel
    {
        public BlogViewModel(int id, string title, string content, DateTime createdAt, string blogFiles, bool isImage, bool isVidio)
        {
            Id = id;
            Title = title;
            Content = content;
            CreatedAt = createdAt;
            BlogFiles = blogFiles;
            IsImage = isImage;
            IsVidio = isVidio;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public string BlogFiles { get; set; }
        public bool IsImage { get; set; }
        public bool IsVidio { get; set; }




    }
}
