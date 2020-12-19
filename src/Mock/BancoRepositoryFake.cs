using System.Collections.Generic;
using System.Linq;
using Projeto.Models;

namespace Projeto.Mock
{
    public class BancoRepositoryFake
    {
        public readonly List<Banco> List;
        public BancoRepositoryFake()
        {
            List = new List<Banco>();

            for (var i = 1; i < 20; i++)
            {
                var cnpj = string.Empty;

                if (i > 9)
                    cnpj = $"000000000000{i}";
                else
                    cnpj = $"0000000000000{i}";

                if (i == 1)
                    cnpj = "35832357000110";

                var banco = new Banco
                {
                    Idbanco = i,
                    Codigo = $"0000{i}",
                    Nome = $"BANCO {i}",
                    NomeSistema = $"BANCO {i}",
                    Cnpj = cnpj,
                    Ativo = true,
                    VlrEncargo = i,
                    VlrEscritorio = i,
                    VlrBoleto = i,
                    VlrNotificacao = i,
                    VlrHonorario = i,
                    VlrLocalizador = i,
                    VlrMeta = i,
                    DiasNotificacao = i,
                    DiasCartorio = i,
                    NumeroLote = i,
                    NumeroBordero = i,
                    NumeroNotificacao = i
                };

                List.Add(banco);
            }

        }

        public List<Banco> GetAll()
        {
            return List.OrderBy(x => x.Idbanco).ToList();
        }

        public Banco GetById(int id)
        {
            return List.FirstOrDefault(x => x.Idbanco == id);
        }

        public List<Banco> Search(string param)
        {
            if (param == "empty" || param == null)
                param = "";

            param = param.ToUpper();

            var lista = List
                .Where(x => x.Nome.Contains(param) || x.NomeSistema.Contains(param) || x.Cnpj.Contains(param))
                .OrderBy(x => x.Idbanco).ToList();

            return lista;
        }

        public bool Exists(Banco model)
        {
            var item = List.FirstOrDefault(x => x.Nome == model.Nome || x.NomeSistema == model.NomeSistema || x.Cnpj == model.Cnpj);
            if (item != null)
                return true;

            return false;
        }

        public bool ExistsUpdate(Banco model, int id)
        {
            var item = List
                .FirstOrDefault(x =>
                    (x.Codigo == model.Codigo || x.Nome == model.Nome || x.NomeSistema == model.NomeSistema || x.Cnpj == model.Cnpj)
                    && x.Idbanco != id);

            if (item != null)
                return true;

            return false;

        }


        public void Create(Banco model)
        {
            model.Idbanco = List.Count + 1;
            List.Add(model);
        }

        public void Update(Banco model)
        {
            var item = List.FirstOrDefault(x => x.Idbanco == model.Idbanco);
            if (item != null)
            {
                List.Remove(item);
                List.Add(model);
            }
        }

        public void Delete(int id)
        {
            var item = List.FirstOrDefault(x => x.Idbanco == id);
            if (item != null)
                List.Remove(item);
        }



    }
}