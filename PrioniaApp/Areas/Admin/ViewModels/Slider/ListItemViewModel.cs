namespace PrioniaApp.Areas.Admin.ViewModels.Slider
{
    public class ListItemViewModel
    {
        public int Id { get; set; }
        public string HeaderTitle { get; set; }
        public string MainTitle { get; set; }
        public string Content { get; set; }
        public string BackgroundİmageUrl { get; set; }
        public string Button { get; set; }
        public string ButtonRedirectUrl { get; set; }
        public DateTime CreatedAt { get; set; }


        public ListItemViewModel(int id, string headerTitle, string mainTitle,
            string content, string backgroundİmageUrl,
            string button, string buttonRedirectUrl, DateTime createdAt)
        {
            Id = id;
            HeaderTitle = headerTitle;
            MainTitle = mainTitle;
            Content = content;
            BackgroundİmageUrl = backgroundİmageUrl;
            Button = button;
            ButtonRedirectUrl = buttonRedirectUrl;
            CreatedAt = createdAt;
        }
    }
}
