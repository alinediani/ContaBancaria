using ContaBancaria.Models;
using ContaBancaria.Repository;

namespace ContaBancaria.Service
{
    public class ContaService : IContaService
    {
        private readonly IContaRepository _contaRepository;

        public ContaService(IContaRepository contaRepository)
        {
            _contaRepository = contaRepository;
        }

        public Conta GetById(int id)
        {
            return _contaRepository.GetById(id);
        }

        public IEnumerable<Conta> GetAll()
        {
            return _contaRepository.GetAll();
        }

        public void Deposito(int contaId, decimal valor)
        {
            if (valor <= 0)
            {
                throw new ArgumentException("O valor do depósito deve ser maior que zero.", nameof(valor));
            }

            var conta = _contaRepository.GetById(contaId);
            if (conta != null)
            {
                conta.Saldo += valor * 0.99m; 
                _contaRepository.Update(conta);
            }
        }

        public void Saque(int contaId, decimal valor)
        {
            if (valor <= 0)
            {
                throw new ArgumentException("O valor do saque deve ser maior que zero.", nameof(valor));
            }

            var conta = _contaRepository.GetById(contaId);
            if (conta != null && conta.Saldo >= valor + 4m)
            {
                conta.Saldo -= valor + 4m;
                _contaRepository.Update(conta);
            }
            else
            {
                throw new InvalidOperationException("Saldo insuficiente para realizar o saque.");
            }
        }

        public void Transferencia(int contaOrigemId, int contaDestinoId, decimal valor)
        {
            if (valor <= 0)
            {
                throw new ArgumentException("O valor da transferência deve ser maior que zero.", nameof(valor));
            }

            var contaOrigem = _contaRepository.GetById(contaOrigemId);
            var contaDestino = _contaRepository.GetById(contaDestinoId);

            if (contaOrigem == null || contaDestino == null)
            {
                throw new InvalidOperationException("Conta de origem ou conta de destino não encontrada.");
            }

            if (contaOrigem.Saldo >= valor + 1m)
            {
                contaOrigem.Saldo -= valor + 1m;
                contaDestino.Saldo += valor;

                _contaRepository.Update(contaOrigem);
                _contaRepository.Update(contaDestino);
            }
            else
            {
                throw new InvalidOperationException("Saldo insuficiente para realizar a transferência.");
            }
        }

        public IEnumerable<Lancamento> Extrato(int contaId)
        {
            return _contaRepository.GetLancamentosByContaId(contaId);
        }
    }
}
