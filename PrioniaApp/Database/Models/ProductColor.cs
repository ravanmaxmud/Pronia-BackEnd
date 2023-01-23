using PrioniaApp.Database.Models.Common;
using System.Drawing;

namespace PrioniaApp.Database.Models
{
    public class ProductColor:BaseEntity
    {
        public int ProductId { get; set; }
        public Product? Product { get; set; }

        public int ColorId { get; set; }
        public Color? Color { get; set; }
    }
}
