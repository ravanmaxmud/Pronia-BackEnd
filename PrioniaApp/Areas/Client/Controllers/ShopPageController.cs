﻿using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Index(string searchBy, string search, [FromQuery] int? categoryId, [FromQuery] int? colorId, [FromQuery] int? tagId)
        {
            var productsQuery = _dataContext.Products.AsQueryable();

            if (searchBy == "Name")
            {
                productsQuery = productsQuery.Where(p => p.Name.StartsWith(search) || Convert.ToString(p.Price).StartsWith(search) || search == null);
            }
            else if(categoryId is not null || colorId is not null)
            {
                productsQuery = productsQuery.Include(p => p.ProductCatagories)
                    .Include(p => p.ProductColors)
                    .Include(p => p.ProductTags)
                    .Where(p => categoryId == null || p.ProductCatagories!.Any(pc => pc.CatagoryId == categoryId))
                    .Where(p => colorId == null || p.ProductColors!.Any(pc => pc.ColorId == colorId))
                    .Where(p => tagId == null || p.ProductTags!.Any(pt=> pt.TagId == tagId));
                    
            }
            else
            {
                productsQuery = productsQuery.OrderBy(p => p.Price);
            }

              var newProduct = await productsQuery.Select(p => new ListItemViewModel(p.Id, p.Name, p.Description, p.Price,
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

            return View(newProduct);

        }
    }
}
