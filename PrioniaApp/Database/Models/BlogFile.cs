using PrioniaApp.Database.Models.Common;

namespace PrioniaApp.Database.Models
{
    public class BlogFile : BaseEntity ,IAuditable
    {
        public string? FileName { get; set; }
        public string? FileNameInSystem { get; set; }
        public bool IsImage { get; set; }
        public bool IsVidio { get; set; }
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
