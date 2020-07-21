using SB.Financa.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SB.Financa.Generics.Extension
{
    public static class EtiquetaExtension
    {
        public static EtiquetaView ToView(this Etiqueta model)
        {
            return new EtiquetaView
            {
                Id = model.Id,
                Nome = model.Nome
            };
        }
    }
}