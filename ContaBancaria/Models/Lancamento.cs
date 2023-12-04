namespace ContaBancaria.Models
{
    public class Lancamento
    {
        public int Id { get; set; }
        public int ContaId { get; set; }
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }
        public TipoOperacao TipoOperacao { get; set; }
    }
}
