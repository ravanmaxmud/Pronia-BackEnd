using Microsoft.AspNetCore.Mvc;
using PrioniaApp.Areas.Client.ViewModels.Contact;
using PrioniaApp.Database;
using PrioniaApp.Database.Models;
using PrioniaApp.Services.Abstracts;

namespace PrioniaApp.Areas.Client.Controllers
{
    [Area("client")]
    [Route("contact")]
    public class ContactController : Controller
    {
        private readonly DataContext _dbContext;
        private readonly IUserService _userService;

        public ContactController(DataContext dbContext, IUserService userService)
        {
            _dbContext = dbContext;
            _userService = userService;
        }

        [HttpGet("index",Name ="client-contact-index")]
        public async Task<IActionResult> Index()
        {
            return View(new ContactViewModel());
        }


        [HttpPost("index", Name = "client-contact-index")]
        public async Task<IActionResult> Index(ContactViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var contact = new Contact 
            {
               FirstName = model.FirstName,
               LastName = model.LastName,
               PhoneNumber = model.PhoneNumber,
               Email = model.Email,
               Content = model.Content,
               CreatedAt = DateTime.Now,
               UpdateAt =DateTime.Now
               
            };

            await _dbContext.Contacts.AddAsync(contact);
            await _dbContext.SaveChangesAsync();

            return RedirectToRoute("client-home-index");
        }
    }
}
