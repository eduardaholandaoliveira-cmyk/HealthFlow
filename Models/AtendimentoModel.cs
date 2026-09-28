namespace HealthFlow.Models
{
    public class AtendimentoModel
    {
        public int IdAtendimento { get; set; }
        public int IdAgendamento { get; set; }
        public string Queixa { get; set; } = string.Empty;
        public string Diagnostico { get; set; } = string.Empty;
        public string Tratamento { get; set; } = string.Empty;
        public string Observacoes { get; set; } = string.Empty;
    }
}