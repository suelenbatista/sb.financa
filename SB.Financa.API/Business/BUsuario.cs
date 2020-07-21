using SB.Financa.API.Services;
using SB.Financa.DAL;
using SB.Financa.Generics.Extension;
using SB.Financa.Generics.Seguranca;
using SB.Financa.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SB.Financa.API.Business
{
    public class BUsuario
    {
        private readonly RepositorioUsuarioEF<Usuario> repository;
        public BUsuario(RepositorioUsuarioEF<Usuario> _repository) { repository = _repository; }

        public UsuarioLogado Autenticacao(string login, string senha)
        {
            Usuario usuarioApp = repository.ObterPorLogin(login);

            if (usuarioApp == null) return null;  

            if (Criptografia.CompararHashMd5(senha, usuarioApp.Senha))
            {
                string token = TokenService.GenerateToken(usuarioApp);
                return new UsuarioLogado() { Token = token, Login = usuarioApp.Login, Perfil = usuarioApp.Perfil };
            }
            else { return null;  } 
        }

        public List<UsuarioView> ObterTodos()
        {
            return repository.Todos.Select(u => u.ToView()).ToList();
        }

        public UsuarioView ObterPorId(int id)
        {
            return repository.ObterPorId(id).ToView();
        }

        public UsuarioView Incluir(UsuarioView usuarioView)
        {        
            if (ObterPorLogin(usuarioView.Login) != null) {
                throw new Exception($"O Login '{usuarioView.Login}' informado já existe, inclusão não permitida!");
            }

            Usuario usuario = ObterModel(usuarioView);
            repository.Incluir(usuario);

            return usuario.ToView();
        }


        public void Alterar(UsuarioView usuarioView)
        {
            Usuario usuario = ObterModel(usuarioView);

            if (usuario.Id == 0) {
                throw new Exception("Usuario não encontrado no banco de dados");
            }

            repository.DetachLocal(p => p.Id == usuario.Id);
            repository.Alterar(usuario);
        }

        public UsuarioView ObterPorLogin(string login)
        {
            return repository.ObterPorLogin(login).ToView();
        }

        public void Excluir(UsuarioView usuarioView)
        {
            if (usuarioView.Id == 0)
            {
                throw new Exception("Usuario não encontrado no banco de dados");
            }

            Usuario usuario = ObterModel(usuarioView);
            repository.Excluir(usuario);
        }

        private Usuario ObterModel(UsuarioView usuarioView)
        {
            return new Usuario
            {
                Id = usuarioView.Id,
                Login = usuarioView.Login,
                Senha = Criptografia.ObterHashMd5(usuarioView.Senha),
                //Perfil = usuarioView.Perfil.StringParaTipoPerfil()
            };
        }
    }
}
