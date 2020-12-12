using System;
using System.ComponentModel.DataAnnotations;

namespace Projeto.Models
{
    public class DadosCliente
    {
        public int Iddadoscliente { get; set; }
        public int Idcliente { get; set; }
        public string NomeMae { get; set; }
        public string NomePai { get; set; }
        public string NrRg { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DataEmissaoRg { get; set; }
        public string OrgaoEmissorRg { get; set; }
        public string UfRg { get; set; }
        public string Sexo { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DataNascimento { get; set; }
        public string Naturalidade { get; set; }
        public string EstadoCivil { get; set; }
        public string NomeConjuge { get; set; }
        public string Nucleo { get; set; }
        public int? CodigoLoja { get; set; }
        public string NomeLoja { get; set; }
        public int? CodigoFilial { get; set; }
        public string NomeFilial { get; set; }
        public int? CodigoGrupo { get; set; }
        public string NomeGrupo { get; set; }
        public int? CodigoEmpresa { get; set; }
        public string NomeEmpresa { get; set; }
        public string TipoSeguro { get; set; }
        public string LocalServico { get; set; }
        public string Cargo { get; set; }
        public string CnpjLoja { get; set; }
        public string UfLoja { get; set; }
        public string Repique { get; set; }
        [MaxLength(5)]
        public string TipoCarteira { get; set; }
        public string Agencia { get; set; }
        public string RennerAdm { get; set; }
        public string NomeAluno { get; set; }
        public string RetornoExterna { get; set; }

        [MaxLength(200)]
        public string ProcSubstabelecido { get; set; }

        [MaxLength(1000)]
        public string Informativo { get; set; }

        [MaxLength(1000)]
        public string InformativoEscritorio { get; set; }

        [MaxLength(100)]
        public string Localizador { get; set; }


        [MaxLength(3)]
        public string FlagAltosValores { get; set; }
        [MaxLength(500)]
        public string RecursoAuxiliar { get; set; }
        [MaxLength(3)]
        public string FlagAcaoContra { get; set; }
        [MaxLength(3)]
        public string FlagWo { get; set; }
        public int? NrListaBanco { get; set; }
        public int? NrSinalVital { get; set; }

        public string Plataforma { get; set; }
        public string CodRegiao { get; set; }
        [MaxLength(5)]
        public string Segmento { get; set; }
        [MaxLength(15)]
        public string ClusterRating { get; set; }
        [MaxLength(3)]
        public string EmpresaEncerrada { get; set; }
        [MaxLength(3)]
        public string PorteCliente { get; set; }
        public int? SubRegiaoComercial { get; set; }
        public int? PrioridadeFinal { get; set; }
        [MaxLength(10)]
        public string PropensaoQuebra { get; set; }
        public string RamoAtividade { get; set; }
        public string ContaEncerrada { get; set; }
        public string OfertaDiferenciada { get; set; }
    }

    public class DadosClienteResult : DataResult
    {
        public DadosCliente Object { get; set; }
    }
}