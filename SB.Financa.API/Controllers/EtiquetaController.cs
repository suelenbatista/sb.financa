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
    public class EtiquetaController : ControllerBase
    {
        private readonly BEtiqueta business;
        private readonly BConsultaMovimento businessMovConsulta;

        public EtiquetaController(IRepository<Etiqueta> repository,
                                  IRepository<Movimento> repositoryMov)
        {
            business = new BEtiqueta(repository);
            businessMovConsulta = new BConsultaMovimento(repositoryMov); 
        }
        
        [HttpGet]
        public IActionResult GetEtiqueta()
        {
            try
            {
                var etiquetas = business.ObterTodos();

                if (etiquetas == null || !etiquetas.Any())
                {
                    return NotFound(new { Mensagem = "Não existem etiquetas cadastradas no banco de dados." });
                }

                return Ok(etiquetas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = ex.Message.ToString() });
            }
        }


        [HttpGet("{id}")]
        public IActionResult GetEtiquetaPorId(int id)
        {
            try
            {
                var etiqueta = business.ObterPorId(id);

                if (etiqueta == null)
                {
                    return NotFound(new { Mensagem = $"A Etiqueta id: '{id}' informada não existe no banco de dados." });
                }

                return Ok(etiqueta);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = ex.Message.ToString() });
            }
        }

        [HttpPost]
        public IActionResult PostEtiqueta([FromBody] EtiquetaView value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                else
                {
                    EtiquetaView etiqueta = business.Incluir(value);

                    var uri = Url.Action("GetEtiquetaPorId", new { id = etiqueta.Id });
                    return Created(uri, etiqueta);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = ex.Message.ToString() });
            }
        }

        [HttpPut]
        public IActionResult PutEtiqueta([FromBody]EtiquetaView value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                else
                {
                    var etiqueta = business.ObterPorId(value.Id);
                    if (etiqueta == null)
                    {
                        return NotFound(new { Mensagem = $"O cartão id: {value.Id} informado não existe no banco de dados." });
                    }

                    business.Alterar(value);
                    return Ok(etiqueta);
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
                    var etiqueta = business.ObterPorId(id);

                    if (etiqueta == null)
                    {
                        return NotFound(new { Mensagem = $"A conta etiqueta id: '{id}' informada não existe no banco de dados." });
                    }

                    if (businessMovConsulta.ExisteMovimentoParaEtiqueta(etiqueta.Id))
                    {
                        return StatusCode(500,
                        new
                        {
                            Mensagem = $"Não é permitido deletar a etiqueta id '{id}' pois existe(m) movimento(s) de conta(s) associado(s) a ela."
                        });
                    }

                    business.Excluir(etiqueta);
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