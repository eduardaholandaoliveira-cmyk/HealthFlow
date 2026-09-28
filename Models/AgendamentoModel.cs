namespace HealthFlow.Models
{
    public class AgendamentoModel
    {
        public int IdAgendamento { get; set; }
        public int IdPaciente { get; set; }
        public int IdProfissional { get; set; }
        public DateTime DataAgendamento { get; set; }
        public TimeSpan HoraAgendamento { get; set; }
        public string Motivo { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}