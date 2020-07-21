using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SB.Financa.Model
{
    public class ListaDirecaoMovimentoBaixa
    {
        public DirecaoBaixa Direcao { get; set; }
        public IEnumerable<MovimentoBaixa> Movimento { get; set; }
    }

    public enum DirecaoBaixa 
    {
        ENTRADA,
        SAIDA
    }

    public static class DirecaoListaMovimentoBaixaExtensions
    {
        private static Dictionary<string, DirecaoBaixa> mapa =
            new Dictionary<string, DirecaoBaixa>
            {
                { "ENTRADA", DirecaoBaixa.ENTRADA },
                { "SAIDA", DirecaoBaixa.SAIDA }
            };

        public static string DirecaoBaixaParaString(this DirecaoBaixa tipo)
        {
            return mapa.First(s => s.Value == tipo).Key;
        }

        public static DirecaoBaixa StringParaDirecaoBaixa(this string texto)
        {
            return mapa.First(t => t.Key == texto).Value;
        }
    }

}
