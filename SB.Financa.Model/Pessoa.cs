using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SB.Financa.Model
{
    public class Pessoa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public TipoDocumento Tipo { get; set; }
        public string Documento { get; set; }
        public bool Ativo { get; set; }

        public IList<Movimento> Movimentos { get; set; }
    }

    public class PessoaView
    {
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        
        [Required]
        public string Tipo { get; set; }
        
        public string Documento { get; set; }
        public bool Ativo { get; set; }
    }
}