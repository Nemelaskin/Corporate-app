using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Corporate_app.Models;
using Corporate_app.Models.Context;
using Corporate_app.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Corporate_app.Controllers
{
    public class AccountController : Controller
    {
        private ModelsContext context;
        public AccountController(ModelsContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid) {
                User user = await context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
                if (user != null) {
                    if (BCrypt.Net.BCrypt.Verify(model.Password, user.Password)){
                        await Authenticate(user.UserId + " " + user.Name + " " + user.SurName);
                        return RedirectToAction("Index", "Home");
                    }
                    
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegistrationModel model)
        {
            if (ModelState.IsValid) {
                User user = await context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
                if (user == null) {
                    string passwordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);
                    context.Users.Add(new User { Email = model.Email, Password = passwordHash, Name = model.Name, SurName = model.Surname, Phone = model.Phone, RoleId=1, PositionId= 1});
                    await context.SaveChangesAsync();

                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Некорректные данные");
            }
            return View(model);
        }
        private async Task Authenticate(string userName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
