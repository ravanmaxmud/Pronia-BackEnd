using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrioniaApp.Areas.Client.ViewModels.Home;
using PrioniaApp.Areas.Client.ViewModels.ShopPage;
using PrioniaApp.Contracts.File;
using PrioniaApp.Database;
using PrioniaApp.Services.Abstracts;
using static PrioniaApp.Areas.Client.ViewModels.ShopPage.ListItemViewModel;

namespace PrioniaApp.Areas.Client.ViewCompanents
{
    [ViewComponent(Name = "ShopPage")]
    public class ShopPage : ViewComponent
    {

        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;
        public ShopPage(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string searchBy , string search)
        {
            var newProduct = new List<ListItemViewModel>();
            if (searchBy == "Description")
            {
                newProduct = await _dataContext.Products.Where(p=> p.Description == search || search == null).Select(p => new ListItemViewModel(p.Id, p.Name, p.Description, p.Price,
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
                newProduct = await _dataContext.Products.Where(p => p.Name.StartsWith(search)|| search == null).Select(p => new ListItemViewModel(p.Id, p.Name, p.Description, p.Price,
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
