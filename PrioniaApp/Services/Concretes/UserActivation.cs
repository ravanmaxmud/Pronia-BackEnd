using PrioniaApp.Database.Models;
using PrioniaApp.Services.Abstracts;

namespace PrioniaApp.Services.Concretes
{
    public class UserActivation : IUserActivationService
    {
        public Task SendActivationUrlAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
