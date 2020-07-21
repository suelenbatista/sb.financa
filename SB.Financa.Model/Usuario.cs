using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SB.Financa.Model
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }

        public string Perfil { get; set; }
    }


    public class UsuarioView
    {
        public int Id { get; set; }
        public string Login { get; set; }

        public string Senha { get; set; }
        public string Perfil { get; set; }
    }
}
