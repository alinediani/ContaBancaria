using ContaBancaria.Models;

namespace ContaBancaria.Service
{
    public interface IContaService
    {
        Conta GetById(int id);
        IEnumerable<Conta> GetAll();
        void Deposito(int contaId, decimal valor);
        void Saque(int contaId, decimal valor);
        void Transferencia(int contaOrigemId, int contaDestinoId, decimal valor);
        IEnumerable<Lancamento> Extrato(int contaId);
    }
}
