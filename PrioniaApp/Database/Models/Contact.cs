using PrioniaApp.Database.Models.Common;

namespace PrioniaApp.Database.Models
{
    public class Contact : BaseEntity,IAuditable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
