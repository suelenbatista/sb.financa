using SB.Financa.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SB.Financa.Generics.Extension
{
    public static class MovimentoBaixaExtension
    {
        public static MovimentoBaixaView ToView(this MovimentoBaixa model)
        {
            return new MovimentoBaixaView
            {
                Id = model.Id,
                DataEvento = model.DataEvento,
                DataBaixa = model.DataBaixa,
                Direcao = model.Direcao.DirecaoBaixaParaString(),
                ValorBaixa = model.ValorBaixa,
                MovimentoId = model.MovimentoId,
                ContaBancariaId = model.ContaBancariaId
            };
        }
    }
}
