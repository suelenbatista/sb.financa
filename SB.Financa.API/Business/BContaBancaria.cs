using SB.Financa.DAL;
using SB.Financa.Generics.Extension;
using SB.Financa.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SB.Financa.API.Business
{
    public class BContaBancaria
    {
        private readonly IRepository<ContaBancaria> repository;
        public BContaBancaria(IRepository<ContaBancaria> _repository) { repository = _repository; }

        public List<ContaBancariaView> ObterTodos()
        {
            return repository.Todos
                             .Select(cb => cb.ToView()).ToList();
        }

        public ContaBancariaView ObterPorId(int id)
        {
            return repository.ObterPorId(id).ToView();
        }

        public ContaBancariaView Incluir(ContaBancariaView contaBancariaView)
        {
            ContaBancaria model = ObterModel(contaBancariaView);
            repository.Incluir(model);

            return model.ToView();
        }

        public void Alterar(ContaBancariaView contaBancariaView)
        {
            if (contaBancariaView.Id <= 0 || contaBancariaView.Id.Equals(int.MinValue))
            {
                throw new ArgumentException("O código da conta bancaria é obrigatório.");
            }

            repository.DetachLocal(p => p.Id == contaBancariaView.Id);
            repository.Alterar(ObterModel(contaBancariaView));
        }

        public void Excluir(ContaBancariaView contaBancariaView)
        {
            ContaBancaria contaBancaria = repository.ObterPorId(contaBancariaView.Id);

            if (contaBancaria == null)
            {
                throw new Exception($"Conta Bancária de Id {contaBancariaView.Id} não localizada no banco de dados.");
            }

            if (contaBancaria.MovimentosBaixa != null  && contaBancaria.MovimentosBaixa.Any())
            {
                throw new Exception("Existem movimentos de baixa associado a essa conta bancária. Operação cancelada!");
            }

            repository.Excluir(contaBancaria);
        }


        private ContaBancaria ObterModel(ContaBancariaView view)
        {
            return new ContaBancaria
            {
                Id = view.Id,
                Nome = view.Nome,
                Agencia = view.Agencia,
                Conta = view.Conta,
                Digito = view.Digito,
                Ativa = view.Ativa
            };
        }
    }
}
