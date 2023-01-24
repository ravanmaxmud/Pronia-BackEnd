namespace PrioniaApp.Areas.Client.ViewModels.Home.Modal
{
    public class ModalViewModel
    {
        public ModalViewModel(string title, string description, int price, string imgUrl)
        {
            Title = title;
            Description = description;
            Price = price;
            ImgUrl = imgUrl;
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string ImgUrl { get; set; }

    }
}
