using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SB.Financa.API.Business;
using SB.Financa.DAL;
using SB.Financa.Model;

namespace SB.Financa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly BUsuario business;

        public UsuarioController(RepositorioUsuarioEF<Usuario> repository)
        {
            business = new BUsuario(repository);
        }

        [HttpGet]
        public IActionResult GetUsuarios()
        {
            try
            {
                var usuarios = business.ObterTodos();

                if (usuarios == null || !usuarios.Any())
                {
                    return NotFound(new { Mensagem = "Não existem usuários cadastradas no banco de dados." });
                }

                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = ex.Message.ToString() });
            }
        }

        [HttpGet("{login}")]
        public IActionResult GetUsuarioPorLogin(string login)
        {
            try
            {
                var usuario = business.ObterPorLogin(login);

                if (usuario == null)
                {
                    return NotFound(new { Mensagem = $"O usuário login: {login} informado não existe no banco de dados." });
                }

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = ex.Message.ToString() });
            }
        }


        [HttpPost]
        public IActionResult PostUsuario([FromBody] UsuarioView value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                else
                {
                    UsuarioView userCadastrado = business.Incluir(value);

                    var uri = Url.Action("GetUsuarioPorId", new { id = userCadastrado.Id });
                    return Created(uri, userCadastrado);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = ex.Message.ToString() });
            }
        }

        [HttpPut]
        public IActionResult PutUsuario([FromBody] UsuarioView value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                else
                {
                    var usuario = business.ObterPorId(value.Id);
                    if (usuario == null) {
                        return NotFound(new { Mensagem = $"O usuario id: '{value.Id}' informado não existe no banco de dados." });
                    }

                    business.Alterar(usuario);
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = ex.Message.ToString() });
            }
        }

        [HttpDelete("{login}")]
        public IActionResult DeleteUsuario(string login)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                else
                {
                    var userDel = business.ObterPorLogin(login);

                    if (userDel == null) {
                        return NotFound(new { Mensagem = $"O usuário login: '{login}' informado não existe no banco de dados." });
                    }

                    business.Excluir(userDel);
                    return NoContent(); //204
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = ex.Message.ToString() });
            }
        }
    }
}