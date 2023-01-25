using PrioniaApp.Database.Models.Common;

namespace PrioniaApp.Database.Models
{
    public class Role : BaseEntity, IAuditable
    {
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public List<User> Users { get; set; }

        public List<ClientSay> ClientSays { get; set; }
    }
}
