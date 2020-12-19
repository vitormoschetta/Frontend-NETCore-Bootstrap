using System.Collections.Generic;
using System.Threading.Tasks;
using Projeto.Models;

namespace Projeto.Interfaces
{
    public interface IClienteService
    {
        Task<ClienteResult> Gravar(Cliente modelo);
        Task<ClienteResult> Atualizar(int id, Cliente modelo);
        Task<ClienteResult> Excluir(int id);
        Task<List<Cliente>> BuscarTodos();
        Task<Cliente> BuscarPorId(int id);
        Task<List<Cliente>> Filtrar(string filtro);
    }
}