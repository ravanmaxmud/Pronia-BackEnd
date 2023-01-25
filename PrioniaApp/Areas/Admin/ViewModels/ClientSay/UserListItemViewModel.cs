namespace PrioniaApp.Areas.Admin.ViewModels.ClientSay
{
    public class UserListItemViewModel
    {
        public UserListItemViewModel(int id, string name, string role)
        {
            Id = id;
            Name = name;
            Role = role;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }

    }
}
