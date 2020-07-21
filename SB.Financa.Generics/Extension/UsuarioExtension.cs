using SB.Financa.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SB.Financa.Generics.Extension
{
    public static class UsuarioExtension
    {
        public static UsuarioView ToView(this Usuario model)
        {
            return new UsuarioView
            {
                Id = model.Id,
                Login = model.Login,
                Senha = model.Senha,
                //Perfil = model.Perfil.TipoPerfilParaString()
            };
        }
    }
}
