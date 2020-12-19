using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Projeto.Interfaces;
using Projeto.Models;
using Projeto.Services;
using Projeto.Util;

namespace Projeto.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _service;
        public HomeController(IUserService service)
        {
            _service = service;
        }

        public IActionResult Index() => View();

        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(UserRegister model)
        {
            if (!ModelState.IsValid) return View(model);

            UserResult result = await _service.Register(model);
            if (result.Success == false)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View(model);
            }

            User user = result.Object;
            TempDataUtil.Put(TempData, "model", model);
            return RedirectToAction("ConfirmedRegister", new { message = result.Message });
        }

        public IActionResult ConfirmedRegister() => View();


        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(User model)
        {
            if (!ModelState.IsValid) return View(model);

            UserResult result = await _service.Login(model);
            if (result.Success == false)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View(model);
            }

            RegistrarCookies(result);

            return RedirectToAction("Index", "Interno");
        }

        public async void RegistrarCookies(UserResult result)
        {
            //cria coockie
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, result.Object.Username),
                new Claim(ClaimTypes.Role, result.Object.Role),
                new Claim("Token", result.Token),
            };

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(20), // <-- tempo expiracao cookie
                IsPersistent = false,       // <-- Se o cookie permanece após fechar browser ou nao
                IssuedUtc = DateTime.Now,   // <-- Data/hora de persistencia do cookie                                
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }



        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
