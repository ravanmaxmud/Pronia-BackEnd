namespace PrioniaApp.Areas.Admin.ViewModels.FeedBack
{
    public class UserListItemViewModel
    {
        public UserListItemViewModel(int id, string name, string lastName, string email, string role)
        {
            Id = id;
            Name = name;
            LastName = lastName;
            Email = email;
            Role = role;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

    }
}
