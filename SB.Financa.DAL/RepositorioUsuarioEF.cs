using Microsoft.EntityFrameworkCore;
using SB.Financa.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SB.Financa.DAL
{
    public class RepositorioUsuarioEF<TEntity> : IRepository<Usuario>
    {
        private readonly FinancaContext _context;
        public RepositorioUsuarioEF(FinancaContext context)
        {
            _context = context;
        }

        public IQueryable<Usuario> Todos => _context.Usuario.AsQueryable();

        public Usuario ObterPorLogin(string login)
        {
           return _context.Usuario.Where(user => user.Login.Equals(login)).FirstOrDefault();
        }

        public Usuario ObterPorId(int Id)
        {
            return _context.Usuario.Find(Id);
        }

        public void Incluir(params Usuario[] entidade)
        {
            _context.Usuario.AddRange(entidade);
            _context.SaveChanges();
        }

        public void Alterar(params Usuario[] entidade)
        {
            _context.Usuario.UpdateRange(entidade);
            _context.SaveChanges();
        }

        public void Excluir(params Usuario[] entidade)
        {
            _context.Usuario.RemoveRange(entidade);
            _context.SaveChanges();
        }

        public void DetachLocal(Func<Usuario, bool> predicate)
        {
            Usuario usuarioLocal = _context.Usuario.Local.Where(predicate).FirstOrDefault();
            if (usuarioLocal != null)   
                _context.Entry(usuarioLocal).State = EntityState.Detached;
        }

        public IQueryable<Usuario> OutroToList(Func<Usuario, bool> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
