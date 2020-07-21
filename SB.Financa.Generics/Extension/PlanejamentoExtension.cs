using SB.Financa.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SB.Financa.Generics.Extension
{
    public static class PlanejamentoExtension
    {
        public static PlanejamentoView ToView(this Planejamento model)
        {
            return new PlanejamentoView
            {
                Id = model.Id,
                Meta = model.Meta,
                DataInicial = model.DataInicial,
                DataFinal = model.DataFinal,
                Valor = model.Valor,
                EtiquetaId = model.EtiquetaId,
                Tipo = model.Tipo.TipoPlanejamentoParaString()
            };
        }
    }
}