namespace HealthFlow.Models
{
    public class Vendam
    {
        public int IdVenda { get; set; }

        public DateTime DataHora { get; set; }

        public string TipoPagamento { get; set; } = string.Empty;

        public double ValorTotal { get; set; }

        public int FkIdCliente { get; set; }

        public int FkIdCaixa { get; set; }
    }
}
