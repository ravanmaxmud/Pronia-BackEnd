using PrioniaApp.Areas.Client.ViewModels.Authentication;
using PrioniaApp.Contracts.Identity;
using PrioniaApp.Database;
using PrioniaApp.Database.Models;
using PrioniaApp.Exceptions;
using PrioniaApp.Services.Abstracts;

namespace PrioniaApp.Services.Concretes
{
    public class UserService : IUserService
    {
        private readonly DataContext _dataContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailService _emailService;
        private User _currentUser;

        public UserService(DataContext dataContext, IHttpContextAccessor httpContextAccessor, IEmailService emailService, User currentUser)
        {
            _dataContext = dataContext;
            _httpContextAccessor = httpContextAccessor;
            _emailService = emailService;
            _currentUser = currentUser;
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
            get => _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;       
        }

        public string GetCurrentUserFullName()
        {
            return $"{CurrentUser.FirstName} {CurrentUser.LastName}";
        }

        public Task<bool> CheckPasswordAsync(string? email, string? password)
        {
            throw new NotImplementedException();
        }


        public Task SignInAsync(int id, string? role = null)
        {
            throw new NotImplementedException();
        }

        public Task SignInAsync(string? email, string? password, string? role = null)
        {
            throw new NotImplementedException();
        }

        public Task SignOutAsync()
        {
            throw new NotImplementedException();
        }

        public async Task CreateAsync(RegisterViewModel model)
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

        }
    }
}
