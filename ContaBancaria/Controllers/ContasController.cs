using ContaBancaria.Models;
using ContaBancaria.Service;
using Microsoft.AspNetCore.Mvc;

namespace ContaBancaria.Controllers
{
    [ApiController]
    [Route("api/contas")]
    public class ContasController : ControllerBase
    {
        private readonly IContaService _contaService;

        public ContasController(IContaService contaService)
        {
            _contaService = contaService;
        }

        [HttpPost("deposito/{contaId}/{valor}")]
        public IActionResult Deposito(int contaId, decimal valor)
        {
            try
            {
                _contaService.Deposito(contaId, valor);
                return Ok("Depósito realizado com sucesso");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("saque/{contaId}/{valor}")]
        public IActionResult Saque(int contaId, decimal valor)
        {
            try
            {
                _contaService.Saque(contaId, valor);
                return Ok("Saque realizado com sucesso");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("transferencia/{contaOrigemId}/{contaDestinoId}/{valor}")]
        public IActionResult Transferencia(int contaOrigemId, int contaDestinoId, decimal valor)
        {
            try
            {
                _contaService.Transferencia(contaOrigemId, contaDestinoId, valor);
                return Ok("Transferência realizada com sucesso");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("extrato/{contaId}")]
        public IActionResult Extrato(int contaId)
        {
            List<Lancamento> extrato = (List<Lancamento>)_contaService.Extrato(contaId);
            return Ok(extrato);
        }
    }
}
