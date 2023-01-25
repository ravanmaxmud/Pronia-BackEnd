using Microsoft.Build.Framework;

namespace PrioniaApp.Areas.Admin.ViewModels.ClientSay
{
    public class AddViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public List<int> RoleIds { get; set; }
        [Required]
        public List<RolesListItemViewModel> Roles { get; set; }
        [Required]
        public IFormFile? ImageName { get; set; }

        [Required]
        public string? ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt  { get; set; }
    }
}
