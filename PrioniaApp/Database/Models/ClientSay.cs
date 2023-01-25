using PrioniaApp.Database.Models.Common;

namespace PrioniaApp.Database.Models
{
    public class ClientSay : BaseEntity , IAuditable
    {
        public string UserName { get; set; }
        public string Content { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public string ImageName { get; set; }
        public string İmageInFileSystem { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
