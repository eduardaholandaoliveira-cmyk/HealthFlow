using System.ComponentModel.DataAnnotations;

namespace HealthFlow.Models
{
    public class AgendamentoModel
    {
        public int IdAgendamento { get; set; }

        [Required(ErrorMessage = "O paciente é obrigatório.")]
        public int IdPaciente { get; set; }

        [Required(ErrorMessage = "O profissional é obrigatório.")]
        public int IdProfissional { get; set; }

        [Required(ErrorMessage = "A data é obrigatória.")]
        public DateTime? DataAgendamento { get; set; }

        [Required(ErrorMessage = "A hora é obrigatória.")]
        public TimeOnly? HoraAgendamento { get; set; }

        [Required(ErrorMessage = "O motivo é obrigatório.")]
        [StringLength(200, ErrorMessage = "O motivo deve ter no máximo 200 caracteres.")]
        public string Motivo { get; set; } = string.Empty;

        [Required(ErrorMessage = "O status é obrigatório.")]
        [StringLength(30, ErrorMessage = "O status deve ter no máximo 30 caracteres.")]
        public string Status { get; set; } = string.Empty;

        public string NomePaciente { get; set; } = string.Empty;

        public string NomeProfissional { get; set; } = string.Empty;
    }
}