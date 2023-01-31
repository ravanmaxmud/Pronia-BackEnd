using Microsoft.Build.Framework;

namespace PrioniaApp.Areas.Admin.ViewModels.Reward
{
    public class AddViewModel
    {
        [Required]
        public IFormFile Image { get; set; }
    }
}
