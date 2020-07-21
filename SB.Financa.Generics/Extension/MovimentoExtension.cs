using SB.Financa.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SB.Financa.Generics.Extension
{
    public static class MovimentoExtension
    {
        public static MovimentoView ToView(this Movimento model)
        {
            return new MovimentoView
            {
                Id = model.Id,
                Cadastro = model.Cadastro,
                Vencimento = model.Vencimento,
                Tipo = model.Tipo.TipoMovimentoParaString(),
                Status = model.Status.StatusMovimentoParaString(),
                EtiquetaId = model.EtiquetaId,
                PessoaId = model.PessoaId,
                Descricao = model.Descricao,
                Valor = model.Valor,
                ValorPago = model.ValorPago,
                ContaCartaoId = (model.ContaCartao == null ? 0 : model.ContaCartao.Id),
                MovimentoBaixa = model.MovimentoBaixa
            };
        }
    }
}
