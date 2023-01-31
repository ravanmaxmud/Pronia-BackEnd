using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrioniaApp.Areas.Admin.ViewModels.Contact;
using PrioniaApp.Database;
using PrioniaApp.Services.Abstracts;

namespace PrioniaApp.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/contact")]
    [Authorize(Roles = "admin")]
    public class ContactController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;

        public ContactController(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }
        [HttpGet("list",Name ="admin-contact-list")]
        public async Task<IActionResult> List()
        {
            var contact =
                await 
                _dataContext.Contacts.Select
                (c => new ListItemViewModel
                (c.Id, c.FirstName, c.LastName, c.PhoneNumber, c.Email, c.Content, c.CreatedAt)).ToListAsync();

            return View(contact);
        }
        [HttpPost("delete/{id}", Name = "admin-contact-delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var contact = await _dataContext.Contacts.FirstOrDefaultAsync(c=> c.Id == id);

            if (contact is null)
            {
                return NotFound();
            }

            _dataContext.Contacts.Remove(contact);

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-contact-list");
        }
    }
}
