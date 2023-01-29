using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrioniaApp.Areas.Client.ViewModels.ShopPage;
using PrioniaApp.Contracts.File;
using PrioniaApp.Database;
using PrioniaApp.Services.Abstracts;
using static PrioniaApp.Areas.Client.ViewModels.ShopPage.ListItemViewModel;

namespace PrioniaApp.Areas.Client.ViewCompanents
{
    [ViewComponent(Name = "ShopPageCatagory")]
    public class ShopPageCatagory : ViewComponent
    {

        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;
        public ShopPageCatagory(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _dataContext.Catagories.Select(c => new CategoryListItemViewModel(c.Id, c.Title)).ToListAsync();
            
             return View(model);
        }
    }

}
