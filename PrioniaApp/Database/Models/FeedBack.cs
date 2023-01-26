using PrioniaApp.Database.Models.Common;

namespace PrioniaApp.Database.Models
{
    public class FeedBack : BaseEntity, IAuditable
    {
        public string? ImageName { get; set; }
        public string? ImageNameInFileSystem { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
