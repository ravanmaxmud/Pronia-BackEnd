using PrioniaApp.Database.Models.Common;

namespace PrioniaApp.Database.Models
{
    public class PaymentBenefit : BaseEntity ,IAuditable
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Backgroundİmage { get; set; }
        public string BackgroundİmageInFileSystem { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
