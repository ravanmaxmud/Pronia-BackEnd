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

        public async Task<IViewComponentResult> InvokeAsync(string? slide=null)
        {
            var productsQuery = _dataContext.Products.AsQueryable();
            if (slide == "NewProduct")
            {
                productsQuery = productsQuery.OrderByDescending(p => p.CreatedAt).Take(4);
            }
            else if (slide =="BestProducts")
            {
                var productsBestQuery = 
                    await _dataContext.OrderProducts
                    .Include(p => p.Product)
                    .GroupBy(p => p.ProductId)
                    .OrderByDescending(p => p.Count()).Take(7).Select(p => p.Key)
                    .ToListAsync();

                productsQuery = productsQuery.Where(p=> productsBestQuery.Contains(p.Id));
            }
            else
            {
                productsQuery = productsQuery.OrderBy(p => p.Price);
            }



            var model = await productsQuery.Take(7).Select(p => new ProductListItemViewModel(p.Id, p.Name, p.Description, p.Price, p.CreatedAt,
                 p.ProductImages!.Take(1).FirstOrDefault() != null
                 ? _fileService.GetFileUrl(p.ProductImages.Take(1).FirstOrDefault()!.ImageNameInFileSystem, UploadDirectory.Products)
                 : String.Empty,
                    p.ProductImages!.Skip(1).Take(1).FirstOrDefault() != null
                 ? _fileService.GetFileUrl(p.ProductImages.Skip(1).Take(1).FirstOrDefault()!.ImageNameInFileSystem, UploadDirectory.Products)
                 : String.Empty)).ToListAsync();
            

            return View(model);
        }
    }
}
