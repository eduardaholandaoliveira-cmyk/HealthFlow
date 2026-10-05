using System.ComponentModel.DataAnnotations;

namespace HealthFlow.Models
{
    public class EspecialidadeModel
    {
        public int IdEspecialidade { get; set; }

        [Required(ErrorMessage = "O nome da especialidade é obrigatório.")]
        [StringLength(100)]
        public string NomeEspecialidade { get; set; } = string.Empty;

        public string Descricao { get; set; } = string.Empty;
    }
}
