using System.Collections.Generic;
using System.Linq;

namespace SB.Financa.Model
{
    public class ListaTipoDocumento
    {
        public TipoDocumento Tipo { get; set; }
        public IEnumerable<Pessoa> Pessoa { get; set; }
    }

    public enum TipoDocumento
    {
        CNPJ,
        CPF,
        RG,
        Outro
    }

    public static class TipoDocListaPessoaExtensions
    {
        private static Dictionary<string, TipoDocumento> mapa =
            new Dictionary<string, TipoDocumento>
            {
                { "CNPJ", TipoDocumento.CNPJ },
                { "CPF", TipoDocumento.CPF },
                { "RG", TipoDocumento.RG },
                { "Outro", TipoDocumento.Outro },
            };

        public static string TipoDocParaString(this TipoDocumento tipo)
        {
            return mapa.First(s => s.Value == tipo).Key;
        }

        public static TipoDocumento StringParaTipoDoc(this string texto)
        {
            return mapa.First(t => t.Key == texto).Value;
        }
    }
}