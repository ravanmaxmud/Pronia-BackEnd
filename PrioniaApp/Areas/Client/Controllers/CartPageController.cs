using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrioniaApp.Areas.Client.ViewCompanents;
using PrioniaApp.Areas.Client.ViewModels.Basket;
using PrioniaApp.Areas.Client.ViewModels.Home;
using PrioniaApp.Contracts.File;
using PrioniaApp.Database;
using PrioniaApp.Services.Abstracts;
using PrioniaApp.Services.Concretes;
using System.Text.Json;

namespace PrioniaApp.Areas.Client.Controllers
{
    [Area("client")]
    [Route("CartPage")]
    public class CartPageController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IUserService _userService;
        private readonly IFileService _fileService;
        private readonly IBasketService _basketService;

        public CartPageController(DataContext dbContext, IUserService userService, IBasketService basketService)
        {
            _dataContext = dbContext;
            _userService = userService;
            _basketService = basketService;
        }

        [HttpGet("cartpageindex", Name = "client-cart-page-index")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet("add/{id}", Name = "client-cartpagebasket-add")]
        public async Task<IActionResult> AddProduct([FromRoute] int id)
        {
            var product = await _dataContext.Products.Include(p => p.ProductImages).FirstOrDefaultAsync(p => p.Id == id);
            if (product is null)
            {
                return NotFound();
            }
            var productCookiViewModel = await _basketService.AddBasketProductAsync(product);
            if (productCookiViewModel.Any())
            {
                return ViewComponent(nameof(CartPage), productCookiViewModel);
            }

            return ViewComponent(nameof(CartPage));
        }


        [HttpGet("update", Name = "client-cartpagebasket-update")]
        public async Task<IActionResult> UpdateProduct()
        {
            return ViewComponent(nameof(MiniBasket));
        }


        [HttpGet("cart-page-delete/{id}", Name = "client-shop-basket-delete")]
        public async Task<IActionResult> DeleteBaketProductAsync([FromRoute] int id)
        {
            if (_userService.IsAuthenticated)
            {

                var basketProduct = await _dataContext.BasketProducts
                        .FirstOrDefaultAsync(bp => bp.Basket.UserId == _userService.CurrentUser.Id && bp.ProductId == id);

                if (basketProduct is null) return NotFound();

                _dataContext.BasketProducts.Remove(basketProduct);
            }
            else
            {

                var product = await _dataContext.Products.FirstOrDefaultAsync(b => b.Id == id);
                if (product is null)
                {
                    return NotFound();
                }

                var productCookieValue = HttpContext.Request.Cookies["products"];
                if (productCookieValue is null)
                {
                    return NotFound();
                }

                var productsCookieViewModel = JsonSerializer.Deserialize<List<BasketCookieViewModel>>(productCookieValue);
                productsCookieViewModel!.RemoveAll(pcvm => pcvm.Id == id);

                HttpContext.Response.Cookies.Append("products", JsonSerializer.Serialize(productsCookieViewModel));
            }


            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("client-cart-page-index");
        }
    }
}
