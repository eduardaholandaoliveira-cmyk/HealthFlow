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

        public void Inserir(PacienteModel paciente)
        {
            using var con = _conexao.GetConnection();

            string sql = @"
                INSERT INTO Paciente
                (
                    id_paciente,
                    nome_paciente,
                    cpf,
                    data_nascimento,
                    sexo,
                    email,
                    telefone,
                    endereco
                )
                VALUES
                (
                    @id,
                    @nome,
                    @cpf,
                    @data,
                    @sexo,
                    @email,
                    @telefone,
                    @endereco
                )";

            using var comando = con.CreateCommand();

            comando.CommandText = sql;

            comando.Parameters.AddWithValue("@id", ObterProximoId());
            comando.Parameters.AddWithValue("@nome", paciente.NomePaciente);
            comando.Parameters.AddWithValue("@cpf", paciente.Cpf);
            comando.Parameters.AddWithValue(
                "@data",
                paciente.DataNascimento!.Value
            );
            comando.Parameters.AddWithValue("@sexo", paciente.Sexo);
            comando.Parameters.AddWithValue("@email", paciente.Email);
            comando.Parameters.AddWithValue("@telefone", paciente.Telefone);
            comando.Parameters.AddWithValue("@endereco", paciente.Endereco);

            comando.ExecuteNonQuery();
        }

        private int ObterProximoId()
        {
            using var con = _conexao.GetConnection();
            using var comando = con.CreateCommand();

            comando.CommandText = @"
                SELECT COALESCE(MAX(id_paciente), 0) + 1
                FROM Paciente";

            return Convert.ToInt32(
                comando.ExecuteScalar()
            );
        }
    }
}