﻿using PrioniaApp.Database.Models.Common;

namespace PrioniaApp.Database.Models
{
    public class Navbar : BaseEntity , IAuditable
    {
        public string Name { get; set; }
        public string ToURL { get; set; }
        public int Order { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get ; set; }

        public List<SubNavbar> SubNavbars { get; set; }
    }
}
