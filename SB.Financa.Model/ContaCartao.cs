using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SB.Financa.Model
{
    public class ContaCartao
    {
        public int Id { get; set; }
        public string Apelido { get; set; }
        public string Numero { get; set; }
        public BandeiraCartao Bandeira { get; set; }
        public TipoCartao Tipo { get; set; }
        public IList<Movimento> Movimentos { get; set; }
    }

    public class ContaCartaoView
    {
        public int Id { get; set; }
        public string Numero { get; set; }

        [Required]
        public string Apelido { get; set; }

        [Required]
        public string Bandeira { get; set; }

        [Required]
        public string Tipo { get; set; }
    }
}
