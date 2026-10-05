using System.ComponentModel.DataAnnotations;

namespace HealthFlow.Models
{
    public class ProfissionalModel
    {
        public int IdProfissional { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(150, ErrorMessage = "O nome deve ter no máximo 150 caracteres.")]
        public string NomeProfissional { get; set; } = string.Empty;

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [StringLength(14, ErrorMessage = "O CPF deve ter no máximo 14 caracteres.")]
        public string Cpf { get; set; } = string.Empty;

        [Required(ErrorMessage = "O CRM é obrigatório.")]
        [StringLength(30, ErrorMessage = "O CRM deve ter no máximo 30 caracteres.")]
        public string Crm { get; set; } = string.Empty;

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [StringLength(100, ErrorMessage = "O e-mail deve ter no máximo 100 caracteres.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "O telefone é obrigatório.")]
        [StringLength(20, ErrorMessage = "O telefone deve ter no máximo 20 caracteres.")]
        public string Telefone { get; set; } = string.Empty;

        [Required(ErrorMessage = "A especialidade é obrigatória.")]
        public int IdEspecialidade { get; set; }

        public string NomeEspecialidade { get; set; } = string.Empty;
    }
}