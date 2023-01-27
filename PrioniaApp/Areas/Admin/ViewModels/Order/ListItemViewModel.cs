using PrioniaApp.Database.Models.Enums;

namespace PrioniaApp.Areas.Admin.ViewModels.Order
{
    public class ListItemViewModel
    {
        public string Id { get; set; }
        public DateTime CreatAt { get; set; }
        public OrderStatus Status { get; set; }
        public decimal Total { get; set; }
        public ListItemViewModel(string id, DateTime creatAt, OrderStatus status, decimal total)
        {
            Id = id;
            CreatAt = creatAt;
            Status = status;
            Total = total;
        }

    }
}
