using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrioniaApp.Areas.Client.ViewModels.Home;
using PrioniaApp.Contracts.File;
using PrioniaApp.Database;
using PrioniaApp.Services.Abstracts;

namespace PrioniaApp.Areas.Client.Controllers
{
    [Area("client")]
    [Route("home")]
    public class HomeController : Controller
    {
        private readonly DataContext _dbContext;
        private readonly IFileService _fileService;
        public HomeController(DataContext dbContext,IFileService fileService)
        {
            _dbContext = dbContext;
            _fileService = fileService;
        }

        [HttpGet("~/", Name = "client-home-index")]
        [HttpGet("home-index")]
        public async Task<IActionResult> Index()
        {
            var model = new IndexViewModel
            {
                Sliders = await _dbContext.Sliders.Select(s => new SliderViewModel(s.Id, s.HeaderTitle, s.MainTitle, s.Content, s.Button, s.ButtonRedirectUrl,
                _fileService.GetFileUrl(s.BackgroundİmageInFileSystem, UploadDirectory.Slider))).ToListAsync(),

                PaymentBenefits = await _dbContext.PaymentBenefits.Select
                (p=> new PaymentBenefitsViewModel
                (p.Id,p.Title,p.Content,_fileService.GetFileUrl(p.BackgroundİmageInFileSystem,UploadDirectory.PaymentBenefits))).ToListAsync()
            };

            return View(model);
        }
    }
}
