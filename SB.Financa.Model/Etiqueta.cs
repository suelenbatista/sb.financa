using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SB.Financa.Model
{
    public class Etiqueta
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public IList<Movimento> Movimentos { get; set; }
        public IList<Planejamento> Planejamentos { get; set; }
    }

    public class EtiquetaView
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nome { get; set; }
    }
}