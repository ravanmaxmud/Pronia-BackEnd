using Microsoft.Build.Framework;
using PrioniaApp.Database.Models.Enums;

namespace PrioniaApp.Areas.Admin.ViewModels.Order
{
    public class UpdateViewModel
    {
        public string Id { get; set; }
        [Required]
        public OrderStatus Status { get; set; }
    }
}
