using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projeto.Interfaces;
using Projeto.Models;
using Projeto.Util;

namespace Projeto.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly IUserService _service;
        public UserController(IUserService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index(int? pageNumber)
        {
            ViewBag.Mensagem = TempData.Get<string>("mensagem");

            var lista = await _service.GetInactivesFirstAccess();
            var itensPorPagina = 5;
            PaginatedList<User> listaPaginada = PaginatedList<User>.Create(lista, pageNumber ?? 1, itensPorPagina);
            return View(listaPaginada);
        }

        public async Task<IActionResult> LiberarAcesso(int? pageNumber)
        {
            ViewBag.Mensagem = TempData.Get<string>("mensagem");

            var lista = await _service.GetInactivesFirstAccess();
            var itensPorPagina = 5;
            PaginatedList<User> listaPaginada = PaginatedList<User>.Create(lista, pageNumber ?? 1, itensPorPagina);
            return View(listaPaginada);
        }

        public async Task<IActionResult> LiberarAcessoConfirmar(int id)
        {
            UserResult result = await _service.ActivateFirstAccess(id);
            if (result.Success == false)
            {
                TempDataUtil.Put(TempData, "mensagem", result.Message);
                return RedirectToAction("LiberarAcesso");
            }

            TempDataUtil.Put(TempData, "mensagem", result.Message);
            return RedirectToAction("LiberarAcesso");
        }

        public async Task<IActionResult> ExcluirPedidoAcesso(int id)
        {
            UserResult result = result = await _service.Delete(id);
            if (result.Success == false)
            {
                TempDataUtil.Put(TempData, "mensagem", result.Message);
                return RedirectToAction("LiberarAcesso");
            }

            TempDataUtil.Put(TempData, "mensagem", result.Message);
            return RedirectToAction("LiberarAcesso");
        }

        public async Task<IActionResult> ExcluirUsuario(int id)
        {
            var user = await _service.GetById(id);
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> ExcluirUsuario(User model)
        {
            UserResult result = result = await _service.Delete(model.Id);
            if (result.Success == false)
            {
                TempDataUtil.Put(TempData, "mensagem", result.Message);
                return RedirectToAction("LiberarAcesso");
            }

            TempDataUtil.Put(TempData, "mensagem", result.Message);
            return RedirectToAction("ListaUsuarios");
        }


        public async Task<IActionResult> ListaUsuarios(int? pageNumber)
        {
            var lista = await _service.GetAll();

            var itensPorPagina = 5;
            PaginatedList<User> listaPaginada = PaginatedList<User>.Create(lista, pageNumber ?? 1, itensPorPagina);

            ViewBag.Mensagem = TempData.Get<string>("mensagem");

            return View(listaPaginada);
        }

        public async Task<IActionResult> EditarAcessoUsuario(int id)
        {
            var user = await _service.GetById(id);
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> EditarAcessoUsuario(User model)
        {
            UserResult result = await _service.UpdateRoleActive(model);
            if (result.Success == false)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View(model);
            }

            TempDataUtil.Put(TempData, "mensagem", result.Message);
            return RedirectToAction("ListaUsuarios");
        }


        public IActionResult AdicionarUsuario() => View();

        [HttpPost]
        public async Task<IActionResult> AdicionarUsuario(UserRegister model)
        {
            if (!ModelState.IsValid) return View(model);

            UserResult result = await _service.RegisterAdmin(model);
            if (result.Success == false)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View(model);
            }

            TempDataUtil.Put(TempData, "mensagem", result.Message);
            return RedirectToAction("ListaUsuarios");
        }


        public async Task<IActionResult> PaginationUser(int? pageNumber)
        {
            if (pageNumber == null) pageNumber = 1;
            var lista = await _service.GetAll();
            var itensPorPagina = 5;
            PaginatedList<User> listaPaginada = PaginatedList<User>.Create(lista, pageNumber ?? 1, itensPorPagina);
            return PartialView("_TabelaUsuarios", listaPaginada);
        }


        public async Task<IActionResult> SearchUser(int? pageNumber, string param)
        {
            if (param == null) param = "";
            var lista = await _service.Search(param);
            var itensPorPagina = 5;
            PaginatedList<User> listaPaginada = PaginatedList<User>.Create(lista, pageNumber ?? 1, itensPorPagina);
            return PartialView("_TabelaUsuarios", listaPaginada);
        }
    }
}