using System.Collections.Generic;
using System.Threading.Tasks;
using Projeto.Interfaces;
using Projeto.Models;

namespace Projeto.Mock
{
    public class BancoPercentualServiceFake : IBancoPercentualService
    {
        private readonly BancoPercentualRepositoryFake _repository;
        public BancoPercentualServiceFake(BancoPercentualRepositoryFake repository)
        {
            _repository = repository;
        }
        public async Task<BancoPercentualResult> Gravar(BancoPercentual modelo)
        {
            if (_repository.Exists(modelo))
                return new BancoPercentualResult()
                { Success = false, Message = "Esse banco já possui percentuais cadastrados! ", Object = modelo };

            _repository.Create(modelo);
            return new BancoPercentualResult()
            { Success = true, Message = "Percentuais cadastrados!", Object = modelo };
        }

        public async Task<BancoPercentualResult> Atualizar(BancoPercentual modelo)
        {
            _repository.Update(modelo);
            return new BancoPercentualResult()
            { Success = true, Message = "Percentuais atualizados!", Object = modelo };
        }

        public async Task<BancoPercentual> ProcurarPercentualBanco(string nomeBanco)
        {
            return _repository.GetByName(nomeBanco);
        }

        public async Task<List<string>> ListaNomeBancoComPercentuaisCadastrados()
        {
            return _repository.ListaNomeBancoComPercentuaisCadastrados();
        }

    }
}