using System.Collections.Generic;
using System.Threading.Tasks;
using Projeto.Models;

namespace Projeto.Interfaces
{
    public interface IDadosClienteService
    {
        Task<DadosClienteResult> Gravar(DadosCliente modelo);
        Task<DadosCliente> ProcurarDadosCliente(int clienteId);
        Task<DadosClienteResult> Atualizar(DadosCliente modelo);
        Task<List<int>> ListaIdClienteComDadosCadastrados();
    }
}