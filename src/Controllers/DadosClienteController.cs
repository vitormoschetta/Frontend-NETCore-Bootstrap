using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Projeto.Interfaces;
using Projeto.Models;
using Projeto.Util;

namespace Projeto.Controllers
{
    public class DadosClienteController : Controller
    {
        private readonly IDadosClienteService _service;
        public DadosClienteController(IDadosClienteService service)
        {
            _service = service;
        }

        public async Task<IActionResult> CreateOrUpdate(int Idcliente, string nome)
        {
            var dadosCliente = await _service.ProcurarDadosCliente(Idcliente);
            if (dadosCliente == null)
            {
                dadosCliente = new DadosCliente();
                dadosCliente.Idcliente = Idcliente;
                ViewBag.Nome = nome;
            }

            return View(dadosCliente);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrUpdate(DadosCliente model)
        {
            if (!ModelState.IsValid) return View(model);

            var result = new DadosClienteResult();

            if (model.Iddadoscliente != 0)
                result = await _service.Atualizar(model);
            else
                result = await _service.Gravar(model);

            if (result.Success == false)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View(model);
            }

            TempDataUtil.Put(TempData, "mensagem", result.Message);
            return RedirectToAction("Index", "Cliente");
        }

    }
}