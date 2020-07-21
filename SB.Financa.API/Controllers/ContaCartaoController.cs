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
    public class ContaCartaoController : ControllerBase
    {
        private readonly BContaCartao business;
        private readonly BConsultaMovimento businessMovConsulta;

        public ContaCartaoController(IRepository<ContaCartao> repository,
                                     IRepository<Movimento> repositoryMov)
        {
            business = new BContaCartao(repository);
            businessMovConsulta = new BConsultaMovimento(repositoryMov);
        }

        [HttpGet]
        public IActionResult GetCartaos()
        {
            try
            {
                var cartoes = business.ObterTodos();

                if (cartoes == null || !cartoes.Any())
                {
                    return NotFound(new { Mensagem = "Não existem cartões cadastradas no banco de dados." });
                }

                return Ok(cartoes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = ex.Message.ToString() });
            }
        }


        [HttpGet("{id}")]
        public IActionResult GetCartaoPorId(int id)
        {
            try
            {
                var cartao = business.ObterPorId(id);

                if (cartao == null)
                {
                    return NotFound(new { Mensagem = $"O Cartão id: '{id}' informado não existe no banco de dados." });
                }

                return Ok(cartao);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = ex.Message.ToString() });
            }
        }

        [HttpPost]
        public IActionResult PostCartao([FromBody] ContaCartaoView value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                else
                {
                    ContaCartaoView cartaoCadastrado = business.Incluir(value);

                    var uri = Url.Action("GetCartaoPorId", new { id = cartaoCadastrado.Id });
                    return Created(uri, cartaoCadastrado);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = ex.Message.ToString() });
            }
        }

        [HttpPut]
        public IActionResult PutCartao([FromBody]ContaCartaoView value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                else
                {
                    var cartao = business.ObterPorId(value.Id);
                    if (cartao == null)
                    {
                        return NotFound(new { Mensagem = $"O cartão id: {value.Id} informado não existe no banco de dados." });
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

        [HttpDelete("{id}")]
        public IActionResult DeleteCartao(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                else
                {
                    var cartaoDel = business.ObterPorId(id);

                    if (cartaoDel == null)
                    {
                        return NotFound(new { Mensagem = $"A conta cartão id: '{id}' informada não existe no banco de dados." });
                    }

                    if (businessMovConsulta.ExisteMovimentoParaContaCartao(cartaoDel.Id))
                    {
                        return StatusCode(500,
                        new
                        {
                            Mensagem = $"Não é permitido deletar a conta cartõa id '{id}' pois existe(m) movimento(s) de conta(s) associado(s) a ela."
                        });
                    }

                    business.Excluir(cartaoDel);
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