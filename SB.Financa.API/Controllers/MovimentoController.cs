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
    public class MovimentoController : ControllerBase
    {
        private readonly BMovimento business;
        private readonly BMovimentoBaixa businessMovBaixa;

        public MovimentoController(IRepository<Movimento> repository,  IRepository<MovimentoBaixa> repositoryMovBaixa, IRepository<ContaBancaria> repoContaBancaria,
                IRepository<Etiqueta> repoEtiqueta, IRepository<Pessoa> repoPessoa,  IRepository<ContaCartao> repoContaCartao)
        {
            business = new BMovimento(repository, repoEtiqueta, repoPessoa, repoContaCartao);
            businessMovBaixa = new BMovimentoBaixa(repositoryMovBaixa, repository, repoContaBancaria);
        }

        [HttpGet]
        public IActionResult GetMovimentos()
        {
            try
            {
                var movimentos = business.ObterTodos();

                if (movimentos == null || !movimentos.Any())
                {
                    return NotFound(new { Mensagem = "Não existem movimentos cadastrados no banco de dados." });
                }

                return Ok(movimentos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = ex.Message.ToString() });
            }
        }


        [HttpGet("{id}")]
        public IActionResult GetMovimentoPorId(int id)
        {
            try
            {
                var movimento = business.ObterPorId(id);

                if (movimento == null)
                {
                    return NotFound(new { Mensagem = $"O movimento id: '{id}' informado não existe no banco de dados." });
                }

                return Ok(movimento);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = ex.Message.ToString() });
            }
        }

        [HttpPost]
        public IActionResult PostMovimento([FromBody] MovimentoView value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                else
                {
                    MovimentoView movimento = business.Incluir(value);

                    var uri = Url.Action("GetMovimentoPorId", new { id = movimento.Id });
                    return Created(uri, movimento);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = ex.Message.ToString() });
            }
        }

        [HttpPut]
        public IActionResult PutMovimento([FromBody]MovimentoView value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                else
                {
                    var movimento = business.ObterPorId(value.Id);
                    if (movimento == null)
                    {
                        return NotFound(new { Mensagem = $"O movimento id: {value.Id} informado não existe no banco de dados." });
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
        public IActionResult DeleteMovimento(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                else
                {
                    var movimento = business.ObterPorId(id);

                    if (movimento == null)
                    {
                        return NotFound(new { Mensagem = $"O movimento de id: '{id}' informado não existe no banco de dados." });
                    }

                    business.Excluir(movimento);
                    return NoContent(); //204
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = ex.Message.ToString() });
            }
        }

        #region [Controle de Baixa de Movimentos ....]


        [HttpGet("{id}/baixa")]
        public IActionResult GetBaixaPorMovimento(int id)
        {
            try
            {

                var movimento = business.ObterPorId(id);
                if (movimento == null)
                {
                    return NotFound(new { Mensagem = $"O movimento id: {id} informado não existe no banco de dados." });
                }


                var baixasMovimento = businessMovBaixa.ObterPorMovimentoId(id);
                if (baixasMovimento == null)
                {
                    return NotFound(new { Mensagem = $"Não existem baixas no banco de dados para o movimento id: '{id}'." });
                }

                return Ok(baixasMovimento);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = ex.Message.ToString() });
            }
        }

        [HttpGet("{id}/baixa/{idBaixa}")]
        public IActionResult GetBaixaPorId(int id, int idBaixa)
        {
            try
            {
                var movimento = business.ObterPorId(id);
                if (movimento == null)
                {
                    return NotFound(new { Mensagem = $"O movimento id: {id} informado não existe no banco de dados." });
                }

                var baixa = businessMovBaixa.ObterPorId(idBaixa);
                if (baixa == null)
                {
                    return NotFound(new { Mensagem = $"Não existem baixas no banco de dados para o id: '{idBaixa}' informado." });
                }

                return Ok(baixa);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = ex.Message.ToString() });
            }
        }

        [HttpPost("{id}/baixa/")]
        public IActionResult PostBaixaMovimento(int id, [FromBody] MovimentoBaixaView model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                else
                {
                    if (!id.Equals(model.MovimentoId))
                    {
                        return NotFound(new { Mensagem = $"O movimento id: {id} informado corresponde com o id informado no body {model.MovimentoId}." });
                    }

                    var movimento = business.ObterPorId(id);
                    if (movimento == null)
                    {
                        return NotFound(new { Mensagem = $"O movimento id: {id} informado não existe no banco de dados." });
                    }

                    MovimentoBaixaView movBaixa = businessMovBaixa.Incluir(model);  
                    var uri = Url.Action("GetBaixaPorId", new { id = movBaixa.Id });
                    return Created(uri, movBaixa);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = ex.Message.ToString() });
            }
        }

        [HttpDelete("{id}/baixa/{idBaixa}")]
        public IActionResult DeleteBaixaMov(int id, int idBaixa)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                else
                {
                    var movimento = business.ObterPorId(id);
                    if (movimento == null)
                    {
                        return NotFound(new { Mensagem = $"O movimento de id: '{id}' informado não existe no banco de dados." });
                    }

                    var movBaixa = businessMovBaixa.ObterPorId(idBaixa);
                    if (movBaixa == null)
                    {
                        return NotFound(new { Mensagem = $"A baixa de id: '{idBaixa}' informado não existe no banco de dados." });
                    }

                    businessMovBaixa.Excluir(movBaixa);
                    return NoContent(); //204
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = ex.Message.ToString() });
            }
        }

        #endregion [... Controle de Baixa de Movimentos]
    }
}