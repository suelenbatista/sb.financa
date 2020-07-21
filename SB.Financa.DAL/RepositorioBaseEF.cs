using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SB.Financa.DAL
{
    public class RepositorioBaseEF<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly FinancaContext _context;

        public RepositorioBaseEF(FinancaContext context)
        {
            _context = context;
        }

        public IQueryable<TEntity> Todos => _context.Set<TEntity>().AsQueryable();

        public void Alterar(params TEntity[] entidade)
        {
            _context.Set<TEntity>().UpdateRange(entidade);
            _context.SaveChanges();
        }

        public void Excluir(params TEntity[] entidade)
        {
            _context.Set<TEntity>().RemoveRange(entidade);
            _context.SaveChanges();
        }

        public void Incluir(params TEntity[] entidade)
        {
            _context.Set<TEntity>().AddRange(entidade);
            _context.SaveChanges();
        }

        public TEntity ObterPorId(int Id)
        {
            return _context.Find<TEntity>(Id);
        }

        public virtual void DetachLocal(Func<TEntity, bool> predicate)
        {
            var local = _context.Set<TEntity>().Local.Where(predicate).FirstOrDefault();

            if (local != null)
            {
                _context.Entry(local).State = EntityState.Detached;
            }
        }

        public IQueryable<TEntity> OutroToList(Func<TEntity, bool> predicate)
        {
            return _context.Set<TEntity>().Where(predicate).AsQueryable();
        }
    }
}
