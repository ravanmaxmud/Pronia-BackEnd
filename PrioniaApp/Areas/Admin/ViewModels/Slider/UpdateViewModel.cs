using System.ComponentModel.DataAnnotations;

namespace PrioniaApp.Areas.Admin.ViewModels.Slider
{
    public class UpdateViewModel
    {
        public int Id { get; set; }
        [Required]
        public string HeaderTitle { get; set; }
        [Required]
        public string MainTitle { get; set; }
        [Required]
        public string Content { get; set; }
        public IFormFile Backgroundİmage { get; set; }
        public string? BackgroundİmageUrl { get; set; }
        [Required]
        public string Button { get; set; }
        [Required]
        public string ButtonRedirectUrl { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

