namespace PrioniaApp.Areas.Admin.ViewModels.ClientSay
{
    public class ListItemViewModel
    {
        public ListItemViewModel(int id, string userName, string userRole)
        {
            Id = id;
            UserName = userName;
            UserRole = userRole;
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserRole { get; set; }
    }
}
