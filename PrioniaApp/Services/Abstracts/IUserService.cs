﻿using PrioniaApp.Areas.Client.ViewModels.Authentication;
using PrioniaApp.Database.Models;

namespace PrioniaApp.Services.Abstracts
{
    public interface IUserService
    {
        public bool IsAuthenticated { get; }
        public User CurrentUser { get; }

        Task<bool> CheckPasswordAsync(string? email, string? password);
        string GetCurrentUserFullName();
        Task SignInAsync(int id, string? role = null);
        Task SignInAsync(string? email, string? password, string? role = null);
        Task CreateAsync(RegisterViewModel model);
        Task SignOutAsync();
    }
}
