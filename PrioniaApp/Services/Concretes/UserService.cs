using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using PrioniaApp.Areas.Client.ViewModels.Authentication;
using PrioniaApp.Contracts.Identity;
using PrioniaApp.Database;
using PrioniaApp.Database.Models;
using PrioniaApp.Exceptions;
using PrioniaApp.Services.Abstracts;
using System.Security.Claims;

namespace PrioniaApp.Services.Concretes
{
    public class UserService : IUserService
    {
        private readonly DataContext _dataContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserActivationService _userActivationService;
        private User _currentUser;

        public UserService(
            DataContext dataContext,
            IHttpContextAccessor httpContextAccessor,
            IUserActivationService userActivationService)
        {
            _dataContext = dataContext;
            _httpContextAccessor = httpContextAccessor;
            _userActivationService = userActivationService;
        }


        public User CurrentUser 
        {
            get
            {
                if (_currentUser is not null)
                {
                    return _currentUser;
                }
                var idClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(u => u.Type == CustomClaimNames.ID);

                if (idClaim is null)
                {
                    throw new IdentityCookieException("Identity cookie not found");
                }
                _currentUser = _dataContext.Users.First(u => u.Id == int.Parse(idClaim.Value));

                return _currentUser;
            }
        }


        public bool IsAuthenticated 
        {
            get => _httpContextAccessor!.HttpContext.User.Identity!.IsAuthenticated;       
        }

        public string GetCurrentUserFullName()
        {
            return $"{CurrentUser.FirstName} {CurrentUser.LastName}";
        }

        public async Task<bool> CheckPasswordAsync(string? email, string? password)
        {
            var model = await _dataContext.Users.FirstOrDefaultAsync(u=> u.Email == email);
            if (model is null || !BCrypt.Net.BCrypt.Verify(password, model.Password))
            {
                return false;
            }
            return true;
        }


        public async Task SignInAsync(int id, string? role = null)
        {
            var claims = new List<Claim>
            {
                new Claim(CustomClaimNames.ID,id.ToString())
            };
            if (role is not null)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var userPrincipal = new ClaimsPrincipal(identity);

            await _httpContextAccessor.HttpContext!.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal);
        }

        public async Task SignInAsync(string? email, string? password, string? role = null)
        {
            var user = await _dataContext.Users.FirstAsync(u => u.Email == email);

            if (user is not null && BCrypt.Net.BCrypt.Verify(password, user.Password) && user.IsActive == true)
            {
                await SignInAsync(user.Id, role);
            }
        }

        public async Task SignOutAsync()
        {
            await _httpContextAccessor.HttpContext!.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public async Task CreateAsync(RegisterViewModel model)
        {
            var user = await CreateUser();

            await _userActivationService.SendActivationUrlAsync(user);


            await _dataContext.SaveChangesAsync();




            async Task<User> CreateUser() 
            {
                var user = new User
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
                    CreatedAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                };
                await _dataContext.Users.AddAsync(user);

                return user;
            }

        }
    }
}
