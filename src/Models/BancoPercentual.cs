using System.ComponentModel.DataAnnotations;

namespace Projeto.Models
{
    public class BancoPercentual
    {
        public int Idbancopercentual { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public string NomeBanco { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public string Fase { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public string Situacao { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public decimal? MinimoSobrePrincipal { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public decimal? MaximoSobrePrincipal { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public decimal? MinimoJuros { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public decimal? MaximoJuros { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public decimal? MinimoMulta { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public decimal? MaximoMulta { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public decimal? MinimoHonorario { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public decimal? MaximoHonorario { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public decimal? Comissao { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public int NrVezes { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public bool Ativo { get; set; } = true;
    }

    public class BancoPercentualResult : DataResult
    {
        public BancoPercentual Object { get; set; }
    }
}