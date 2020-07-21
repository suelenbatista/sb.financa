using System.Collections.Generic;
using System.Linq;

namespace SB.Financa.Model
{
    public class ListaTipoPlanejamento
    {
        public TipoPlanejamento Tipo { get; set; }
        public IEnumerable<Planejamento> Planejamento { get; set; }
    }
    public enum TipoPlanejamento
    {
        Patrimonio,
        Despesas
    }

    public static class TipoListaPlanjamentoExtensions
    {
        private static Dictionary<string, TipoPlanejamento> mapa =
            new Dictionary<string, TipoPlanejamento>
            {
                { "Patrimonio", TipoPlanejamento.Patrimonio },
                { "Despesas", TipoPlanejamento.Despesas },
            };

        public static string TipoPlanejamentoParaString(this TipoPlanejamento tipo)
        {
            return mapa.First(s => s.Value == tipo).Key;
        }

        public static TipoPlanejamento StringParaTipoPlanejamento(this string texto)
        {
            return mapa.First(t => t.Key == texto).Value;
        }
    }
}