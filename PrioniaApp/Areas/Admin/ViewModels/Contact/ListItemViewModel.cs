namespace PrioniaApp.Areas.Admin.ViewModels.Contact
{
    public class ListItemViewModel
    {
        public ListItemViewModel(int id, string firstName, string lastName, string phoneNumber, string email, string content, DateTime cratedAt)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Email = email;
            Content = content;
            CratedAt = cratedAt;
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Content { get; set; }
        public DateTime CratedAt { get; set; }
    }
}
