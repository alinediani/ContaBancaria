using ContaBancaria.Models;

namespace ContaBancaria.Repository
{
    public interface IContaRepository
    {
        Conta GetById(int id);
        IEnumerable<Conta> GetAll();
        void Add(Conta conta);
        void Update(Conta conta);
        void Delete(int id);
        IEnumerable<Lancamento> GetLancamentosByContaId(int contaId);
    }
}
