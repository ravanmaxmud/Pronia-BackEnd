namespace PrioniaApp.Areas.Admin.ViewModels.ClientSay
{
    public class ListItemViewModel
    {
        public ListItemViewModel(int id, string userName, string userRole, string imageUrl)
        {
            Id = id;
            UserName = userName;
            UserRole = userRole;
            ImageUrl = imageUrl;
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserRole { get; set; }
        public string ImageUrl { get; set; }
    }
}
