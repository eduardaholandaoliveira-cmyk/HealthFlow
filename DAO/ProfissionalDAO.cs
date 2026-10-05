using AppWebExemplo.Configs;
using HealthFlow.Models;

namespace HealthFlow.DAO
{
    public class ProfissionalDAO
    {
        private readonly Conexao _conexao;

        public ProfissionalDAO(Conexao conexao)
        {
            _conexao = conexao;
        }

        public List<ProfissionalModel> Listar()
        {
            var lista = new List<ProfissionalModel>();

            using var con = _conexao.GetConnection();
            using var comando = con.CreateCommand();

            comando.CommandText = @"
                SELECT
                    p.id_profissional,
                    p.nome_profissional,
                    p.cpf,
                    p.crm,
                    p.email,
                    p.telefone,
                    p.id_especialidade,
                    e.nome_especialidade
                FROM Profissional p
                LEFT JOIN Especialidade e
                    ON p.id_especialidade = e.id_especialidade
                ORDER BY p.nome_profissional";

            using var leitor = comando.ExecuteReader();

            while (leitor.Read())
            {
                lista.Add(new ProfissionalModel
                {
                    IdProfissional = leitor.GetInt32("id_profissional"),
                    NomeProfissional = leitor.GetString("nome_profissional"),
                    Cpf = leitor.GetString("cpf"),
                    Crm = leitor.GetString("crm"),
                    Email = leitor.GetString("email"),
                    Telefone = leitor.GetString("telefone"),
                    IdEspecialidade = leitor.GetInt32("id_especialidade"),
                    NomeEspecialidade = leitor.IsDBNull(
                        leitor.GetOrdinal("nome_especialidade"))
                        ? ""
                        : leitor.GetString("nome_especialidade")
                });
            }

            return lista;
        }

        public void Inserir(ProfissionalModel profissional)
        {
            using var con = _conexao.GetConnection();

            using var comando = con.CreateCommand();

            comando.CommandText = @"
                INSERT INTO Profissional
                (
                    id_profissional,
                    nome_profissional,
                    cpf,
                    crm,
                    email,
                    telefone,
                    id_especialidade
                )
                VALUES
                (
                    @id,
                    @nome,
                    @cpf,
                    @crm,
                    @email,
                    @telefone,
                    @especialidade
                )";

            comando.Parameters.AddWithValue(
                "@id",
                ObterProximoId()
            );

            comando.Parameters.AddWithValue(
                "@nome",
                profissional.NomeProfissional
            );

            comando.Parameters.AddWithValue(
                "@cpf",
                profissional.Cpf
            );

            comando.Parameters.AddWithValue(
                "@crm",
                profissional.Crm
            );

            comando.Parameters.AddWithValue(
                "@email",
                profissional.Email
            );

            comando.Parameters.AddWithValue(
                "@telefone",
                profissional.Telefone
            );

            comando.Parameters.AddWithValue(
                "@especialidade",
                profissional.IdEspecialidade
            );

            comando.ExecuteNonQuery();
        }

        public string Excluir(int id)
        {
            using var con = _conexao.GetConnection();

            using var verificar = con.CreateCommand();

            verificar.CommandText = @"
                SELECT COUNT(*)
                FROM Agendamento
                WHERE id_profissional = @id";

            verificar.Parameters.AddWithValue("@id", id);

            int quantidade = Convert.ToInt32(
                verificar.ExecuteScalar()
            );

            if (quantidade > 0)
            {
                return "Não é possível excluir este profissional porque ele possui agendamento cadastrado.";
            }

            using var comando = con.CreateCommand();

            comando.CommandText = @"
                DELETE FROM Profissional
                WHERE id_profissional = @id";

            comando.Parameters.AddWithValue("@id", id);

            comando.ExecuteNonQuery();

            return "Profissional excluído com sucesso!";
        }

        private int ObterProximoId()
        {
            using var con = _conexao.GetConnection();

            using var comando = con.CreateCommand();

            comando.CommandText = @"
                SELECT COALESCE(MAX(id_profissional), 0) + 1
                FROM Profissional";

            return Convert.ToInt32(
                comando.ExecuteScalar()
            );
        }
    }
}