namespace PrioniaApp.Areas.Client.ViewModels.Home
{
    public class PaymentBenefitsViewModel
    {
      

        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string BackGroundImageUrl { get; set; }
        public PaymentBenefitsViewModel()
        {
        }

        public PaymentBenefitsViewModel(int id, string title, string content, string backGroundImageUrl)
        {
            Id = id;
            Title = title;
            Content = content;
            BackGroundImageUrl = backGroundImageUrl;
        }
    }
}
