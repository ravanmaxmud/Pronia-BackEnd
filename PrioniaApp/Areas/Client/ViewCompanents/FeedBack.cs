using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrioniaApp.Areas.Client.ViewModels.Home;
using PrioniaApp.Contracts.File;
using PrioniaApp.Database;
using PrioniaApp.Services.Abstracts;

namespace PrioniaApp.Areas.Client.ViewCompanents
{
    [ViewComponent(Name = "FeedBack")]
    public class FeedBack : ViewComponent
    {

        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;

        public FeedBack(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new IndexViewModel
            {
                FeedBacks = await _dataContext.FeedBacks.Select
                (f=> new FeedBackViewModel
                (f.Id,f.User.FirstName,f.User.LastName,
                f.User.Roles.Name,f.Content, 
                _fileService.GetFileUrl(f.ImageNameInFileSystem, UploadDirectory.Feedback))).ToListAsync()
            };


            return View(model);
        }
    }
}
