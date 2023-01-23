using PrioniaApp.Database.Models.Common;

namespace PrioniaApp.Database.Models
{
    public class Color : BaseEntity ,IAuditable
    {
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }

        public List<ProductColor> ProductColors { get; set; }
    }
}
