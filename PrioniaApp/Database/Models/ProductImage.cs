using PrioniaApp.Database.Models.Common;

namespace PrioniaApp.Database.Models
{
    public class ProductImage : BaseEntity , IAuditable
    {
        public string? ImageName { get; set; }
        public string? ImageNameInFileSystem { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
