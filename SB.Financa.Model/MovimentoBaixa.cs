using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SB.Financa.Model
{
    public class MovimentoBaixa
    {
        public int Id { get; set; }

        public DateTime DataEvento { get; set; }
        public DateTime DataBaixa { get; set; }

        public DirecaoBaixa Direcao { get; set; }

        public decimal ValorBaixa { get; set; }
        
        public Movimento Movimento { get; set; }
        
        public ContaBancaria ContaBancaria { get; set; }

        public int MovimentoId { get; set; }

        public int ContaBancariaId { get; set; }
    }

    public class MovimentoBaixaView
    {
        public int Id { get; set; }

        public DateTime DataEvento { get; set; }

        [Required]
        public DateTime DataBaixa { get; set; }

        public string Direcao { get; set; }

        [Required]
        public decimal ValorBaixa { get; set; }

        [Required]
        public int MovimentoId { get; set; }

        [Required]
        public int ContaBancariaId { get; set; }
    }

}
