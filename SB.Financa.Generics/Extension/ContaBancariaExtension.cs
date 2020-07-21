using SB.Financa.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SB.Financa.Generics.Extension
{
    public static class ContaBancariaExtension
    {
        public static ContaBancariaView ToView(this ContaBancaria model)
        {
            return new ContaBancariaView
            {
                Id = model.Id,
                Nome = model.Nome,
                Agencia = model.Agencia,
                Conta = model.Conta,
                Digito = model.Digito,
                Ativa = model.Ativa
            };
        }
    }
}