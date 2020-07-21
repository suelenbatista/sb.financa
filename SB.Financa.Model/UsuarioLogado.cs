using System;
using System.Collections.Generic;
using System.Text;

namespace SB.Financa.Model
{
    public class UsuarioLogado
    {
        public UsuarioLogado()
        {
            this.Token = "";
            this.Login = "";
            this.Perfil = "";
            this.Mensagem = "";
            this.StatusCode = "";
        }

        public string Token { get; set; }
        public string Login { get; set; }
        public string Perfil { get; set; }
        public string StatusCode { get; set; }
        public string Mensagem { get; set; }
    }
}
