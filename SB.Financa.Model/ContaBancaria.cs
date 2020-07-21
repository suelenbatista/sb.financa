using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SB.Financa.Model
{
    public class ContaBancaria
    {
        public int Id { get;  set; }
        public string Nome { get;  set; }
        public string Agencia { get;  set; }
        public string Conta { get;  set; }
        public string Digito { get;  set; }
        public bool Ativa { get;  set; }

        public IList<Movimento> MovimentosBaixa { get; set; }
    }

    public class ContaBancariaView
    {
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        public string Agencia { get; set; }
        public string Conta { get; set; }
        public string Digito { get; set; }
        public bool Ativa { get; set; }
    }
}
