﻿using PrioniaApp.Database.Models.Common;

namespace PrioniaApp.Database.Models
{
    public class Basket : BaseEntity , IAuditable
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public DateTime CreatedAt { get ; set ; }
        public DateTime UpdateAt { get; set ; }
        public List<BasketProduct>? BasketProducts { get; set; }
    }
}
