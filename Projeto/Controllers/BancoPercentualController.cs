using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Projeto.Interfaces;
using Projeto.Models;
using Projeto.Util;

namespace Projeto.Controllers
{
    public class BancoPercentualController : Controller
    {
        private readonly IBancoPercentualService _service;
        public BancoPercentualController(IBancoPercentualService service)
        {
            _service = service;
        }

        public async Task<IActionResult> CreateOrUpdate(string nomeBanco)
        {
            var bancoPercentual = await _service.ProcurarPercentualBanco(nomeBanco);
            if (bancoPercentual == null)
            {
                bancoPercentual = new BancoPercentual();
                bancoPercentual.NomeBanco = nomeBanco;
            }

            return View(bancoPercentual);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrUpdate(BancoPercentual model)
        {
            if (!ModelState.IsValid) return View(model);

            var result = new BancoPercentualResult();

            if (model.Idbancopercentual != 0)
                result = await _service.Atualizar(model);
            else
                result = await _service.Gravar(model);

            if (result.Success == false)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View(model);
            }

            TempDataUtil.Put(TempData, "mensagem", result.Message);
            return RedirectToAction("Index", "Banco");
        }

    }
}