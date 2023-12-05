using ContaBancaria.Models;

namespace ContaBancaria.Repository
{
    public class ContaRepository : IContaRepository
    {
        private readonly AppDbContext _context;

        public ContaRepository(AppDbContext context)
        {
            _context = context;
        }

        public Conta GetById(int id)
        {
            return _context.Contas.FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Conta> GetAll()
        {
            return _context.Contas.ToList();
        }

        public void Add(Conta conta)
        {
            _context.Contas.Add(conta);
            _context.SaveChanges();
        }

        public void Update(Conta conta)
        {
            _context.Contas.Update(conta);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var conta = _context.Contas.FirstOrDefault(c => c.Id == id);
            if (conta != null)
            {
                _context.Contas.Remove(conta);
                _context.SaveChanges();
            }
        }
        public IEnumerable<Lancamento> GetLancamentosByContaId(int contaId)
        {
            return _context.Lancamentos.Where(l => l.ContaId == contaId).ToList();
        }
    }

}
