using SB.Financa.DAL;
using SB.Financa.Generics.Extension;
using SB.Financa.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SB.Financa.API.Business
{
    public class BMovimento
    {
        private readonly IRepository<Movimento> repository;
        private readonly IRepository<Etiqueta> repoEtiqueta;
        private readonly IRepository<Pessoa> repoPessoa;
        private readonly IRepository<ContaCartao> repoContaCartao;

        public BMovimento(IRepository<Movimento> _repository, IRepository<Etiqueta> _repoEtiqueta, 
                IRepository<Pessoa> _repoPessoa, IRepository<ContaCartao> _repoContaCartao)  
        { 
            repository = _repository;
            repoPessoa = _repoPessoa;
            repoEtiqueta = _repoEtiqueta;
            repoContaCartao = _repoContaCartao;
        }

        #region [Crud Movimentos ...]
        public List<MovimentoView> ObterTodos()
        {
            return repository.Todos.Select(m => m.ToView()).ToList();
        }

        public MovimentoView ObterPorId(int id)
        {
            return repository.ObterPorId(id).ToView();
        }

        public MovimentoView Incluir(MovimentoView movimentoView)
        {
            if (movimentoView.Cadastro.Equals(DateTime.MinValue)) {
                movimentoView.Cadastro = DateTime.Now;
            }

            Movimento movimento = ObterModel(movimentoView);
            repository.Incluir(movimento);

            return movimento.ToView();
        }


        public void Alterar(MovimentoView movimentoView)
        {
            if (movimentoView.Id <= 0 || movimentoView.Id.Equals(int.MinValue))
            {
                throw new ArgumentException("O código do movimento é obrigatório.");
            }

            if (movimentoView.Saldo < 0)
            {
                throw new Exception($"O movimento id {movimentoView.Id} não pode possuir saldo negativo." +
                                    $"Ajuste o valor da baixa do título.");
            }

            Movimento movimento = ObterModel(movimentoView);

            decimal valorJahBaixado = 0M;

            if (movimento.MovimentoBaixa != null  && movimento.MovimentoBaixa.Any()) {
                valorJahBaixado = movimento.MovimentoBaixa.ToList().Sum(vl => vl.ValorBaixa);
            }

            /* Caso o usurio sete o status para cancelado */
            if (valorJahBaixado > 0 && movimento.Status.Equals(StatusMovimento.CANCELADO))
            {
                throw new Exception($"O movimento id '{movimento.Id}' já possui registro de baixa - Valor Baixado R$ {valorJahBaixado.ToString("D2")}. " +
                                    "Operação não permitida - Remova as baixas antes de realizar o cancelamento do título. ");
            }

            /* Caso o usurio sete o status para encerrado */
            if (valorJahBaixado != movimento.ValorPago && movimento.Status.Equals(StatusMovimento.ENCERRADO))
            {
                throw new Exception($"O movimento id '{movimento.Id}' não poderá ter o status para 'ENCERRADO' " +
                                     $"pois o valor baixado não é igual ao valor pago.");
            }

            if (!movimento.Status.Equals(StatusMovimento.CANCELADO))
            {
                if (movimento.Saldo > 0) 
                {
                    movimento.Status = StatusMovimento.PENDENTE;
                }else if (movimento.Saldo == 0) 
                {
                    movimento.Status = StatusMovimento.ENCERRADO;
                }
            }

            repository.DetachLocal(mov => mov.Id == movimento.Id);
            repository.Alterar(movimento);
        }

        public void Excluir(MovimentoView movimentoView)
        {
            repository.Excluir(ObterModel(movimentoView));
        }

        private Movimento ObterModel(MovimentoView view)
        {
            if (view.EtiquetaId <=0 || view.EtiquetaId.Equals(int.MinValue))
            {
                throw new ArgumentException("O campo de 'EtiquetaId' é obrigatório e deve ser preenchido corretamente.");
            }

            if (view.PessoaId <= 0 || view.PessoaId.Equals(int.MinValue))
            {
                throw new ArgumentException("O campo de 'PessoaId' é obrigatório e deve ser preenchido corretamente.");
            }

            Etiqueta etiqueta = repoEtiqueta.ObterPorId(view.EtiquetaId);
            if (etiqueta == null)
            {
                throw new Exception($"A EtiquetaId  '{view.EtiquetaId}' informada não existe no banco de dados! Campo obrigatório.");
            }

            Pessoa pessoa = repoPessoa.ObterPorId(view.PessoaId);
            if (etiqueta == null)
            {
                throw new Exception($"A PessoaId '{view.PessoaId}' informada não existe no banco de dados! Campo obrigatório.");
            }

            ContaCartao contaCartao = null;
            if (view.ContaCartaoId.HasValue)
            {
                contaCartao = repoContaCartao.ObterPorId(view.ContaCartaoId.Value);

                if (contaCartao == null) {
                    throw new Exception($"A ContaCartaoId '{view.ContaCartaoId.Value}' informada não existe no banco de dados!");
                }
            }

            return new Movimento()
            {
                Id = view.Id,
                Cadastro = view.Cadastro,
                Tipo = view.Tipo.StringParaTipoMovimento(),
                Vencimento = view.Vencimento,
                Status = view.Status.StringParaStatusMovimento(),
                Descricao = view.Descricao,
                Valor = view.Valor,
                ValorPago = view.ValorPago,
                EtiquetaId = view.EtiquetaId,
                Etiqueta = etiqueta,
                PessoaId = view.PessoaId,
                Pessoa = pessoa,
                ContaCartao = contaCartao
            };
        }

        #endregion [... Crud Movimentos]

     

    }
}
