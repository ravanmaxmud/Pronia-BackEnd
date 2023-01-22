using Microsoft.Build.Framework;

namespace PrioniaApp.Areas.Admin.ViewModels.Navbars
{
    public class AddViewModel
    {

        [Required]
        public string? Name { get; set; }
        [Required]
        public string? ToURL { get; set; }
        [Required]
        public int Order { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }

    }
}
