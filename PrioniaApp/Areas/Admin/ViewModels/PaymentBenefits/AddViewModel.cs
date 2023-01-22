using Microsoft.Build.Framework;

namespace PrioniaApp.Areas.Admin.ViewModels.PaymentBenefits
{
    public class AddViewModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public IFormFile? Backgroundİmage { get; set; }
        public string BackgroundİmageInFileSystem { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
