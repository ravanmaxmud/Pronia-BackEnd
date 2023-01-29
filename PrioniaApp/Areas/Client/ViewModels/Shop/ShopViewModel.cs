﻿using PrioniaApp.Areas.Client.ViewModels.ShopPage;

namespace PrioniaApp.Areas.Client.ViewModels.Shop
{
    public class ShopViewModel
    {

        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public List<ColorViewModeL> Colors { get; set; }
        public List<SizeViewModeL> Sizes { get; set; }
        public List<CatagoryViewModeL> Catagories { get; set; }
        public List<TagViewModeL> Tags { get; set; }
        public List<ImageViewModeL> Images { get; set; }

        public List<ListItemViewModel> Products { get; set; }




        public class ImageViewModeL
        {
            public ImageViewModeL(string imageUrl)
            {
                ImageUrl = imageUrl;
            }
            public string ImageUrl { get; set; }
        }



        public class CatagoryViewModeL
        {
            public CatagoryViewModeL(string title, int id)
            {
                Title = title;
                Id = id;
            }

            public int Id { get; set; }
            public string Title { get; set; }
        }
        public class TagViewModeL
        {
            public TagViewModeL(string title, int id)
            {
                Title = title;
                Id = id;
            }

            public int Id { get; set; }
            public string Title { get; set; }
        }



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
