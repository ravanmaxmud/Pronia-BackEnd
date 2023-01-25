﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrioniaApp.Areas.Client.ActionFilter;
using PrioniaApp.Areas.Client.ViewModels.Authentication;
using PrioniaApp.Contracts.Identity;
using PrioniaApp.Database;
using PrioniaApp.Services.Abstracts;

namespace PrioniaApp.Areas.Client.Controllers
{
    [Area("client")]
    [Route("authentication")]
    public class AuthenticationController : Controller
    {
        private readonly DataContext _dbContext;
        private readonly IUserService _userService;

        public AuthenticationController(DataContext dbContext, IUserService userService)
        {
            _dbContext = dbContext;
            _userService = userService;
        }


        [HttpGet("login",Name ="client-auth-login")]
        public async Task<IActionResult> Login()
        {
            if (_userService.IsAuthenticated)
            {
                return RedirectToRoute("client-home-index");
            }

            return View(new LoginViewModel());
        }

        [HttpPost("login", Name = "client-auth-login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (!await _userService.CheckPasswordAsync(model!.Email, model!.Password))
            {
                ModelState.AddModelError(String.Empty, "Email or password is not correct");
                return View(model);
            }
            if (await _dbContext.Users.AnyAsync(u=> u.Email == model.Email && u.Roles.Name == RoleNames.ADMIN))
            {
                await _userService.SignInAsync(model!.Email, model!.Password, RoleNames.ADMIN);
                return RedirectToRoute("admin-auth-login");
            }

            await _userService.SignInAsync(model!.Email, model!.Password);

            return RedirectToRoute("client-home-index");
        }



        [HttpGet("register", Name = "client-auth-register")]
        [ServiceFilter(typeof(ValidationCurrentUserAttribute))]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost("register", Name = "client-auth-register")]
        public async Task<IActionResult> Register(RegisterViewModel model)  
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var emails = new List<string>();
            emails.Add(model.Email);

            await _userService.CreateAsync(model);

            return RedirectToRoute("client-home-index");
        }

        [HttpGet("logout", Name = "client-auth-logout")]
        public async Task<IActionResult> Logout() 
        {
            await _userService.SignOutAsync();

            return RedirectToRoute("client-home-index");
        }



        [HttpGet("activate/{token}", Name = "client-auth-activate")]
        public async Task<IActionResult> Activate([FromRoute] string token)
        {
            var userActivation = await _dbContext.UserActivations.Include(u => u.User)
                .FirstOrDefaultAsync(u => !u.User!.IsActive && u.ActivationToken == token);

            if (userActivation is null)
            { 
            return NotFound();
            }  

            if (DateTime.Now > userActivation.ExpiredDate)
            {
                return Ok("Token expired olub teessufler");
            }

            userActivation.User.IsActive = true;

            await _dbContext.SaveChangesAsync();

            return RedirectToRoute("client-auth-login");
        }
    }
}
