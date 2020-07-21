using SB.Financa.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SB.Financa.Generics.Extension
{
    public static class PessoaExtension
    {
        public static PessoaView ToView(this Pessoa model)
        {
            return new PessoaView
            {
                Id = model.Id,
                Nome = model.Nome,
                Tipo = model.Tipo.TipoDocParaString(),
                Documento = model.Documento,
                Ativo = model.Ativo
            };
        }
    }
}
