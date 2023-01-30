using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrioniaApp.Areas.Client.ViewCompanents;
using PrioniaApp.Areas.Client.ViewModels.ShopPage;
using PrioniaApp.Contracts.File;
using PrioniaApp.Database;
using PrioniaApp.Services.Abstracts;
using System.Linq;
using static PrioniaApp.Areas.Client.ViewModels.ShopPage.ListItemViewModel;

namespace PrioniaApp.Areas.Client.Controllers
{
    [Area("client")]
    [Route("shoppage")]
    public class ShopPageController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IBasketService _basketService;
        private readonly IUserService _userService;
        private readonly IFileService _fileService;


        public ShopPageController(DataContext dataContext, IBasketService basketService, IUserService userService, IFileService fileService)
        {
            _dataContext = dataContext;
            _basketService = basketService;
            _userService = userService;
            _fileService = fileService;
        }


        [HttpGet("index", Name = "client-shoppage-index")]
        public async Task<IActionResult> Index()
        {
            return View();
        }


        [HttpGet("Filter", Name = "client-shoppage-filter")]
        public async Task<IActionResult> Filter(string? searchBy=null,
            string? Search = null,  int? MinPrice=null, 
            int? MaxPrice = null, [FromQuery] int? categoryId=null, [FromQuery] int? colorId=null, [FromQuery] int? tagId=null)
        {

            return ViewComponent(nameof(ShopPageProduct) , new { searchBy = searchBy , Search = Search, MinPrice = MinPrice, MaxPrice = MaxPrice, categoryId = categoryId , colorId = colorId , tagId = tagId });

        }
    }
}
