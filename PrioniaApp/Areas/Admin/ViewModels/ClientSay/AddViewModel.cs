using Microsoft.Build.Framework;

namespace PrioniaApp.Areas.Admin.ViewModels.ClientSay
{
    public class AddViewModel
    {
        [Required]
        public string Content { get; set; }
        [Required]
        public int UserId { get; set; }
        public List<UserListItemViewModel>? Users { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt  { get; set; }
    }
}
