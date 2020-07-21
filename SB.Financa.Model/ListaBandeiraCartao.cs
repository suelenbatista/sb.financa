using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SB.Financa.Model
{
    public class ListaBandeiraCartao
    {
        public BandeiraCartao Bandeira { get; set; }
        public IEnumerable<ContaCartao> ContaCartao { get; set; }
    }

    public enum BandeiraCartao
    {
        Visa,
        Mastercard,
        Elo,
        AmericanExpress,
        Outras
    }

    public static class BandeiraListaCartaoExtensions
    {
        private static Dictionary<string, BandeiraCartao> mapa =
            new Dictionary<string, BandeiraCartao>
            {
                { "American Express", BandeiraCartao.AmericanExpress },
                { "Elo", BandeiraCartao.Elo },
                { "Mastercard", BandeiraCartao.Mastercard},
                { "Outras", BandeiraCartao.Outras },
                { "Visa", BandeiraCartao.Visa }
            };

        public static string BandeiraCartaoParaString(this BandeiraCartao tipo)
        {
            return mapa.First(s => s.Value == tipo).Key;
        }

        public static BandeiraCartao StringParaBandeiraCartao(this string texto)
        {
           return mapa.First(t => t.Key == texto).Value;
        }
    }
}
