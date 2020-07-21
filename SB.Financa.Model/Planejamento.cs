using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SB.Financa.Model
{
    public class Planejamento
    {
        public int Id { get; set; }
        public string Meta { get; set; }
        public DateTime DataInicial { get; set; }
        public DateTime DataFinal { get; set; }
        public TipoPlanejamento Tipo { get; set; }
        public int EtiquetaId { get; set; }
        public Etiqueta Etiqueta { get; set; }
        public decimal Valor { get; set;  }
    }

    public class PlanejamentoView
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Meta { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Data Inicio")]
        public DateTime DataInicial { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Data Fim")]
        public DateTime DataFinal { get; set; }
        public string Tipo { get; set; }

        [Required]
        public int EtiquetaId { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Valor")]
        public decimal Valor { get; set; }
    }

}
