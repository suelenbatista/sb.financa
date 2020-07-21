using SB.Financa.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SB.Financa.Generics.Extension
{
    public static class ContaCartaoExtension
    {
        public static ContaCartaoView ToView(this ContaCartao model)
        {
            return new ContaCartaoView
            {
                Id = model.Id,
                Numero = model.Numero,
                Apelido = model.Apelido,
                Bandeira = model.Bandeira.BandeiraCartaoParaString(),
                Tipo = model.Tipo.TipoCartaoParaString()
            };
        }
    }
}
