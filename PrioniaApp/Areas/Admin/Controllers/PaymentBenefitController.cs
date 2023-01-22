using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrioniaApp.Areas.Admin.ViewModels.PaymentBenefits;
using PrioniaApp.Contracts.File;
using PrioniaApp.Database;
using PrioniaApp.Database.Models;
using PrioniaApp.Services.Abstracts;

namespace PrioniaApp.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/payment")]
    public class PaymentBenefitController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;

        public PaymentBenefitController(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }
        #region List
        [HttpGet("list",Name = "admin-payment-list")]
        public async Task<IActionResult> List()
        {
            var model = await _dataContext.PaymentBenefits.Select
                (p => new ListItemViewModel
                (p.Id, p.Title, p.Content,
                _fileService.GetFileUrl(p.BackgroundİmageInFileSystem, UploadDirectory.PaymentBenefits),
                p.CreatedAt, p.UpdateAt))
                .ToListAsync();

            return View(model);
        }
        #endregion

        #region add
        [HttpGet("add", Name = "admin-payment-add")]
        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost("add", Name = "admin-payment-add")]
        public async Task<IActionResult> Add(AddViewModel model)
        {

            if (ModelState.IsValid) return View(model);

            var imageNameInSystem = await _fileService.UploadAsync(model.Backgroundİmage, UploadDirectory.PaymentBenefits);

            AddPaymnetBenefit(model.Backgroundİmage.FileName, imageNameInSystem);

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-payment-list");


            async void AddPaymnetBenefit(string imageName, string imageNameInSystem)
            {
                var paymentBenefit = new PaymentBenefit
                {
                   Title = model.Title, 
                   Content = model.Content,
                   Backgroundİmage = imageName,
                   BackgroundİmageInFileSystem = imageNameInSystem,
                   CreatedAt = DateTime.Now,
                   UpdateAt = DateTime.Now
                };

                await _dataContext.PaymentBenefits.AddAsync(paymentBenefit);
            }
        }
        #endregion

        #region update
        [HttpGet("update/{id}", Name = "admin-payment-update")]
        public async Task<IActionResult> Update([FromRoute] int id)
        {
            var paymentBenefit = await _dataContext.PaymentBenefits.FirstOrDefaultAsync(s => s.Id == id);
            if (paymentBenefit is null)
            {
                return NotFound();
            }
            var model = new UpdateViewModel
            {
                Id = paymentBenefit.Id,
                Title = paymentBenefit.Title, 
                Content = paymentBenefit.Content,
                BackgroundİmageInFileSystem = _fileService.GetFileUrl(paymentBenefit.BackgroundİmageInFileSystem, UploadDirectory.PaymentBenefits),
            };

            return View(model);
        }

        [HttpPost("update/{id}", Name = "admin-payment-update")]
        public async Task<IActionResult> Update(UpdateViewModel model)
        {

            var paymentBenefit = await _dataContext.PaymentBenefits.FirstOrDefaultAsync(s => s.Id == model.Id);
            if (paymentBenefit is null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _fileService.DeleteAsync(model.Backgroundİmage.FileName, UploadDirectory.PaymentBenefits);

            var backGroundImageFileSystem = await _fileService.UploadAsync(model.Backgroundİmage, UploadDirectory.PaymentBenefits);


            await UpdatePaymentBenefit(model.Backgroundİmage.FileName, backGroundImageFileSystem);

            return RedirectToRoute("admin-payment-list");


            async Task UpdatePaymentBenefit(string imageName, string imageNameInSystem)
            {
                paymentBenefit.Title = model.Title;
                paymentBenefit.Content = model.Content;
                paymentBenefit.Backgroundİmage = imageName;
                paymentBenefit.BackgroundİmageInFileSystem = imageNameInSystem;
                paymentBenefit.UpdateAt = DateTime.Now;

                await _dataContext.SaveChangesAsync();
            }
        }

        #endregion


        [HttpPost("delete/{id}", Name = "admin-payment-delete")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var paymentBenefit = await _dataContext.PaymentBenefits.FirstOrDefaultAsync(s => s.Id == id);
            if (paymentBenefit is null)
            {
                return NotFound();
            }
            await _fileService.DeleteAsync(paymentBenefit.BackgroundİmageInFileSystem, UploadDirectory.PaymentBenefits);
            _dataContext.PaymentBenefits.Remove(paymentBenefit);
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-payment-list");
        }
    }
}
