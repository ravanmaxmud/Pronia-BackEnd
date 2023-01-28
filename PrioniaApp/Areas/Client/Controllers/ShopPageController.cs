using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> Index(string searchBy, string search)
        {

            var newProduct = new List<ListItemViewModel>();
            if (searchBy == "Name")
            {
                newProduct = await _dataContext.Products.Where(p => p.Name.StartsWith(search) || search == null).Select(p => new ListItemViewModel(p.Id, p.Name, p.Description, p.Price,
                                 p.ProductImages.Take(1).FirstOrDefault() != null
                                 ? _fileService.GetFileUrl(p.ProductImages.Take(1).FirstOrDefault()!.ImageNameInFileSystem, UploadDirectory.Products)
                                 : String.Empty,
                                  p.ProductImages.Skip(1).Take(1).FirstOrDefault() != null
                                 ? _fileService.GetFileUrl(p.ProductImages.Skip(1).Take(1).FirstOrDefault()!.ImageNameInFileSystem, UploadDirectory.Products)
                                 : String.Empty,
                                  p.ProductCatagories.Select(p => p.Catagory).Select(p => new CategoryViewModeL(p.Title, p.Parent.Title)).ToList(),
                                  p.ProductColors.Select(p => p.Color).Select(p => new ColorViewModeL(p.Name)).ToList(),
                                  p.ProductSizes.Select(p => p.Size).Select(p => new SizeViewModeL(p.Title)).ToList(),
                                  p.ProductTags.Select(p => p.Tag).Select(p => new TagViewModel(p.Title)).ToList()
                                  )).ToListAsync();
            }
            else
            {
                newProduct = await _dataContext.Products.Select(p => new ListItemViewModel(p.Id, p.Name, p.Description, p.Price,
                                 p.ProductImages.Take(1).FirstOrDefault() != null
                                 ? _fileService.GetFileUrl(p.ProductImages.Take(1).FirstOrDefault()!.ImageNameInFileSystem, UploadDirectory.Products)
                                 : String.Empty,
                                  p.ProductImages.Skip(1).Take(1).FirstOrDefault() != null
                                 ? _fileService.GetFileUrl(p.ProductImages.Skip(1).Take(1).FirstOrDefault()!.ImageNameInFileSystem, UploadDirectory.Products)
                                 : String.Empty,
                                  p.ProductCatagories.Select(p => p.Catagory).Select(p => new CategoryViewModeL(p.Title, p.Parent.Title)).ToList(),
                                  p.ProductColors.Select(p => p.Color).Select(p => new ColorViewModeL(p.Name)).ToList(),
                                  p.ProductSizes.Select(p => p.Size).Select(p => new SizeViewModeL(p.Title)).ToList(),
                                  p.ProductTags.Select(p => p.Tag).Select(p => new TagViewModel(p.Title)).ToList()
                                  )).ToListAsync();
            }

            return View(newProduct);

        }
    }
}
