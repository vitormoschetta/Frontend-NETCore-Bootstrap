using System.Collections.Generic;
using System.Threading.Tasks;
using Projeto.Interfaces;
using Projeto.Models;

namespace Projeto.Mock
{
    public class ClienteServiceFake : IClienteService
    {
        private readonly ClienteRepositoryFake _repository;
        public ClienteServiceFake(ClienteRepositoryFake repository)
        {
            _repository = repository;
        }
        public async Task<ClienteResult> Gravar(Cliente modelo)
        {
            if (_repository.Exists(modelo))
                return new ClienteResult() { Success = false, Message = "Cliente já cadastrado. ", Object = modelo };

            _repository.Create(modelo);
            return new ClienteResult() { Success = true, Message = "Cadastrado com sucesso. ", Object = modelo };
        }

        public async Task<ClienteResult> Atualizar(int id, Cliente modelo)
        {
            if (_repository.ExistsUpdate(modelo, id))
                return new ClienteResult() { Success = false, Message = "Existe outro Cliente com os mesmos parâmetros. ", Object = modelo };

            _repository.Update(modelo);
            return new ClienteResult() { Success = true, Message = "Atualizado com sucesso. ", Object = modelo };
        }

        public async Task<ClienteResult> Excluir(int id)
        {
            var model = _repository.GetById(id);
            if (model == null)
                return new ClienteResult() { Success = false, Message = "Cliente não existe. ", Object = null };

            _repository.Delete(id);
            return new ClienteResult() { Success = true, Message = "Excluído com sucesso. ", Object = null };
        }

        public async Task<List<Cliente>> BuscarTodos()
        {
            return _repository.GetAll();
        }

        public async Task<Cliente> BuscarPorId(int id)
        {
            return _repository.GetById(id);
        }

        public async Task<List<Cliente>> Filtrar(string filtro)
        {
            return _repository.Search(filtro);
        }
    }
}