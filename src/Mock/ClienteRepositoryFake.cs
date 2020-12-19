using System.Collections.Generic;
using System.Linq;
using Projeto.Models;

namespace Projeto.Mock
{
    public class ClienteRepositoryFake
    {
        public readonly List<Cliente> List;
        public ClienteRepositoryFake()
        {
            List = new List<Cliente>();

            for (var i = 1; i < 20; i++)
            {
                var cnpj = string.Empty;
                if (i > 9)
                    cnpj = $"000000000{i}";
                else
                    cnpj = $"0000000000{i}";

                var Cliente = new Cliente
                {
                    Idcliente = i,
                    Nome = $"CLIENTE {i}",
                    CpfCnpj = cnpj,
                    TipoPessoa = "Pessoa Fisica",
                    Ativo = true
                };

                List.Add(Cliente);
            }

        }

        public List<Cliente> GetAll()
        {
            return List.OrderBy(x => x.Idcliente).ToList();
        }

        public Cliente GetById(int id)
        {
            return List.FirstOrDefault(x => x.Idcliente == id);
        }

        public List<Cliente> Search(string param)
        {
            if (param == "empty" || param == null)
                param = "";

            param = param.ToUpper();

            var lista = List
                .Where(x => x.Nome.Contains(param) || x.Nome.Contains(param) || x.CpfCnpj.Contains(param))
                .OrderBy(x => x.Idcliente).ToList();

            return lista;
        }

        public bool Exists(Cliente model)
        {
            var item = List.FirstOrDefault(x => x.Nome == model.Nome || x.CpfCnpj == model.CpfCnpj);
            if (item != null)
                return true;

            return false;
        }

        public bool ExistsUpdate(Cliente model, int id)
        {
            var item = List
                .FirstOrDefault(x =>
                    (x.Nome == model.Nome || x.CpfCnpj == model.CpfCnpj) && x.Idcliente != id);

            if (item != null)
                return true;

            return false;
        }


        public void Create(Cliente model)
        {
            model.Idcliente = List.Count + 1;
            List.Add(model);
        }

        public void Update(Cliente model)
        {
            var item = List.FirstOrDefault(x => x.Idcliente == model.Idcliente);
            if (item != null)
            {
                List.Remove(item);
                List.Add(model);
            }
        }

        public void Delete(int id)
        {
            var item = List.FirstOrDefault(x => x.Idcliente == id);
            if (item != null)
                List.Remove(item);
        }

    }
}