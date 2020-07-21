using System.Collections.Generic;
using System.Linq;

namespace SB.Financa.Model
{
    public class ListaTipoPerfil
    {
        public TipoPerfil Perfil { get; set; }
        public IEnumerable<Usuario> Usuario { get; set; }
    }

    public enum TipoPerfil
    {
        ADMINISTRADOR,
        CONTROLE
    }
    public static class ListaTipoPerfilExtensions
    {
        private static Dictionary<string, TipoPerfil> mapa =
            new Dictionary<string, TipoPerfil>
            {
                { "ADMINISTRADOR", TipoPerfil.ADMINISTRADOR },
                { "CONTROLE", TipoPerfil.CONTROLE },
            };

        public static string TipoPerfilParaString(this TipoPerfil tipo)
        {
            return mapa.First(s => s.Value == tipo).Key;
        }

        public static TipoPerfil StringParaTipoPerfil(this string texto)
        {
            return mapa.First(t => t.Key == texto).Value;
        }
    }


}