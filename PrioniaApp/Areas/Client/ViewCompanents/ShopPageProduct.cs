using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrioniaApp.Contracts.File;
using PrioniaApp.Database;
using PrioniaApp.Areas.Client.ViewModels.ShopPage;
using PrioniaApp.Database.Models;
using PrioniaApp.Services.Abstracts;
using static PrioniaApp.Areas.Client.ViewModels.ShopPage.ListItemViewModel;

namespace PrioniaApp.Areas.Client.ViewCompanents
{

    [ViewComponent(Name = "ShopPageProduct")]
    public class ShopPageProduct : ViewComponent
    {

        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;
        public ShopPageProduct(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string? searchBy = null,
            string? Search = null, int? MinPrice = null,
            int? MaxPrice = null, [FromQuery] int? categoryId = null, [FromQuery] int? colorId = null, [FromQuery] int? tagId = null)
        {

            var productsQuery = _dataContext.Products.AsQueryable();

            if (searchBy == "Name")
            {
                productsQuery = productsQuery.Where(p => p.Name.StartsWith(Search) || Convert.ToString(p.Price).StartsWith(Search) || Search == null);
            }
            else if (MinPrice is not null && MaxPrice is not null)
            {
                productsQuery = productsQuery.Where(p => p.Price >= MinPrice && p.Price <= MaxPrice);
            }
            else if (categoryId is not null || colorId is not null || tagId is not null)
            {
                productsQuery = productsQuery.Include(p => p.ProductCatagories)
                    .Include(p => p.ProductColors)
                    .Include(p => p.ProductTags)
                    .Where(p => categoryId == null || p.ProductCatagories!.Any(pc => pc.CatagoryId == categoryId))
                .Where(p => colorId == null || p.ProductColors!.Any(pc => pc.ColorId == colorId))
                    .Where(p => tagId == null || p.ProductTags!.Any(pt => pt.TagId == tagId));

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
