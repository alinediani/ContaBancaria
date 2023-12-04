using ContaBancaria.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContaBancaria.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ContasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("deposito")]
        public IActionResult Deposito(int contaId, decimal valor)
        {
            Conta conta = _context.Contas.Find(contaId);

            if (conta == null)
            {
                return NotFound("Conta não encontrada");
            }

            var taxa = valor * 0.01m;
            var valorDepositado = valor - taxa;

            conta.Saldo += valorDepositado;

            Lancamento lancamento = new Lancamento
            {
                ContaId = conta.Id,
                Valor = valorDepositado,
                Data = DateTime.Now,
                TipoOperacao = TipoOperacao.Deposito
            };

            _context.Lancamentos.Add(lancamento);
            _context.SaveChanges();

            return Ok("Depósito realizado com sucesso");
        }

        [HttpPost("saque")]
        public IActionResult Saque(int contaId, decimal valor)
        {
            Conta conta = _context.Contas.Find(contaId);

            if (conta == null)
            {
                return NotFound("Conta não encontrada");
            }

            if (conta.Saldo < valor)
            {
                return BadRequest("Saldo insuficiente para realizar o saque");
            }

            var valorComTaxa = valor - 4.00m;

            conta.Saldo -= valorComTaxa;

            Lancamento lancamento = new Lancamento
            {
                ContaId = conta.Id,
                Valor = -valorComTaxa,
                Data = DateTime.Now,
                TipoOperacao = TipoOperacao.Saque
            };

            _context.Lancamentos.Add(lancamento);
            _context.SaveChanges();

            return Ok("Saque realizado com sucesso");
        }

        [HttpPost("transferencia")]
        public IActionResult Transferencia(int contaOrigemId, int contaDestinoId, decimal valor)
        {
            Conta contaOrigem = _context.Contas.Find(contaOrigemId);
            Conta contaDestino = _context.Contas.Find(contaDestinoId);

            if (contaOrigem == null || contaDestino == null)
            {
                return NotFound("Conta(s) não encontrada(s)");
            }

            if (contaOrigem.Saldo < valor)
            {
                return BadRequest("Saldo insuficiente para realizar a transferência");
            }

            var valorComTaxa = valor - 1.00m;

            contaOrigem.Saldo -= valorComTaxa;
            contaDestino.Saldo += valor;

            Lancamento lancamentoOrigem = new Lancamento
            {
                ContaId = contaOrigem.Id,
                Valor = -valorComTaxa,
                Data = DateTime.Now,
                TipoOperacao = TipoOperacao.Transferencia
            };

            Lancamento lancamentoDestino = new Lancamento
            {
                ContaId = contaDestino.Id,
                Valor = valor,
                Data = DateTime.Now,
                TipoOperacao = TipoOperacao.Transferencia
            };

            _context.Lancamentos.AddRange(lancamentoOrigem, lancamentoDestino);
            _context.SaveChanges();

            return Ok("Transferência realizada com sucesso");
        }

        [HttpGet("extrato/{contaId}")]
        public IActionResult Extrato(int contaId)
        {
            Conta conta = _context.Contas.Find(contaId);

            if (conta == null)
            {
                return NotFound("Conta não encontrada");
            }

            List<Lancamento> extrato = _context.Lancamentos
                .Where(l => l.ContaId == conta.Id)
                .OrderByDescending(l => l.Data)
                .ToList();

            return Ok(extrato);
        }
    }
}
