using System;
using System.Linq;

namespace SB.Financa.DAL
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> Todos { get; }
        TEntity ObterPorId(int Id);
        void Incluir(params TEntity[] entidade);
        void Alterar(params TEntity[] entidade);
        void Excluir(params TEntity[] entidade);
        void DetachLocal(Func<TEntity, bool> predicate);
        IQueryable<TEntity> OutroToList(Func<TEntity, bool> predicate);
    }
}
