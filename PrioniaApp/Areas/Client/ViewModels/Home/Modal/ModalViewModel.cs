namespace PrioniaApp.Areas.Client.ViewModels.Home.Modal
{
    public class ModalViewModel
    {
        public ModalViewModel(int id, string title, string description, int price, string imgUrl, List<ColorViewModeL> colors, List<SizeViewModeL> sizes)
        {
            Id = id;
            Title = title;
            Description = description;
            Price = price;
            ImgUrl = imgUrl;
            Colors = colors;
            Sizes = sizes;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string ImgUrl { get; set; }
        public List<ColorViewModeL> Colors { get; set; }
        public List<SizeViewModeL> Sizes { get; set; }








        public class SizeViewModeL
        {
            public SizeViewModeL(string title, int id)
            {
                Title = title;
                Id = id;
            }

            public int Id { get; set; }
            public string Title { get; set; }
        }
        public class ColorViewModeL
        {
            public ColorViewModeL(string name, int id)
            {
                Name = name;
                Id = id;
            }
            public int Id { get; set; }
            public string Name { get; set; }
        }

    }
}
