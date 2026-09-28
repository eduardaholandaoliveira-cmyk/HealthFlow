using AppWebExemplo.Configs;
using HealthFlow.Models;

namespace HealthFlow.DAO
{
    public class AtendimentoDAO
    {
        private readonly Conexao _conexao;

        public AtendimentoDAO(Conexao conexao)
        {
            _conexao = conexao;
        }

        public List<AtendimentoModel> Listar()
        {
            var lista = new List<AtendimentoModel>();

            using var con = _conexao.GetConnection();
            using var comando = con.CreateCommand();

            comando.CommandText = "SELECT * FROM Atendimento";

            using var leitor = comando.ExecuteReader();

            while (leitor.Read())
            {
                lista.Add(new AtendimentoModel
                {
                    IdAtendimento = leitor.GetInt32("id_atendimento"),
                    IdAgendamento = leitor.GetInt32("id_agendamento"),
                    Queixa = leitor.GetString("queixa"),
                    Diagnostico = leitor.GetString("diagnostico"),
                    Tratamento = leitor.GetString("tratamento"),
                    Observacoes = leitor.GetString("observacoes")
                });
            }

            return lista;
        }
    }
}