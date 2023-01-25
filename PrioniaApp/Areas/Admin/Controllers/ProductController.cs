using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrioniaApp.Areas.Admin.ViewModels.Product;
using PrioniaApp.Database;
using PrioniaApp.Database.Models;
using PrioniaApp.Services.Abstracts;
using System.Data;
using System.Linq;

namespace PrioniaApp.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/product")]
    [Authorize(Roles = "admin")]
    public class ProductController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(DataContext dataContext, IFileService fileService, ILogger<ProductController> logger)
        {
            _dataContext = dataContext;
            _fileService = fileService;
            _logger = logger;
        }

        #region List
        [HttpGet("list", Name = "admin-product-list")]
        public async Task<IActionResult> List()
        {
            var model = await _dataContext.Products.Select(p => new ListItemViewModel(p.Id, p.Name, p.Rate, p.Description,
                p.Price,
                p.CreatedAt,
                p.ProductCatagories.Select(pc => pc.Catagory).Select(c => new ListItemViewModel.CategoryViewModeL(c.Title, c.Parent.Title)).ToList(),
                p.ProductColors.Select(pc => pc.Color).Select(c => new ListItemViewModel.ColorViewModeL(c.Name)).ToList(),
                p.ProductSizes.Select(ps => ps.Size).Select(s => new ListItemViewModel.SizeViewModeL(s.Title)).ToList(),
                p.ProductTags.Select(ps => ps.Tag).Select(s => new ListItemViewModel.TagViewModel(s.Title)).ToList()
                )).ToListAsync();


            return View(model);
        }
        #endregion


        #region Add
        [HttpGet("add", Name = "admin-product-add")]
        public async Task<IActionResult> Add()
        {
            var model = new AddViewModel
            {
                Categories = await _dataContext.Catagories
                    .Select(c => new CatagoryListItemViewModel(c.Id, c.Title))
                    .ToListAsync(),
                Sizes = await _dataContext.Sizes.Select(s => new SizeListItemViewModel(s.Id, s.Title)).ToListAsync(),
                Colors = await _dataContext.Colors.Select(c => new ColorListItemViewModel(c.Id, c.Name)).ToListAsync(),
                Tags = await _dataContext.Tags.Select(t=> new TagListItemViewModel(t.Id,t.Title)).ToListAsync()
            };

            return View(model);
        }

        [HttpPost("add",Name = "admin-product-add")]
        public async Task<IActionResult> Add(AddViewModel model) 
        {
            if (!ModelState.IsValid)
            {
                return GetView(model);
            }

            foreach (var categoryId in model.CategoryIds)
            {
                if (!await _dataContext.Catagories.AnyAsync(c => c.Id == categoryId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                    _logger.LogWarning($"Category with id({categoryId}) not found in db ");
                    return GetView(model);
                }

            }

            foreach (var sizeId in model.SizeIds)
            {
                if (!await _dataContext.Sizes.AnyAsync(c => c.Id == sizeId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                    _logger.LogWarning($"Size with id({sizeId}) not found in db ");
                    return GetView(model);
                }

            }

            foreach (var colorId in model.ColorIds)
            {
                if (!await _dataContext.Colors.AnyAsync(c => c.Id == colorId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                    _logger.LogWarning($"Color with id({colorId}) not found in db ");
                    return GetView(model);
                }

            }

            foreach (var tagId in model.TagIds)
            {
                if (!await _dataContext.Tags.AnyAsync(c => c.Id == tagId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                    _logger.LogWarning($"Tag with id({tagId}) not found in db ");
                    return GetView(model);
                }

            }



            AddProduct();

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-product-list");



            IActionResult GetView(AddViewModel model)
            {

                model.Categories =  _dataContext.Catagories
                   .Select(c => new CatagoryListItemViewModel(c.Id, c.Title))
                   .ToList();

                model.Sizes =  _dataContext.Sizes
                 .Select(c => new SizeListItemViewModel(c.Id, c.Title))
                 .ToList();

                model.Colors =  _dataContext.Colors
                 .Select(c => new ColorListItemViewModel(c.Id, c.Name))
                 .ToList();

                model.Tags =  _dataContext.Tags
                 .Select(c => new TagListItemViewModel(c.Id, c.Title))
                 .ToList();


                return View(model);
            }


            async void AddProduct() 
            {
                var product = new Product 
                {
                    Name = model.Name,
                    Rate = model.Rate,
                    Description =model.Description,
                    Price = model.Price,
                    CreatedAt = DateTime.Now,
                    UpdateAt = DateTime.Now
                };

                await _dataContext.Products.AddAsync(product);

                foreach (var catagoryId in model.CategoryIds)
                {
                    var productCatagory = new ProductCatagory
                    {
                        CatagoryId = catagoryId,
                        Product = product,
                    };

                    await _dataContext.ProductCatagories.AddAsync(productCatagory);
                }

                foreach (var colorId in model.ColorIds)
                {
                    var productColor = new ProductColor
                    {
                        ColorId = colorId,
                        Product = product,
                    };

                    await _dataContext.ProductColors.AddAsync(productColor);
                }

                foreach (var sizeId in model.SizeIds)
                {
                    var productSize = new ProductSize
                    {
                        SizeId = sizeId,
                        Product = product,
                    };

                    await _dataContext.ProductSizes.AddAsync(productSize);
                }

                foreach (var tagId in model.TagIds)
                {
                    var productTag = new ProductTag
                    {
                        TagId = tagId,
                        Product = product,
                    };

                    await _dataContext.ProductTags.AddAsync(productTag);
                }

             
            }
        }

        #endregion


        #region Update
        [HttpGet("update/{id}", Name = "admin-product-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id) 
        {
            var product = await _dataContext.Products
                .Include(c=> c.ProductCatagories).Include(c=>c.ProductColors).Include(s=> s.ProductSizes).Include(t=> t.ProductTags)
                .FirstOrDefaultAsync(p=> p.Id == id);

            if (product is null)
            {
                return NotFound();
            }

            var model = new UpdateViewModel 
            {
               Id = product.Id,
               Name = product.Name,
               Description = product.Description,
               Price = product.Price,
               Rate = product.Rate,

               Categories = await _dataContext.Catagories.Select(c=> new CatagoryListItemViewModel(c.Id,c.Title)).ToListAsync(),
               CategoryIds = product.ProductCatagories.Select(pc=> pc.CatagoryId).ToList(),

                Sizes = await _dataContext.Sizes.Select(c => new SizeListItemViewModel(c.Id, c.Title)).ToListAsync(),
                SizeIds = product.ProductSizes.Select(pc => pc.SizeId).ToList(),

                Colors = await _dataContext.Colors.Select(c => new ColorListItemViewModel(c.Id, c.Name)).ToListAsync(),
                ColorIds = product.ProductColors.Select(pc => pc.ColorId).ToList(),

                Tags = await _dataContext.Tags.Select(c => new TagListItemViewModel(c.Id, c.Title)).ToListAsync(),
                TagIds = product.ProductTags.Select(pc => pc.TagId).ToList(),

            };

            return View(model);
        
        }

        [HttpPost("update/{id}", Name = "admin-product-update")]
        public async Task<IActionResult> UpdateAsync(UpdateViewModel model) 
        {
            var product = await _dataContext.Products
                    .Include(c => c.ProductCatagories).Include(c => c.ProductColors).Include(s => s.ProductSizes).Include(t => t.ProductTags)
                    .FirstOrDefaultAsync(p => p.Id == model.Id);

            if (product is null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return GetView(model);
            }

            foreach (var categoryId in model.CategoryIds)
            {
                if (!await _dataContext.Catagories.AnyAsync(c => c.Id == categoryId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                    _logger.LogWarning($"Category with id({categoryId}) not found in db ");
                    return GetView(model);
                }

            }

            foreach (var sizeId in model.SizeIds)
            {
                if (!await _dataContext.Sizes.AnyAsync(c => c.Id == sizeId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                    _logger.LogWarning($"Size with id({sizeId}) not found in db ");
                    return GetView(model);
                }

            }

            foreach (var colorId in model.ColorIds)
            {
                if (!await _dataContext.Colors.AnyAsync(c => c.Id == colorId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                    _logger.LogWarning($"Color with id({colorId}) not found in db ");
                    return GetView(model);
                }

            }

            foreach (var tagId in model.TagIds)
            {
                if (!await _dataContext.Tags.AnyAsync(c => c.Id == tagId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                    _logger.LogWarning($"Tag with id({tagId}) not found in db ");
                    return GetView(model);
                }

            }


            UpdateProductAsync();

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-product-list");


            #region GetviewAndUpdate
            IActionResult GetView(UpdateViewModel model)
            {

                model.Categories = _dataContext.Catagories
                   .Select(c => new CatagoryListItemViewModel(c.Id, c.Title))
                   .ToList();

                model.CategoryIds = product.ProductCatagories.Select(c => c.CatagoryId).ToList();
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                model.Sizes = _dataContext.Sizes
                 .Select(c => new SizeListItemViewModel(c.Id, c.Title))
                 .ToList();

                model.SizeIds = product.ProductSizes.Select(c => c.SizeId).ToList();
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                model.Colors = _dataContext.Colors
                 .Select(c => new ColorListItemViewModel(c.Id, c.Name))
                 .ToList();

                model.ColorIds = product.ProductColors.Select(c => c.ColorId).ToList();

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                model.Tags = _dataContext.Tags
                 .Select(c => new TagListItemViewModel(c.Id, c.Title))
                 .ToList();

                model.TagIds = product.ProductTags.Select(c => c.TagId).ToList();

                return View(model);
            }
            async Task UpdateProductAsync() 
            {
                product.Name = model.Name;
                product.Description = model.Description;
                product.Price = model.Price;
                product.Rate = model.Rate;
                product.UpdateAt = DateTime.Now;

                #region Catagory
                var categoriesInDb = product.ProductCatagories.Select(bc => bc.CatagoryId).ToList();
                var categoriesToRemove = categoriesInDb.Except(model.CategoryIds).ToList();
                var categoriesToAdd = model.CategoryIds.Except(categoriesInDb).ToList();

                product.ProductCatagories.RemoveAll(bc => categoriesToRemove.Contains(bc.CatagoryId));

                foreach (var categoryId in categoriesToAdd)
                {
                    var productCatagory = new ProductCatagory
                    {
                        CatagoryId = categoryId,
                        Product = product,
                    };

                   await _dataContext.ProductCatagories.AddAsync(productCatagory);
                }
                #endregion

                #region Color
                var colorInDb = product.ProductColors.Select(bc => bc.ColorId).ToList();
                var colorToRemove = colorInDb.Except(model.ColorIds).ToList();
                var colorToAdd = model.ColorIds.Except(colorInDb).ToList();

                product.ProductColors.RemoveAll(bc => colorToRemove.Contains(bc.ColorId));


                foreach (var colorId in colorToAdd)
                {
                    var productColor = new ProductColor
                    {
                        ColorId = colorId,
                        Product = product,
                    };

                    await _dataContext.ProductColors.AddAsync(productColor);
                }
                #endregion


                #region Size
                var sizeInDb = product.ProductSizes.Select(bc => bc.SizeId).ToList();
                var sizeToRemove = sizeInDb.Except(model.SizeIds).ToList();
                var sizeToAdd = model.SizeIds.Except(sizeInDb).ToList();

                product.ProductSizes.RemoveAll(bc => sizeToRemove.Contains(bc.SizeId));


                foreach (var sizeId in sizeToAdd)
                {
                    var productSize = new ProductSize
                    {
                         SizeId= sizeId,
                        Product = product,
                    };

                    await _dataContext.ProductSizes.AddAsync(productSize);
                }

                #endregion

                #region Tag
                var tagInDb = product.ProductTags.Select(bc => bc.TagId).ToList();
                var tagToRemove = tagInDb.Except(model.TagIds).ToList();
                var tagToAdd = model.TagIds.Except(tagInDb).ToList();

                product.ProductTags.RemoveAll(bc => tagToRemove.Contains(bc.TagId));


                foreach (var tagId in tagToAdd)
                {
                    var productTag = new ProductTag
                    {
                        TagId = tagId,
                        Product = product,
                    };

                    await _dataContext.ProductTags.AddAsync(productTag);
                }
                #endregion
            }
            #endregion
        }
        #endregion


        #region Delete
        [HttpPost("delete/{id}", Name = "admin-product-delete")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id) 
        {
            var products = await _dataContext.Products.FirstOrDefaultAsync(p=> p.Id == id);

            if (products is null)
            {
                return NotFound();
            }
             
            _dataContext.Products.Remove(products);
            await _dataContext.SaveChangesAsync();
            return RedirectToRoute("admin-product-list");
        }


        #endregion
    }
}
