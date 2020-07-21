using SB.Financa.DAL;
using SB.Financa.Generics.Extension;
using SB.Financa.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SB.Financa.API.Business
{
    public class BPlanejamento
    {
        private readonly IRepository<Planejamento> repository;
        private readonly IRepository<Etiqueta> repoEtiqueta;
        public BPlanejamento(IRepository<Planejamento> _repository, IRepository<Etiqueta> _repoEtiqueta) { 
            repository = _repository;
            repoEtiqueta = _repoEtiqueta;
        }

        public List<PlanejamentoView> ObterTodos()
        {
            return repository.Todos.Select(p => p.ToView()).ToList();
        }

        public PlanejamentoView ObterPorId(int id)
        {
            return repository.ObterPorId(id).ToView();
        }

        public PlanejamentoView Incluir(PlanejamentoView planejamentoView)
        {
            Planejamento planejamento = ObterModel(planejamentoView);
            repository.Incluir(planejamento);

            return planejamento.ToView();
        }

        public void Alterar(PlanejamentoView planejamentoView)
        {
            if (planejamentoView.Id <= 0 || planejamentoView.Id.Equals(int.MinValue))
            {
                throw new ArgumentException("O código do planejamento é obrigatório.");
            }

            repository.DetachLocal(e => e.Id == planejamentoView.Id);
            repository.Alterar(ObterModel(planejamentoView));
        }

        public void Excluir(PlanejamentoView planejamentoView)
        {
            Planejamento planejamento = repository.ObterPorId(planejamentoView.Id);
             
            if (planejamento != null) {
                repository.Excluir(planejamento);
            }
        }

        private Planejamento ObterModel(PlanejamentoView planejamentoView)
        {
            if (planejamentoView.EtiquetaId <= 0 || planejamentoView.EtiquetaId.Equals(int.MinValue))
            {
                throw new ArgumentException("O campo 'EtiquetaId' é obrigatório.");
            }
           
            Etiqueta etiqueta = repoEtiqueta.ObterPorId(planejamentoView.EtiquetaId);
            if (etiqueta == null)
            {
                throw new Exception($"A EtiquetaId '{planejamentoView.EtiquetaId}' não existe no banco de dados.");
            }

            return new Planejamento
            {
                Id = planejamentoView.Id,
                Meta = planejamentoView.Meta,
                DataInicial = planejamentoView.DataInicial,
                DataFinal = planejamentoView.DataFinal,
                Tipo = planejamentoView.Tipo.StringParaTipoPlanejamento(),
                EtiquetaId = planejamentoView.EtiquetaId,
                Etiqueta = etiqueta,
                Valor = planejamentoView.Valor,
            };
        }
    }
}
