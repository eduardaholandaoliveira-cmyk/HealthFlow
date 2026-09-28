namespace HealthFlow.Models
{
    public class ProfissionalModel
    {
        public int IdProfissional { get; set; }
        public string NomeProfissional { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public string Crm { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public int IdEspecialidade { get; set; }
    }
}
