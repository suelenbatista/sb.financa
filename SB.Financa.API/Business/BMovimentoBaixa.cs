using SB.Financa.DAL;
using SB.Financa.Generics.Extension;
using SB.Financa.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SB.Financa.API.Business
{
    public class BMovimentoBaixa
    {
        private readonly IRepository<MovimentoBaixa> repositoryMovBaixa;
        private readonly IRepository<Movimento> repoMovimento;
        private readonly IRepository<ContaBancaria> repoContaBancaria;
        public BMovimentoBaixa(IRepository<MovimentoBaixa> _repository,
                               IRepository<Movimento> _repoMovimento, IRepository<ContaBancaria> _repoContaBancaria) 
        {
            repositoryMovBaixa = _repository;
            repoMovimento = _repoMovimento;
            repoContaBancaria = _repoContaBancaria;
        }

        #region [Crud Movimento Baixa ...]
        public List<MovimentoBaixaView> ObterTodos()
        {
            return repositoryMovBaixa.Todos.Select(mb => mb.ToView()).ToList();
        }

        public MovimentoBaixaView ObterPorId(int id)
        {
            return repositoryMovBaixa.ObterPorId(id).ToView();
        }

        public List<MovimentoBaixaView> ObterPorMovimentoId(int id)
        {
            return repositoryMovBaixa.OutroToList(mb => mb.Movimento.Id.Equals(id))
                    .Select(mb => mb.ToView()).ToList();
        }


        public List<MovimentoBaixaView> ObterBaixaPorMovimento(int movimentoId)
        {
            return repositoryMovBaixa
                    .OutroToList(movBaixa => movBaixa.Movimento.Id.Equals(movimentoId))
                    .Select(mb => mb.ToView())
                    .ToList();
        }

        public MovimentoBaixaView Incluir(MovimentoBaixaView movBaixaView)
        {
            movBaixaView.DataEvento = DateTime.Now;

            MovimentoBaixa movBaixa = ObterModel(movBaixaView);

            /* Identifica a direção do valor na conta (entra ou saida) */  
            if (movBaixa.Movimento.Tipo.Equals(TipoMovimento.PAGAR)) {
                movBaixa.Direcao = DirecaoBaixa.SAIDA;
            }else {
                movBaixa.Direcao = DirecaoBaixa.ENTRADA;
            }

            if (movBaixa.Movimento.Saldo < movBaixa.ValorBaixa) {
                throw new Exception($"O movimento id {movBaixa.Movimento.Id} possui um saldo '{movBaixa.Movimento.Saldo}' " +
                                    $"inferior ao valor da baixa '{movBaixa.ValorBaixa}'. " +
                                    $"Baixa somente até o saldo devedor / credor."); }

            movBaixa.Movimento.ValorPago = movBaixa.Movimento.ValorPago  + movBaixa.ValorBaixa;

            /* Atualiza o registro pai de movimento */
            repoMovimento.Alterar(movBaixa.Movimento);

            /* Inclui o registro de baixa */
            repositoryMovBaixa.Incluir(movBaixa);
            
            return movBaixa.ToView();
        }

        public void Excluir(MovimentoBaixaView movBaixaView)
        {
            MovimentoBaixa movBaixa = ObterModel(movBaixaView);

            if (movBaixa.Movimento.ValorPago < movBaixa.ValorBaixa)
            {
                throw new Exception($"O movimento id {movBaixa.Movimento.Id} possui um saldo pago de  '{movBaixa.Movimento.ValorPago}' " +
                                    $"inferior ao valor da baixa '{movBaixa.ValorBaixa}'. " +
                                    $"Baixa somente até o saldo devedor / credor.");
            }

            movBaixa.Movimento.ValorPago = movBaixa.Movimento.ValorPago - movBaixa.ValorBaixa;

            /* Atualiza o registro pai de movimento */
            repoMovimento.Alterar(movBaixa.Movimento);

            /* Exclui o registro da tabela de baixa */
            repositoryMovBaixa.Excluir(movBaixa);
        }

        private MovimentoBaixa ObterModel(MovimentoBaixaView movBaixaView)
        {
            if (movBaixaView.MovimentoId <= 0 || movBaixaView.MovimentoId.Equals(int.MinValue))
            {
                throw new ArgumentException("O campo de 'MovimentoId' é obrigatório e deve ser preenchido corretamente.");
            }

            if (movBaixaView.ContaBancariaId <= 0 || movBaixaView.ContaBancariaId.Equals(int.MinValue))
            {
                throw new ArgumentException("O campo de 'ContaBancariaId' é obrigatório e deve ser preenchido corretamente.");
            }

            Movimento movimento = repoMovimento.ObterPorId(movBaixaView.MovimentoId);
            if (movimento == null)
            {
                throw new Exception($"O MovimentoId '{movBaixaView.MovimentoId}' informado não existe no banco de dados! Campo obrigatório.");
            }

            ContaBancaria contaBancaria = repoContaBancaria.ObterPorId(movBaixaView.ContaBancariaId);
            if (contaBancaria == null)
            {
                throw new Exception($"A ContaBancariaId '{movBaixaView.ContaBancariaId}' informada não existe no banco de dados! Campo obrigatório.");
            }

            return new MovimentoBaixa
            {
                Id = movBaixaView.Id,
                DataEvento = movBaixaView.DataEvento,
                DataBaixa = movBaixaView.DataBaixa,
                ValorBaixa = movBaixaView.ValorBaixa,
                Direcao = movBaixaView.Direcao.StringParaDirecaoBaixa(),
                MovimentoId = movBaixaView.MovimentoId,
                Movimento = movimento,
                ContaBancariaId = movBaixaView.ContaBancariaId,
                ContaBancaria = contaBancaria
            };
        }


        #endregion [... Crud Movimento Baixa]

    }
}
