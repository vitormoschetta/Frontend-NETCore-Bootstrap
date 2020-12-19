using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Projeto.Interfaces;
using Projeto.Models;
using Projeto.Util;

namespace Projeto.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IClienteService _service;
        private readonly IDadosClienteService _serviceDadosCliente;
        public ClienteController(IClienteService service, IDadosClienteService serviceDadosCliente)
        {
            _service = service;
            _serviceDadosCliente = serviceDadosCliente;
        }

        public async Task<IActionResult> Index(int? pageNumber)
        {
            ViewBag.Mensagem = TempData.Get<string>("mensagem");

            var listaModelo = await _service.BuscarTodos();
            int pageSize = 10;
            PaginatedList<Cliente> ModelComPaginacao = PaginatedList<Cliente>.Create(listaModelo, pageNumber ?? 1, pageSize);

            ViewBag.ListaIdClienteComDadosCadastrados = await _serviceDadosCliente.ListaIdClienteComDadosCadastrados();
            return View(ModelComPaginacao);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Cliente model)
        {
            if (!ModelState.IsValid) return View(model);

            ClienteResult result = await _service.Gravar(model);
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
        public async Task<IActionResult> Edit(Cliente model)
        {
            if (!ModelState.IsValid) return View(model);

            ClienteResult result = await _service.Atualizar(model.Idcliente, model);
            if (result.Success == false)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View(model);
            }

            TempDataUtil.Put(TempData, "mensagem", result.Message);
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Delete(int id)
        {
            var model = await _service.BuscarPorId(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Cliente model)
        {
            if (model == null) return View(model);

            ClienteResult result = await _service.Excluir(model.Idcliente);
            if (result.Success == false)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View(model);
            }

            TempDataUtil.Put(TempData, "mensagem", result.Message);
            return RedirectToAction("Index");
        }


        public IActionResult Details(string message)
        {
            var model = TempData.Get<Cliente>("model");
            ViewBag.Mensagem = TempData.Get<string>("mensagem");
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> PaginacaoComFiltro(string filtro, int? pageNumber)
        {
            if (filtro == "" || filtro == null) filtro = "empty";
            var listaModelo = await _service.Filtrar(filtro);
            int pageSize = 10;
            PaginatedList<Cliente> ModelComPaginacao = PaginatedList<Cliente>.Create(listaModelo, pageNumber ?? 1, pageSize);

            ViewBag.ListaIdClienteComDadosCadastrados = await _serviceDadosCliente.ListaIdClienteComDadosCadastrados();
            return PartialView("_TabelaIndex", ModelComPaginacao);
        }
    }
}