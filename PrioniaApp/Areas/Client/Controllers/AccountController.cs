using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrioniaApp.Areas.Client.ViewModels.Account;
using PrioniaApp.Database;
using PrioniaApp.Services.Abstracts;

namespace PrioniaApp.Areas.Client.Controllers
{
    [Area("client")]
    [Route("account")]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IUserService _userService;

        public AccountController(DataContext dataContext, IUserService userService)
        {
            _dataContext = dataContext;
            _userService = userService;
        }

        [HttpGet("dashboard",Name ="client-account-dashboard")]
        public IActionResult DashBoard()
        {
            return View();
        }

        [HttpGet("order", Name = "client-account-order")]
        public async  Task<IActionResult> Order()
        {
          var model = await _dataContext.Orders.Where(o=> o.UserId == _userService.CurrentUser.Id)
                .Select(b => new OrderViewModel(b.Id, b.CreatedAt, b.Status, b.SumTotalPrice))
                .ToListAsync();




            return View(model);
        }

        [HttpGet("address", Name = "client-account-address")]
        public IActionResult Address()
        {
            return View();
        }

        [HttpGet("details", Name = "client-account-details")]
        public IActionResult Details()
        {
            return View();
        }
    }
}
