using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SB.Financa.API.Business;
using SB.Financa.DAL;
using SB.Financa.Model;

namespace SB.Financa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly BUsuario business;

        public LoginController(RepositorioUsuarioEF<Usuario> repository)
        {
            business = new BUsuario(repository);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult PostAutenticacao([FromBody]UsuarioView usuario)
        {
            if (!ModelState.IsValid) { return BadRequest(); }

            UsuarioLogado usuarioLogado = business.Autenticacao(usuario.Login, usuario.Senha);

            if (usuarioLogado == null) {
                return NotFound(new { message = "Usuário ou senha inválidos" }); }
               
            return Ok(usuarioLogado);
        }

        [HttpGet]
        [Route("anonimo")]
        [AllowAnonymous]
        public string Anonymous() => "Anônimo";

        [HttpGet]
        [Route("autenticado")]
        [Authorize]
        
        public string Authenticated() => String.Format("Autenticado - {0}", User.Identity.Name);

        [HttpGet]
        [Route("administrador")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public string Administrator() => "administrador";

        [HttpGet]
        [Route("controle")]
        [Authorize(Roles = "CONTROLE")]
        public string Control() => "controlador";
    }
}