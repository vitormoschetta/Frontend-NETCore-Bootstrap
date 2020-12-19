using System.Collections.Generic;
using System.Linq;
using Projeto.Models;

namespace Projeto.Mock
{
    public class DadosClienteRepositoryFake
    {
        public readonly List<DadosCliente> List;
        public DadosClienteRepositoryFake()
        {
            List = new List<DadosCliente>();
        }

        public void Create(DadosCliente model)
        {
            model.Iddadoscliente = List.Count + 1;
            List.Add(model);
        }

        public void Update(DadosCliente model)
        {
            var item = List.FirstOrDefault(x => x.Iddadoscliente == model.Iddadoscliente);
            if (item != null)
            {
                List.Remove(item);
                List.Add(model);
            }
        }

        public bool Exists(DadosCliente model)
        {
            var item = List.FirstOrDefault(x => x.Iddadoscliente == model.Iddadoscliente);
            if (item != null)
                return true;

            return false;
        }

        public DadosCliente GetById(int clienteId)
        {
            return List.FirstOrDefault(x => x.Idcliente == clienteId);
        }

        public List<int> ListaIdClienteComDadosCadastrados()
        {
            return List.Select(x => x.Idcliente).ToList();
        }
    }
}