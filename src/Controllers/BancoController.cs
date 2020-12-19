using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projeto.Interfaces;
using Projeto.Mock;
using Projeto.Models;
using Projeto.Services;
using Projeto.Util;

namespace Projeto.Controllers
{
    [Authorize]
    public class BancoController : Controller
    {
        private readonly IBancoService _service;
        private readonly IBancoPercentualService _serviceBancoPercentual;
        public BancoController(IBancoService service, IBancoPercentualService serviceBancoPercentual)
        {
            _service = service;
            _serviceBancoPercentual = serviceBancoPercentual;
        }

        public async Task<IActionResult> Index(int? pageNumber)
        {
            ViewBag.Mensagem = TempData.Get<string>("mensagem");

            var listaModelo = await _service.BuscarTodos();
            int pageSize = 10;
            PaginatedList<Banco> ModelComPaginacao = PaginatedList<Banco>.Create(listaModelo, pageNumber ?? 1, pageSize);

            ViewBag.ListaNomeBancoComPercentuaisCadastrados = await _serviceBancoPercentual.ListaNomeBancoComPercentuaisCadastrados();
            return View(ModelComPaginacao);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Banco model)
        {
            if (!ModelState.IsValid) return View(model);

            BancoResult result = await _service.Gravar(model);
            if (result.Success == false)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View(model);
            }

            TempDataUtil.Put(TempData, "model", result.Object);
            TempDataUtil.Put(TempData, "mensagem", result.Message);
            return RedirectToAction("Details");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await _service.BuscarPorId(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Banco model)
        {
            if (!ModelState.IsValid) return View(model);

            BancoResult result = await _service.Atualizar(model.Idbanco, model);
            if (result.Success == false)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View(model);
            }

            TempDataUtil.Put(TempData, "mensagem", result.Message);
            return RedirectToAction("Index");
        }


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _service.BuscarPorId(id);
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Banco model)
        {
            if (model == null) return View(model);

            BancoResult result = await _service.Excluir(model.Idbanco);
            if (result.Success == false)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View(model);
            }

            TempDataUtil.Put(TempData, "mensagem", result.Message);
            return RedirectToAction("Index");
        }


        public IActionResult Details()
        {
            var model = TempData.Get<Banco>("model");
            ViewBag.Mensagem = TempData.Get<string>("mensagem");
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> PaginacaoComFiltro(string filtro, int? pageNumber)
        {
            if (filtro == "" || filtro == null) filtro = "empty";
            var listaModelo = await _service.Filtrar(filtro);
            int pageSize = 10;
            PaginatedList<Banco> ModelComPaginacao = PaginatedList<Banco>.Create(listaModelo, pageNumber ?? 1, pageSize);

            ViewBag.ListaNomeBancoComPercentuaisCadastrados = await _serviceBancoPercentual.ListaNomeBancoComPercentuaisCadastrados();
            return PartialView("_TabelaIndex", ModelComPaginacao);
        }

    }
}