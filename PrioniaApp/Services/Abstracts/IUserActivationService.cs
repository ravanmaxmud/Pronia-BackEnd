using PrioniaApp.Database.Models;

namespace PrioniaApp.Services.Abstracts
{
    public interface IUserActivationService
    {
        Task SendActivationUrlAsync(User user);
    }
}
