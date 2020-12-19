using System.Collections.Generic;
using System.Threading.Tasks;
using Projeto.Interfaces;
using Projeto.Models;

namespace Projeto.Mock
{
    public class DadosClienteServiceFake : IDadosClienteService
    {
        private readonly DadosClienteRepositoryFake _repository;
        public DadosClienteServiceFake(DadosClienteRepositoryFake repository)
        {
            _repository = repository;
        }

        public async Task<DadosClienteResult> Gravar(DadosCliente modelo)
        {
            if (_repository.Exists(modelo))
                return new DadosClienteResult()
                { Success = false, Message = "Esse cliente já possui dados cadastrados! ", Object = modelo };

            _repository.Create(modelo);
            return new DadosClienteResult()
            { Success = true, Message = "Dados do cliente cadastrados!", Object = modelo };
        }

        public async Task<DadosClienteResult> Atualizar(DadosCliente modelo)
        {
            _repository.Update(modelo);
            return new DadosClienteResult()
            { Success = true, Message = "Dados do cliente atualizados!", Object = modelo };
        }

        public async Task<DadosCliente> ProcurarDadosCliente(int clienteId)
        {
            return _repository.GetById(clienteId);
        }

        public async Task<List<int>> ListaIdClienteComDadosCadastrados()
        {
            return _repository.ListaIdClienteComDadosCadastrados();
        }
    }
}