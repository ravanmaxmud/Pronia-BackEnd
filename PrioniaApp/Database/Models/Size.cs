using PrioniaApp.Database.Models.Common;

namespace PrioniaApp.Database.Models
{
    public class Size : BaseEntity, IAuditable
    {
        public string Title { get; set; }
        public DateTime CreatedAt { get ; set; }
        public DateTime UpdateAt { get; set; }
        public List<ProductSize>? ProductSizes { get; set; }
    }
}
