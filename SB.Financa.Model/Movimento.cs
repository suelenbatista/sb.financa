using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SB.Financa.Model
{
    public class Movimento
    {
        public int Id { get; set; }
        public DateTime Cadastro { get; set; }
        public DateTime Vencimento { get; set; }
        public TipoMovimento Tipo { get; set; }
        public StatusMovimento Status { get; set; }
        public Etiqueta Etiqueta { get; set; }
        public Pessoa Pessoa { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public decimal ValorPago { get; set; }
        public decimal Saldo { get => Valor - ValorPago; }
        public int EtiquetaId { get; set; }
        public int PessoaId { get; set; }
        public ContaCartao ContaCartao { get; set;  }
        public IList<MovimentoBaixa> MovimentoBaixa { get; set; }
    }


    public class MovimentoView
    {
        public int Id { get; set; }
        public DateTime Cadastro { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Data de Vencimento")]
        public DateTime Vencimento { get; set; }

        [Required]
        [Display(Name = "Tipo de Movimento")]
        public string Tipo { get; set; }

        [Required]
        [Display(Name = "Status do Movimento")]
        public string Status { get; set; }

        [Required]
        [Display(Name = "Descrição")]
        [StringLength(250)]
        public string Descricao { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Valor do Movimento")]
        public decimal Valor { get; set; }
        public decimal ValorPago { get; set; }
        public decimal Saldo { get => Valor - ValorPago; }
        [Required]
        public int EtiquetaId { get; set; }
        [Required]
        public int PessoaId { get; set; }
        public int ? ContaCartaoId { get; set; }
        public IList<MovimentoBaixa> MovimentoBaixa { get; set; }
    }
}
