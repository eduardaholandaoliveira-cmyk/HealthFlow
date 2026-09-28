namespace HealthFlow.Models
{
    public class ProntuarioModel
    {
        public int IdProntuario { get; set; }
        public int IdPaciente { get; set; }
        public string TipoSanguineo { get; set; } = string.Empty;
        public string Alergias { get; set; } = string.Empty;
        public string Medicamentos { get; set; } = string.Empty;
        public string Historico { get; set; } = string.Empty;
        public string Observacoes { get; set; } = string.Empty;
    }
}