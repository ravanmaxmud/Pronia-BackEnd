using PrioniaApp.Areas.Client.ViewModels.ShopPage;

namespace PrioniaApp.Areas.Client.ViewModels.Home
{
    public class IndexViewModel
    {
        public List<SliderViewModel> Sliders { get; set; }
        public List<PaymentBenefitsViewModel> PaymentBenefits { get; set; }

        public List<ProductListItemViewModel> Products { get; set; }
        public List<FeedBackViewModel> FeedBacks { get; set; }

        public List<ListItemViewModel> ShopPageProducts { get; set; }
    }
}
