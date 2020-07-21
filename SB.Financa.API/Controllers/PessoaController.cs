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
    [Authorize]
    public class PessoaController : ControllerBase
    {
        private readonly BPessoa business;
        
        public PessoaController(IRepository<Pessoa> repository)
        {
            business = new BPessoa(repository);
        }

        [HttpGet]
        public IActionResult GetPessoas()
        {
            try
            {
                var pessoas = business.ObterTodos();

                if (pessoas == null || !pessoas.Any())
                {
                    return NotFound(new { Mensagem = "Não existem pessoas cadastradas no banco de dados." });
                }

                return Ok(pessoas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = ex.Message.ToString() });
            }
        }


        [HttpGet("{id}")]
        public IActionResult GetPessoaPorId(int id)
        {
            try
            {
                var pessoa = business.ObterPorId(id);

                if (pessoa == null)
                {
                    return NotFound(new { Mensagem = $"A pessoa id: {id} informada não existe no banco de dados." });
                }

                return Ok(pessoa);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = ex.Message.ToString() });
            }
        }

        [HttpPost]
        public IActionResult PostPessoa([FromBody] PessoaView value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }    
                else 
                {
                    PessoaView pesCadastrada = business.Incluir(value);

                    var uri = Url.Action("GetPessoaPorId", new { id = pesCadastrada.Id });
                    return Created(uri, pesCadastrada);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = ex.Message.ToString() });
            }
        }

        [HttpPut]
        public IActionResult PutPessoa([FromBody] PessoaView value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                else
                {
                    var pessoa = business.ObterPorId(value.Id);
                    if (pessoa == null)
                    {
                        return NotFound(new { Mensagem = $"A pessoa id: {value.Id} informada não existe no banco de dados." });
                    }

                    business.Alterar(value);
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = ex.Message.ToString() });
            }
        }

        [HttpPut("{id}")]
        public IActionResult PutInativar(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                else
                {
                    var pessoa = business.ObterPorId(id);
                    if (pessoa == null)
                    {
                        return NotFound(new { Mensagem = $"A pessoa id: {id} informada não existe no banco de dados." });
                    }

                    /* Trocar o ativar ou inativar */
                    bool bAtivarInativar = (pessoa.Ativo ? false : true);

                    pessoa.Ativo = bAtivarInativar;
                    business.Alterar(pessoa);

                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = ex.Message.ToString() });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePessoa(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                else
                {
                    var pesDel = business.ObterPorId(id);

                    if (pesDel == null)
                    {
                        return NotFound(new { Mensagem = $"A pessoa id: {id} informada não existe no banco de dados." });
                    }

                  
                    business.Excluir(pesDel);
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