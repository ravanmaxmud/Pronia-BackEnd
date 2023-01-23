using PrioniaApp.Database.Models.Common;

namespace PrioniaApp.Database.Models
{
    public class Catagory : BaseEntity,IAuditable
    {
        public string Title { get; set; }
        public int? ParentId { get; set; }
        public Catagory? Parent { get; set; }
        public List<ProductCatagory> ProductCatagories { get; set; }
        public List<Catagory> Catagories { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
