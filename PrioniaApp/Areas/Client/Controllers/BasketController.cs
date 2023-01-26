using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrioniaApp.Areas.Client.ViewCompanents;
using PrioniaApp.Areas.Client.ViewModels.Basket;
using PrioniaApp.Database;
using PrioniaApp.Services.Abstracts;
using System.Text.Json;

namespace PrioniaApp.Areas.Client.Controllers
{
    [Area("client")]
    [Route("basket")]
    public class BasketController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IBasketService _basketService;
        private readonly IUserService _userService;
        public BasketController(DataContext dataContext, IBasketService basketService, IUserService userService)
        {
            _dataContext = dataContext;
            _basketService = basketService;
            _userService = userService;
        }

        [HttpGet("add/{id}",Name ="client-basket-add")]
        public async Task<IActionResult> AddProduct([FromRoute] int id)
        {
            var product = await _dataContext.Products.Include(p => p.ProductImages).FirstOrDefaultAsync(p=> p.Id == id);
            if (product is null) 
            {
                return NotFound();
            }
            var productCookiViewModel = await _basketService.AddBasketProductAsync(product);
            if (productCookiViewModel.Any())
            {
                return ViewComponent(nameof(MiniBasket), productCookiViewModel);
            }
            return ViewComponent(nameof(MiniBasket));
        }

        [HttpGet("basket-delete/{id}", Name = "client-basket-delete")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id) 
        {
            if (_userService.IsAuthenticated) 
            {
                var basketProduct = await _dataContext.BasketProducts
                    .FirstOrDefaultAsync(bp=> bp.Basket.UserId == _userService.CurrentUser.Id && bp.ProductId==id);

                if (basketProduct is null)
                {
                    return NotFound();
                }
                _dataContext.BasketProducts.Remove(basketProduct);
            }
         
                var product = await _dataContext.Products.FirstOrDefaultAsync(p => p.Id == id);
                if (product is null)
                {
                    return NotFound();
                }
                var productCookieValue = HttpContext.Request.Cookies["products"];
                if (productCookieValue is null)
                {
                    return NotFound();
                }

                var productCookieViewModel = JsonSerializer.Deserialize<List<BasketCookieViewModel>>(productCookieValue);

                productCookieViewModel!.RemoveAll(pcvm => pcvm.Id == id);
                HttpContext.Response.Cookies.Append("products",JsonSerializer.Serialize(productCookieViewModel));
          
            await _dataContext.SaveChangesAsync();
            return ViewComponent(nameof(MiniBasket),productCookieViewModel);
        }
    }
}
