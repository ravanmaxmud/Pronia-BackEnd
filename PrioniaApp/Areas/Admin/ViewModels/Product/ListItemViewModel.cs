namespace PrioniaApp.Areas.Admin.ViewModels.Product
{
    public class ListItemViewModel
    {

        public ListItemViewModel(int id, string name, int? rate, string description,
            int price, DateTime createdAt, List<CategoryViewModeL> categories, List<ColorViewModeL> colors, List<SizeViewModeL> sizes, List<TagViewModel> tags)
        {
            Id = id;
            Name = name;
            Rate = rate;
            Description = description;
            Price = price;
            CreatedAt = createdAt;
            Categories = categories;
            Colors = colors;
            Sizes = sizes;
            Tags = tags;
        }

        public int Id { get; set; }
        public string Name { get; set; }
  
        public int? Rate { get; set; }
     
        public string Description { get; set; }

        public int Price { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<CategoryViewModeL> Categories { get; set; }
        public List<ColorViewModeL> Colors { get; set; }
        public List<SizeViewModeL> Sizes { get; set; }
        public List<TagViewModel> Tags { get; set; }



        ///
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// 

        public class CategoryViewModeL
        {
            public CategoryViewModeL(string title, string parentTitle)
            {
                Title = title;
                ParentTitle = parentTitle;
            }

            public string Title { get; set; }
            public string ParentTitle { get; set; }


        }
        public class SizeViewModeL
        {
            public SizeViewModeL(string title)
            {
                Title = title;
            }

            public string Title { get; set; }
        }
        public class ColorViewModeL
        {
            public ColorViewModeL(string name)
            {
                Name = name;
            }

            public string Name { get; set; }
        }
        public class TagViewModel
        {
            public TagViewModel(string title)
            {
                Title = title;
            }

            public string Title { get; set; }
        }

    }
}
