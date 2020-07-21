using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SB.Financa.Model
{
    public class ListaTipoCartao
    {
        public TipoCartao Tipo { get; set; }
        public IEnumerable<ContaCartao> ContaCartao { get; set; }
    }

    public enum TipoCartao
    {
        Credito,
        Debito
    }

    public static class TipoListaCartaoExtensions
    {
        private static Dictionary<string, TipoCartao> mapa =
            new Dictionary<string, TipoCartao>
            {
                { "Credito", TipoCartao.Credito },
                { "Debito", TipoCartao.Debito },
            };

        public static string TipoCartaoParaString(this TipoCartao tipo)
        {
            return mapa.First(s => s.Value == tipo).Key;
        }

        public static TipoCartao StringParaTipoCartao(this string texto)
        {
            return mapa.First(t => t.Key == texto).Value;
        }
    }
}
