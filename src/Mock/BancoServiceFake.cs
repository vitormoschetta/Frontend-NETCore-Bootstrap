using System.Collections.Generic;
using System.Threading.Tasks;
using Projeto.Interfaces;
using Projeto.Models;

namespace Projeto.Mock
{
    public class BancoServiceFake : IBancoService
    {
        private readonly BancoRepositoryFake _repository;
        public BancoServiceFake(BancoRepositoryFake repository)
        {
            _repository = repository;
        }
        public async Task<BancoResult> Gravar(Banco modelo)
        {
            if (_repository.Exists(modelo))
                return new BancoResult() { Success = false, Message = "Banco já cadastrado. ", Object = modelo };

            _repository.Create(modelo);
            return new BancoResult() { Success = true, Message = "Cadastrado com sucesso. ", Object = modelo };
        }

        public async Task<BancoResult> Atualizar(int id, Banco modelo)
        {
            if (_repository.ExistsUpdate(modelo, id))
                return new BancoResult() { Success = false, Message = "Existe outro banco com os mesmos parâmetros. ", Object = modelo };

            _repository.Update(modelo);
            return new BancoResult() { Success = true, Message = "Atualizado com sucesso. ", Object = modelo };
        }

        public async Task<BancoResult> Excluir(int id)
        {
            var model = _repository.GetById(id);
            if (model == null)
                return new BancoResult() { Success = false, Message = "Banco não existe. ", Object = null };

            _repository.Delete(id);
            return new BancoResult() { Success = true, Message = "Excluído com sucesso. ", Object = null };
        }

        public async Task<List<Banco>> BuscarTodos()
        {
            return _repository.GetAll();
        }

        public async Task<Banco> BuscarPorId(int id)
        {
            return _repository.GetById(id);
        }

        public async Task<List<Banco>> Filtrar(string filtro)
        {
            return _repository.Search(filtro);
        }

    }
}