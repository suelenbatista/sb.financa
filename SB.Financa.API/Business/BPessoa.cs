using SB.Financa.DAL;
using SB.Financa.Generics.Extension;
using SB.Financa.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SB.Financa.API.Business
{
    public class BPessoa
    {
        private readonly IRepository<Pessoa> repository;
        
        public BPessoa(IRepository<Pessoa> _repositoryPes) 
        {
           this.repository = _repositoryPes;
        }

        public List<PessoaView> ObterTodos()
        {
            return repository.Todos.Select(p => p.ToView()).ToList();
        }

        public PessoaView ObterPorId(int id)
        {
            return repository.ObterPorId(id).ToView();
        }

        public PessoaView Incluir(PessoaView pessoaView)
        {
            Pessoa pessoa = ObterModel(pessoaView);
            repository.Incluir(pessoa);

            return pessoa.ToView();
        }


        public PessoaView ObterPorDocumento (string documento)
        {
            return repository.Todos.Where(p => p.Documento.Equals(documento))
                            .Select(P => P.ToView()).FirstOrDefault();
        }
        public void Alterar(PessoaView pessoaView)
        {
            if (pessoaView.Id <= 0 || pessoaView.Id.Equals(int.MinValue))
            {
                throw new ArgumentException("O código da pessoa é obrigatório.");
            }

            repository.DetachLocal(p => p.Id == pessoaView.Id);
            repository.Alterar(ObterModel(pessoaView));
        }

        public void Excluir(PessoaView pessView)
        {
            Pessoa pessoa = repository.ObterPorId(pessView.Id);

            if (pessoa.Movimentos != null && pessoa.Movimentos.Any())
            {
                throw new Exception("Operação não permitida pois existe(m) movimentos associadas a pessoa da exclusão.");
            }

            repository.Excluir(pessoa);
        }

        private Pessoa ObterModel(PessoaView pessoaView)
        {
            return new Pessoa
            {
                Id = pessoaView.Id,
                Nome = pessoaView.Nome,
                Tipo = pessoaView.Tipo.StringParaTipoDoc(),
                Documento = pessoaView.Documento,
                Ativo = pessoaView.Ativo
            };
        }

    }
}