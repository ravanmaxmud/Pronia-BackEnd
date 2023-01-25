namespace PrioniaApp.Areas.Admin.ViewModels.ClientSay
{
    public class RolesListItemViewModel
    {
        public RolesListItemViewModel(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
