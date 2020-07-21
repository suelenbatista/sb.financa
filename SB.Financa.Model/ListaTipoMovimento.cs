using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SB.Financa.Model
{
    public class ListaTipoMovimento
    {
        public TipoMovimento Tipo { get; set; }
        public IEnumerable<Movimento> Movimento { get; set; }
    }

    public enum TipoMovimento
    {
        PAGAR,
        RECEBER
    }
    public static class TipoListaMovimentoExtensions
    {
        private static Dictionary<string, TipoMovimento> mapa =
            new Dictionary<string, TipoMovimento>
            {
                { "PAGAR", TipoMovimento.PAGAR },
                { "RECEBER", TipoMovimento.RECEBER }
            };

        public static string TipoMovimentoParaString(this TipoMovimento tipo)
        {
            return mapa.First(s => s.Value == tipo).Key;
        }

        public static TipoMovimento StringParaTipoMovimento(this string texto)
        {
            return mapa.First(t => t.Key == texto).Value;
        }
    }
}
