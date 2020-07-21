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
    public class PlanejamentoController : ControllerBase
    {
        private readonly BPlanejamento business;

        public PlanejamentoController(IRepository<Planejamento> repository,
                                      IRepository<Etiqueta> repoEtiqueta)
        {
            business = new BPlanejamento(repository, repoEtiqueta);
        }

        [HttpGet]
        public IActionResult GetPlanejamento()
        {
            try
            {
                var planejamentos = business.ObterTodos();

                if (planejamentos == null || !planejamentos.Any())
                {
                    return NotFound(new { Mensagem = "Não existem planejamentos/ metas cadastradas no banco de dados." });
                }

                return Ok(planejamentos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = ex.Message.ToString() });
            }
        }


        [HttpGet("{id}")]
        public IActionResult GetPlanejamentoPorId(int id)
        {
            try
            {
                var planejamento = business.ObterPorId(id);

                if (planejamento == null)
                {
                    return NotFound(new { Mensagem = $"A Planejamento/ Meta id: '{id}' informada não existe no banco de dados." });
                }

                return Ok(planejamento);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = ex.Message.ToString() });
            }
        }

        [HttpPost]
        public IActionResult PostPlanejamento([FromBody] PlanejamentoView value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                else
                {
                    PlanejamentoView planejamento = business.Incluir(value);

                    var uri = Url.Action("GetPlanejamentoPorId", new { id = planejamento.Id });
                    return Created(uri, planejamento);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = ex.Message.ToString() });
            }
        }

        [HttpPut]
        public IActionResult PutPlanejamento([FromBody]PlanejamentoView value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                else
                {
                    var planejamento = business.ObterPorId(value.Id);
                    if (planejamento == null)
                    {
                        return NotFound(new { Mensagem = $"A meta/planejamento id: {value.Id} informada não existe no banco de dados." });
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
        public IActionResult DeletePlajemento(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                else
                {
                    var planDel = business.ObterPorId(id);

                    if (planDel == null)
                    {
                        return NotFound(new { Mensagem = $"A meta/ planejamento de id: '{id}' informada não existe no banco de dados." });
                    }

                    business.Excluir(planDel);
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