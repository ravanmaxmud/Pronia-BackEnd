using Microsoft.Build.Framework;

namespace PrioniaApp.Areas.Admin.ViewModels.Product
{
    public class AddViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int? Rate { get; set; }
        [Required]
        public List<int> CategoryIds { get; set; }
        [Required]
        public List<int> ColorIds { get; set; }

        [Required]
        public List<int> TagIds { get; set; }
        [Required]
        public List<int> SizeIds { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public List<CatagoryListItemViewModel>? Categories { get; set; }
        [Required]
        public List<SizeListItemViewModel>? Sizes { get; set; }
        [Required]
        public List<ColorListItemViewModel>? Colors { get; set; }
        [Required]
        public List<TagListItemViewModel>? Tags { get; set; }
    }
}
