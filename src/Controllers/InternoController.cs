using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projeto.Interfaces;
using Projeto.Models;
using Projeto.Util;

namespace Projeto.Controllers
{
    [Authorize]
    public class InternoController : Controller
    {
        private readonly IUserService _service;
        public InternoController(IUserService service)
        {
            _service = service;
        }
        public IActionResult Index()
        {
            ViewBag.Mensagem = TempData.Get<string>("mensagem");
            return View();
        }

        public IActionResult AccessDenied() => View();


        public async Task<IActionResult> AlterarSenha()
        {
            var user = await _service.GetByName(User.Identity.Name);
            var userUpdatePassword = new UserUpdatePassword();
            userUpdatePassword.Id = user.Id;
            userUpdatePassword.Username = user.Username;

            return View(userUpdatePassword);
        }

        [HttpPost]
        public async Task<IActionResult> AlterarSenha(UserUpdatePassword user)
        {
            if (!ModelState.IsValid) return View(user);

            UserResult result = await _service.UpdatePassword(user);
            if (result.Success == false)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View(user);
            }

            TempDataUtil.Put(TempData, "mensagem", result.Message);
            return RedirectToAction("Index");
        }
    }
}