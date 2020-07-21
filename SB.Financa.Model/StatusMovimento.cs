using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SB.Financa.Model
{
    public class ListaStatusMovimento
    {
        public StatusMovimento Status { get; set; }
        public IEnumerable<Movimento> Movimento { get; set; }
    }

    public enum StatusMovimento
    {
        CANCELADO,
        ENCERRADO,
        PENDENTE
    }

    public static class StatusListaMovimentoExtensions
    {
        private static Dictionary<string, StatusMovimento> mapa =
            new Dictionary<string, StatusMovimento>
            {
                { "CANCELADO", StatusMovimento.CANCELADO },
                { "ENCERRADO", StatusMovimento.ENCERRADO },
                { "PENDENTE", StatusMovimento.PENDENTE }
            };

        public static string StatusMovimentoParaString(this StatusMovimento tipo)
        {
            return mapa.First(s => s.Value == tipo).Key;
        }

        public static StatusMovimento StringParaStatusMovimento(this string texto)
        {
            return mapa.First(t => t.Key == texto).Value;
        }
    }
}
