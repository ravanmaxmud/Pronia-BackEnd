namespace PrioniaApp.Areas.Admin.ViewModels.SubNavbars
{
    public class UpdateViewModel
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string ToURL { get; set; }
        public int Order { get; set; }

        public int NavbarId { get; set; }
        public List<NavbarListItemViewModel>? Navbar { get; set; }
    }
}
