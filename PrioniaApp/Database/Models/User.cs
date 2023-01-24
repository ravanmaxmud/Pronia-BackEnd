﻿using NuGet.ContentModel;
using PrioniaApp.Database.Models.Common;
using System.Data;

namespace PrioniaApp.Database.Models
{
    public class User : BaseEntity ,IAuditable
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public int? RoleId { get; set; }
        public Role? Roles { get; set; }
        public UserActivation? UserActivation { get; set; }
    }
}
