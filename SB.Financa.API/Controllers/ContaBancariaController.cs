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
    public class ContaBancariaController : ControllerBase
    {
        private readonly BContaBancaria business;

        public ContaBancariaController(IRepository<ContaBancaria> repository)
        {
            business = new BContaBancaria(repository);
        }

        [HttpGet]
        public IActionResult GetContaBancaria()
        {
            try
            {
                var contas = business.ObterTodos();

                if (contas == null || !contas.Any())
                {
                    return NotFound(new { Mensagem = "Não existem contas bancárias cadastradas no banco de dados." });
                }

                return Ok(contas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = ex.Message.ToString() });
            }
        }


        [HttpGet("{id}")]
        public IActionResult GetContaBancariaPorId(int id)
        {
            try
            {
                var conta = business.ObterPorId(id);

                if (conta == null)
                {
                    return NotFound(new { Mensagem = $"A Conta Bancaria id: '{id}' informada não existe no banco de dados." });
                }

                return Ok(conta);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = ex.Message.ToString() });
            }
        }

        [HttpPost]
        public IActionResult PostContaBancaria([FromBody] ContaBancariaView value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                else
                {
                    ContaBancariaView contaCadastrada = business.Incluir(value);

                    var uri = Url.Action("GetContaBancariaPorId", new { id = contaCadastrada.Id });
                    return Created(uri, contaCadastrada);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = ex.Message.ToString() });
            }
        }

        [HttpPut]
        public IActionResult PutContaBancaria([FromBody]ContaBancariaView value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                else
                {
                    var conta = business.ObterPorId(value.Id);
                    if (conta == null)
                    {
                        return NotFound(new { Mensagem = $"A conta bancária id: {value.Id} informada não existe no banco de dados." });
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
        public IActionResult DeleteContaBancaria(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                else
                {
                    var contaDel = business.ObterPorId(id);

                    if (contaDel == null)
                    {
                        return NotFound(new { Mensagem = $"A conta bancária de id: '{id}' informada não existe no banco de dados." });
                    }

                    business.Excluir(contaDel);
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