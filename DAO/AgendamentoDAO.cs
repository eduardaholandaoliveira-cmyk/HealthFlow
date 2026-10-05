using AppWebExemplo.Configs;
using HealthFlow.Models;

namespace HealthFlow.DAO
{
    public class AgendamentoDAO
    {
        private readonly Conexao _conexao;

        public AgendamentoDAO(Conexao conexao)
        {
            _conexao = conexao;
        }

        public List<AgendamentoModel> Listar()
        {
            var lista = new List<AgendamentoModel>();

            using var con = _conexao.GetConnection();
            using var comando = con.CreateCommand();

            comando.CommandText = @"
                SELECT
                    a.id_agendamento,
                    a.id_paciente,
                    a.id_profissional,
                    a.data_agendamento,
                    a.hora_agendamento,
                    a.motivo,
                    a.status,
                    p.nome_paciente,
                    pr.nome_profissional
                FROM Agendamento a
                INNER JOIN Paciente p
                    ON a.id_paciente = p.id_paciente
                INNER JOIN Profissional pr
                    ON a.id_profissional = pr.id_profissional
                ORDER BY a.data_agendamento, a.hora_agendamento";

            using var leitor = comando.ExecuteReader();

            while (leitor.Read())
            {
                var hora = leitor.GetTimeSpan("hora_agendamento");

                lista.Add(new AgendamentoModel
                {
                    IdAgendamento = leitor.GetInt32("id_agendamento"),

                    IdPaciente = leitor.GetInt32("id_paciente"),

                    IdProfissional = leitor.GetInt32("id_profissional"),

                    DataAgendamento = leitor.GetDateTime("data_agendamento"),

                    HoraAgendamento = TimeOnly.FromTimeSpan(hora),

                    Motivo = leitor.GetString("motivo"),

                    Status = leitor.GetString("status"),

                    NomePaciente = leitor.GetString("nome_paciente"),

                    NomeProfissional = leitor.GetString("nome_profissional")
                });
            }

            return lista;
        }

        public void Inserir(AgendamentoModel agendamento)
        {
            using var con = _conexao.GetConnection();

            using var comando = con.CreateCommand();

            comando.CommandText = @"
                INSERT INTO Agendamento
                (
                    id_agendamento,
                    id_paciente,
                    id_profissional,
                    data_agendamento,
                    hora_agendamento,
                    motivo,
                    status
                )
                VALUES
                (
                    @id,
                    @paciente,
                    @profissional,
                    @data,
                    @hora,
                    @motivo,
                    @status
                )";

            comando.Parameters.AddWithValue(
                "@id",
                ObterProximoId()
            );

            comando.Parameters.AddWithValue(
                "@paciente",
                agendamento.IdPaciente
            );

            comando.Parameters.AddWithValue(
                "@profissional",
                agendamento.IdProfissional
            );

            comando.Parameters.AddWithValue(
                "@data",
                agendamento.DataAgendamento!.Value
            );

            comando.Parameters.AddWithValue(
                "@hora",
                agendamento.HoraAgendamento!.Value.ToTimeSpan()
            );

            comando.Parameters.AddWithValue(
                "@motivo",
                agendamento.Motivo
            );

            comando.Parameters.AddWithValue(
                "@status",
                agendamento.Status
            );

            comando.ExecuteNonQuery();
        }

        public string Excluir(int id)
        {
            using var con = _conexao.GetConnection();

            using var verificar = con.CreateCommand();

            verificar.CommandText = @"
                SELECT COUNT(*)
                FROM Atendimento
                WHERE id_agendamento = @id";

            verificar.Parameters.AddWithValue("@id", id);

            int quantidade = Convert.ToInt32(
                verificar.ExecuteScalar()
            );

            if (quantidade > 0)
            {
                return "Não é possível excluir este agendamento porque ele possui atendimento cadastrado.";
            }

            using var comando = con.CreateCommand();

            comando.CommandText = @"
                DELETE FROM Agendamento
                WHERE id_agendamento = @id";

            comando.Parameters.AddWithValue("@id", id);

            comando.ExecuteNonQuery();

            return "Agendamento excluído com sucesso!";
        }

        private int ObterProximoId()
        {
            using var con = _conexao.GetConnection();

            using var comando = con.CreateCommand();

            comando.CommandText = @"
                SELECT COALESCE(MAX(id_agendamento), 0) + 1
                FROM Agendamento";

            return Convert.ToInt32(
                comando.ExecuteScalar()
            );
        }
    }
}