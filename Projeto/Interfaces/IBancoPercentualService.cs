using System.Collections.Generic;
using System.Threading.Tasks;
using Projeto.Models;

namespace Projeto.Interfaces
{
    public interface IBancoPercentualService
    {
        Task<BancoPercentualResult> Gravar(BancoPercentual modelo);
        Task<BancoPercentual> ProcurarPercentualBanco(string nomeBanco);
        Task<BancoPercentualResult> Atualizar(BancoPercentual modelo);
        Task<List<string>> ListaNomeBancoComPercentuaisCadastrados();


    }
}