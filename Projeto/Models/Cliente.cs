using System.ComponentModel.DataAnnotations;
using Projeto.Validators;

namespace Projeto.Models
{
    public class Cliente
    {
        public int Idcliente { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [ValidarCpfCnpj]
        public string CpfCnpj { get; set; }
        public string TipoPessoa { get; set; }
        public bool Ativo { get; set; }
    }


    public class ClienteResult : DataResult
    {
        public Cliente Object { get; set; }
    }
}