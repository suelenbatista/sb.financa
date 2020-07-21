using SB.Financa.DAL;
using SB.Financa.Generics.Extension;
using SB.Financa.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SB.Financa.API.Business
{
    public class BEtiqueta
    {
        private readonly IRepository<Etiqueta> repository;
        public BEtiqueta(IRepository<Etiqueta> _repository) { repository = _repository; }

        public List<EtiquetaView> ObterTodos()
        {
            return repository.Todos
                            .Select(e => e.ToView()).ToList();
        }

        public EtiquetaView ObterPorId(int id)
        {
            return repository.ObterPorId(id).ToView();
        }

        public EtiquetaView Incluir(EtiquetaView etiquetaView)
        {
            if (ObterPorNome(etiquetaView.Nome) != null)
            {
                throw new Exception($"Já existe uma etique cadastrada para o nome " +
                                    $"'{etiquetaView.Nome}'. Operação cancelada!");
            }

            Etiqueta model = ObterModel(etiquetaView);
            repository.Incluir(ObterModel(etiquetaView));


            return model.ToView();
        }

        public void Alterar(EtiquetaView etiquetaView)
        {
            if (etiquetaView.Id <= 0 || etiquetaView.Id.Equals(int.MinValue))
            {
                throw new ArgumentException("O código da etiqueta é obrigatório.");
            }

            repository.DetachLocal(e => e.Id == etiquetaView.Id);
            repository.Alterar(ObterModel(etiquetaView));
        }

        public EtiquetaView ObterPorNome(string nome)
        {
            return repository.OutroToList(e => e.Nome.Equals(nome))
                .Select(e => e.ToView()).FirstOrDefault();
        }
       
        public void Excluir(EtiquetaView etiquetaView)
        {
            Etiqueta etiqueta = ObterModel(etiquetaView);

            if (etiqueta.Planejamentos != null && etiqueta.Planejamentos.Any())
            {
                throw new Exception("Existe(m) planejamento(s)/ Meta(s) associada(s) a essa etiqueta. Operação cancelada!");
            }

            if (etiqueta.Movimentos != null && etiqueta.Movimentos.Any())
            {
                throw new Exception("Existe(m) movimento(s) associado(s) a essa etiqueta. Operação cancelada!");
            }

            repository.Excluir(etiqueta);
        }

        private Etiqueta ObterModel(EtiquetaView etiquetaView)
        {
            return new Etiqueta
            {
                Id = etiquetaView.Id,
                Nome = etiquetaView.Nome
            };
        }
    }
}
