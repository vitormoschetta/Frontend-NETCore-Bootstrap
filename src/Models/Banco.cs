using System.ComponentModel.DataAnnotations;
using Projeto.Validators;

namespace Projeto.Models
{
    public class Banco
    {
        public int Idbanco { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public string NomeSistema { get; set; }
        public string Endereco { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [ValidarCnpj]
        public string Cnpj { get; set; }

        public string Agencia { get; set; }
        public string Cedente { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public decimal? VlrEncargo { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public decimal? VlrEscritorio { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public decimal? VlrBoleto { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public decimal? VlrNotificacao { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public decimal? VlrHonorario { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public decimal? VlrLocalizador { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public decimal? VlrMeta { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public int? DiasNotificacao { get; set; }


        public int? DiasAjuizamento { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public int? DiasCartorio { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public int? NumeroLote { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public int? NumeroBordero { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public int? NumeroNotificacao { get; set; }
        public string Tipo { get; set; }
        public int? DiasAgendamento { get; set; }

        public int? DiasQuebraAcordo { get; set; }

        public bool Ativo { get; set; }
        public string DigitoAgencia { get; set; }
        public string Especie { get; set; }
        public string Carteira { get; set; }
    }

    public class BancoResult : DataResult
    {
        public Banco Object { get; set; }
    }
}