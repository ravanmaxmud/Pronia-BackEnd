﻿using PrioniaApp.Database.Models.Common;

namespace PrioniaApp.Database.Models
{
    public class BlogAndBlogCategory : BaseEntity
    {
        public int BlogId { get; set; }
        public Blog Blog { get; set; }

        public int BlogCategoryId { get; set; }
        public BlogCategory Category { get; set; }
    }
}
