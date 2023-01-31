using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrioniaApp.Areas.Client.ViewModels.Reward;
using PrioniaApp.Database;
using PrioniaApp.Services.Abstracts;

namespace PrioniaApp.Areas.Client.ViewCompanents
{
    [ViewComponent(Name = "Reward")]
    public class Reward : ViewComponent
    {

        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;
        public Reward(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var reward = await _dataContext.Rewards
                .Select(r =>
                new ListItemViewModel
                (r.Id, _fileService.GetFileUrl(r.ImageNameInFileSystem, Contracts.File.UploadDirectory.Reward))).ToListAsync();

            return View(reward);
        }
    }

}
