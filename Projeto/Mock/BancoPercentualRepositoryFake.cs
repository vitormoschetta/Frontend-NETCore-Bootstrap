using System.Collections.Generic;
using System.Linq;
using Projeto.Models;

namespace Projeto.Mock
{
    public class BancoPercentualRepositoryFake
    {
        public readonly List<BancoPercentual> List;
        public BancoPercentualRepositoryFake()
        {
            List = new List<BancoPercentual>();
        }

        public void Create(BancoPercentual model)
        {
            model.Idbancopercentual = List.Count + 1;
            List.Add(model);
        }

        public void Update(BancoPercentual model)
        {
            var item = List.FirstOrDefault(x => x.NomeBanco == model.NomeBanco);
            if (item != null)
            {
                List.Remove(item);
                List.Add(model);
            }
        }

        public bool Exists(BancoPercentual model)
        {
            var item = List.FirstOrDefault(x => x.NomeBanco == model.NomeBanco);
            if (item != null)
                return true;

            return false;
        }

        public BancoPercentual GetByName(string nomeBanco)
        {
            return List.FirstOrDefault(x => x.NomeBanco == nomeBanco);
        }

        public List<string> ListaNomeBancoComPercentuaisCadastrados()
        {
            return List.Select(x => x.NomeBanco).ToList();
        }
    }
}