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

            comando.CommandText = "SELECT * FROM Agendamento";

            using var leitor = comando.ExecuteReader();

            while (leitor.Read())
            {
                lista.Add(new AgendamentoModel
                {
                    IdAgendamento = leitor.GetInt32("id_agendamento"),
                    IdPaciente = leitor.GetInt32("id_paciente"),
                    IdProfissional = leitor.GetInt32("id_profissional"),
                    DataAgendamento = leitor.GetDateTime("data_agendamento"),
                    HoraAgendamento = leitor.GetTimeSpan("hora_agendamento"),
                    Motivo = leitor.GetString("motivo"),
                    Status = leitor.GetString("status")
                });
            }

            return lista;
        }
    }
}