using PrioniaApp.Database.Models.Common;

namespace PrioniaApp.Database.Models
{
    public class Blog : BaseEntity , IAuditable
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsVidio { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
