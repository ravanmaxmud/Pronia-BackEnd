using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrioniaApp.Areas.Admin.ViewModels.ProductImage;
using PrioniaApp.Database;
using PrioniaApp.Services.Abstracts;

namespace PrioniaApp.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/productimg")]
    public class ProductImageController : Controller
    {
        private readonly DataContext _dataContext;
        
        private readonly IFileService _fileService;

        public ProductImageController(DataContext dataContext,IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }
        [HttpGet("{productId}/image/list",Name = "admin-productimg-list")]
        public async Task<IActionResult> List([FromRoute] int productId)
        {
            var product = await _dataContext.Products.FirstOrDefaultAsync(p=> p.Id == productId);
            if (product == null) return NotFound();

            var model = new ProductImagesViewModel { ProductId = product.Id };

            model.Images = product.ProductImages.Select(p=> new ProductImagesViewModel.ListItem 
            {
               Id = p.Id,
               ImageUrl = _fileService.GetFileUrl(p.ImageNameInFileSystem,Contracts.File.UploadDirectory.Products),
               CreatedAt = p.CreatedAt
            }).ToList();

            return View(model);
        }
    }
}
