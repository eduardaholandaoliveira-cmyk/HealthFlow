using AppWebExemplo.Configs;
using HealthFlow.Models;

namespace HealthFlow.DAO
{
    public class PacienteDAO
    {
        private readonly Conexao _conexao;

        public PacienteDAO(Conexao conexao)
        {
            _conexao = conexao;
        }

        public List<PacienteModel> Listar()
        {
            var lista = new List<PacienteModel>();

            using var con = _conexao.GetConnection();
            using var comando = con.CreateCommand();

            comando.CommandText = "SELECT * FROM Paciente";

            using var leitor = comando.ExecuteReader();

            while (leitor.Read())
            {
                lista.Add(new PacienteModel
                {
                    IdPaciente = leitor.GetInt32("id_paciente"),
                    NomePaciente = leitor.GetString("nome_paciente"),
                    Cpf = leitor.GetString("cpf"),
                    DataNascimento = leitor.GetDateTime("data_nascimento"),
                    Sexo = leitor.GetString("sexo"),
                    Email = leitor.GetString("email"),
                    Telefone = leitor.GetString("telefone"),
                    Endereco = leitor.GetString("endereco")
                });
            }

            return lista;
        }
    }
}