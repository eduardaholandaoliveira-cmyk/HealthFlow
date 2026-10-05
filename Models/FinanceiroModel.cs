using System.ComponentModel.DataAnnotations;

namespace HealthFlow.Models
{
    public class FinanceiroModel
    {
        public int IdFinanceiro { get; set; }

        public int? IdPaciente { get; set; }

        public int? IdAgendamento { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória.")]
        public string Descricao { get; set; } = string.Empty;

        [Required(ErrorMessage = "O valor é obrigatório.")]
        public decimal Valor { get; set; }

        public string FormaPagamento { get; set; } = "Não informado";

        public string Status { get; set; } = "Recebido";
    }
}