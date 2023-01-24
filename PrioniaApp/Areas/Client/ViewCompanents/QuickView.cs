using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrioniaApp.Areas.Client.ViewModels.Home;
using PrioniaApp.Contracts.File;
using PrioniaApp.Database;
using PrioniaApp.Services.Abstracts;

namespace PrioniaApp.Areas.Client.ViewCompanents
{
    [ViewComponent(Name = "QuickView")]
    public class QuickView : ViewComponent
    {

        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;
        public QuickView(DataContext dataContext,IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new IndexViewModel
            {
                Products = await _dataContext.Products.Select(p => new ProductListItemViewModel(p.Id, p.Name, p.Description, p.Price,
                p.ProductImages!.Take(1).FirstOrDefault() != null
                ? _fileService.GetFileUrl(p.ProductImages.Take(1).FirstOrDefault()!.ImageNameInFileSystem, UploadDirectory.Products)
                : String.Empty,
                   p.ProductImages!.Skip(1).Take(1).FirstOrDefault() != null
                ? _fileService.GetFileUrl(p.ProductImages.Skip(1).Take(1).FirstOrDefault()!.ImageNameInFileSystem, UploadDirectory.Products)
                : String.Empty,
                p.ProductCatagories!.Select(pc => pc.Catagory).Select(c => new ProductListItemViewModel.CategoryViewModeL(c.Title, c.Parent.Title)).ToList(),
                p.ProductColors!.Select(pc => pc.Color).Select(c => new ProductListItemViewModel.ColorViewModeL(c.Name)).ToList(),
                p.ProductSizes!.Select(ps => ps.Size).Select(s => new ProductListItemViewModel.SizeViewModeL(s.Title)).ToList(),
                p.ProductTags!.Select(ps => ps.Tag).Select(s => new ProductListItemViewModel.TagViewModel(s.Title)).ToList()))
                 .ToListAsync(),
            };

            return View(model);
        }
    }
}
