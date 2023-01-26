using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrioniaApp.Areas.Client.ViewModels.Home;
using PrioniaApp.Contracts.File;
using PrioniaApp.Database;
using PrioniaApp.Services.Abstracts;

namespace PrioniaApp.Areas.Client.ViewCompanents
{
    [ViewComponent(Name = "Product")]
    public class Product : ViewComponent
    {

        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;
        public Product(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string slide)
        {
            if (slide == "NewProduct")
            {
                var newProduct = new IndexViewModel
                {
                    Products = await _dataContext.Products.OrderByDescending(p => p.CreatedAt).Take(4).Select(p => new ProductListItemViewModel(p.Id, p.Name, p.Description, p.Price,p.CreatedAt,
                    p.ProductImages!.Take(1).FirstOrDefault() != null
                    ? _fileService.GetFileUrl(p.ProductImages.Take(1).FirstOrDefault()!.ImageNameInFileSystem, UploadDirectory.Products)
                    : String.Empty,
                       p.ProductImages!.Skip(1).Take(1).FirstOrDefault() != null
                    ? _fileService.GetFileUrl(p.ProductImages.Skip(1).Take(1).FirstOrDefault()!.ImageNameInFileSystem, UploadDirectory.Products)
                    : String.Empty)).ToListAsync()
                };

                return View(newProduct);
            }
            var model = new IndexViewModel
            {
                Products = await _dataContext.Products.Take(7).Select(p => new ProductListItemViewModel(p.Id, p.Name, p.Description, p.Price, p.CreatedAt,
                    p.ProductImages!.Take(1).FirstOrDefault() != null
                    ? _fileService.GetFileUrl(p.ProductImages.Take(1).FirstOrDefault()!.ImageNameInFileSystem, UploadDirectory.Products)
                    : String.Empty,
                       p.ProductImages!.Skip(1).Take(1).FirstOrDefault() != null
                    ? _fileService.GetFileUrl(p.ProductImages.Skip(1).Take(1).FirstOrDefault()!.ImageNameInFileSystem, UploadDirectory.Products)
                    : String.Empty)).ToListAsync()
            };

            return View(model);
        }
    }
}
