using System.Collections.Generic;
using System.Threading.Tasks;
using Projeto.Models;

namespace Projeto.Interfaces
{
    public interface IBancoService
    {
        Task<BancoResult> Gravar(Banco modelo);
        Task<BancoResult> Atualizar(int id, Banco modelo);
        Task<BancoResult> Excluir(int id);
        Task<List<Banco>> BuscarTodos();
        Task<Banco> BuscarPorId(int id);
        Task<List<Banco>> Filtrar(string filtro);

    }
}