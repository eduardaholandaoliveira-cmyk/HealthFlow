namespace HealthFlow.Models
{
    public class FinanceiroModel
    {
        public int IdFinanceiro { get; set; }
        public int IdPaciente { get; set; }
        public int IdAgendamento { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public string FormaPagamento { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}