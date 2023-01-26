using PrioniaApp.Areas.Client.ViewModels.Basket;
using PrioniaApp.Database.Models;

namespace PrioniaApp.Services.Abstracts
{
    public interface IBasketService
    {
        Task<List<BasketCookieViewModel>> AddBasketProductAsync(Product product);
    }
}
