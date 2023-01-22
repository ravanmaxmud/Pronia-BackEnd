﻿using PrioniaApp.Database.Models.Common;

namespace PrioniaApp.Database.Models
{
    public class Slider : BaseEntity , IAuditable
    {
        public string HeaderTitle { get; set; }
        public string MainTitle { get; set; }
        public string Content { get; set; }
        public string Backgroundİmage { get; set; }

        public string BackgroundİmageInFileSystem { get; set; }
        public string Button { get; set; }
        public string ButtonRedirectUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }

    }
}
