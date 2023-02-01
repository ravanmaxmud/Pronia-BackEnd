namespace PrioniaApp.Areas.Admin.ViewModels.BlogFile
{
    public class BlogFileViewModel
    {
        public int BlogId { get; set; }


        public class ListItem 
        {

            public int Id { get; set; }
            public string FileUrl { get; set; }
            public DateTime CreatedAt { get; set; }
            public bool IsImage { get; set; }
            public bool IsVidio { get; set; }
        }
    }
}
