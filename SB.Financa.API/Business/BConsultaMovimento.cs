using SB.Financa.DAL;
using SB.Financa.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SB.Financa.API.Business
{
    public class BConsultaMovimento 
    {
        private readonly IRepository<Movimento> repository;
        public BConsultaMovimento(IRepository<Movimento> _repository) { this.repository = _repository; }

        public ListaTipoMovimento ObterListaPorTipoMovimento(TipoMovimento tipoMov)
        {
            return new ListaTipoMovimento()
            {
                Tipo = tipoMov,
                Movimento = repository.Todos.Where(mov => mov.Tipo.Equals(tipoMov)).ToList()
            };
        }

        public ListaStatusMovimento ObterListaPorStatusMovimento(StatusMovimento statusMov)
        {
            return new ListaStatusMovimento()
            {
                Status = statusMov,
                Movimento = repository.Todos.Where(mov => mov.Status.Equals(statusMov)).ToList()
            };
        }

        public ListaTipoMovimento ObterTipoMovimentoFiltro(TipoMovimento tipoMovimento, StatusMovimento statusMovimento,
                                                           Pessoa? pessoa, Etiqueta? etiqueta, DateTime? vencInicial, DateTime? vencFinal)
        {
            return new ListaTipoMovimento()
            {
                Tipo = tipoMovimento,
                Movimento = repository.Todos.Where(mov => mov.Tipo.Equals(tipoMovimento) &&  mov.Status.Equals(statusMovimento) &&
                                        (pessoa == null   ? mov.Pessoa.Equals(mov.Pessoa)     : mov.Pessoa.Equals(pessoa)) &&
                                        (etiqueta == null ? mov.Etiqueta.Equals(mov.Etiqueta) : mov.Etiqueta.Equals(etiqueta)) && 
                                        (!vencInicial.HasValue ? mov.Vencimento.Equals(mov.Vencimento) : mov.Vencimento >= vencInicial) &&
                                        (!vencFinal.HasValue ? mov.Vencimento.Equals(mov.Vencimento) : mov.Vencimento <= vencFinal)).ToList()
            };
          }

        #region [Funções de Apoio ...]
        public bool ExisteMovimentoParaPessoa(int pessoaId)
        {
            return repository.OutroToList(mov => mov.Pessoa.Id.Equals(pessoaId)).Any();
        }

        public bool ExisteMovimentoParaEtiqueta(int etiquetaId)
        {
            return repository.OutroToList(mov => mov.Etiqueta.Id.Equals(etiquetaId)).Any();
        }

        public bool ExisteMovimentoParaContaCartao(int contaCartaoId)
        {
            return repository.OutroToList(mov => mov.ContaCartao.Id.Equals(contaCartaoId)).Any();
        }
        #endregion [... Funções de Apoio ]
    }
}
