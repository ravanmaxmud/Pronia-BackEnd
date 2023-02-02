namespace PrioniaApp.Areas.Client.ViewModels.Home
{
    public class BlogViewModel
    {
        public BlogViewModel(int id, string title, string content, DateTime createdAt, List<TagViewModel> tags, List<CategoryViewModeL> categories, string blogFiles, bool isImage, bool isVidio)
        {
            Id = id;
            Title = title;
            Content = content;
            CreatedAt = createdAt;
            Tags = tags;
            Categories = categories;
            BlogFiles = blogFiles;
            IsImage = isImage;
            IsVidio = isVidio;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<TagViewModel> Tags { get; set; }
        public List<CategoryViewModeL> Categories { get; set; }
        public string BlogFiles { get; set; }
        public bool IsImage { get; set; }
        public bool IsVidio { get; set; }


        public class TagViewModel
        {
            public TagViewModel(string title)
            {
                Title = title;
            }

            public string Title { get; set; }
        }
        public class CategoryViewModeL
        {
            public CategoryViewModeL(string title, string parentTitle)
            {
                Title = title;
                ParentTitle = parentTitle;
            }

            public string Title { get; set; }
            public string ParentTitle { get; set; }


        }

    }
}
