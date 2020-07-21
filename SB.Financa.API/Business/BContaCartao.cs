using SB.Financa.DAL;
using SB.Financa.Generics.Extension;
using SB.Financa.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SB.Financa.API.Business
{
    public class BContaCartao
    {
        private readonly IRepository<ContaCartao> repository;
        public BContaCartao(IRepository<ContaCartao> _repository) { repository = _repository; }

        public List<ContaCartaoView> ObterTodos()
        {
            return repository.Todos
                            .Select(c => c.ToView()).ToList();
        }

        public ContaCartaoView ObterPorId(int id)
        {
            return repository.ObterPorId(id).ToView();
        }

        public ContaCartaoView Incluir(ContaCartaoView cartaoCreditoView)
        {
            if (cartaoCreditoView.Id > 0 )
            {
                throw new ArgumentException("O código da conta cartão é não deve ser maior que zero.");
            }

            ContaCartao model = ObterModel(cartaoCreditoView);
            repository.Incluir(model);

            return model.ToView();
        }

        public void Alterar(ContaCartaoView contaCartaoView)
        {
            if (contaCartaoView.Id <= 0 || contaCartaoView.Id.Equals(int.MinValue))
            {
                throw new ArgumentException("O código da conta cartão é obrigatório.");
            }

            repository.DetachLocal(p => p.Id == contaCartaoView.Id);
            repository.Alterar(ObterModel(contaCartaoView));
        }

        public void Excluir(ContaCartaoView contaCartaoView)
        {
            repository.Excluir(ObterModel(contaCartaoView));
        }


        private ContaCartao ObterModel(ContaCartaoView view)
        {
            return new ContaCartao
            {
                Id = view.Id,
                Apelido = view.Apelido,
                Numero = view.Numero,
                Bandeira = view.Bandeira.StringParaBandeiraCartao(),
                Tipo = view.Tipo.StringParaTipoCartao()
            };
        }
    }
}
