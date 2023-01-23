using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrioniaApp.Areas.Client.ViewModels.Home;
using PrioniaApp.Contracts.File;
using PrioniaApp.Database;
using PrioniaApp.Migrations;
using PrioniaApp.Services.Abstracts;

namespace PrioniaApp.Areas.Client.ViewCompanents
{
    [ViewComponent(Name = "PaymentBenefit")]
    public class PaymentBenefit : ViewComponent
    {

        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;

        public PaymentBenefit(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new IndexViewModel
            {
                PaymentBenefits = await _dataContext.PaymentBenefits.Select(p => new PaymentBenefitsViewModel
                (p.Id, p.Title, p.Content, _fileService.GetFileUrl
                (p.BackgroundİmageInFileSystem, UploadDirectory.PaymentBenefits))).ToListAsync()
            };



            return View(model);
        }
    }
}
