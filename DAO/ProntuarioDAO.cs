using AppWebExemplo.Configs;
using HealthFlow.Models;

namespace HealthFlow.DAO
{
    public class ProntuarioDAO
    {
        private readonly Conexao _conexao;

        public ProntuarioDAO(Conexao conexao)
        {
            _conexao = conexao;
        }

        public List<ProntuarioModel> Listar()
        {
            var lista = new List<ProntuarioModel>();

            using var con = _conexao.GetConnection();
            using var comando = con.CreateCommand();

            comando.CommandText = "SELECT * FROM Prontuario";

            using var leitor = comando.ExecuteReader();

            while (leitor.Read())
            {
                lista.Add(new ProntuarioModel
                {
                    IdProntuario = leitor.GetInt32("id_prontuario"),
                    IdPaciente = leitor.GetInt32("id_paciente"),
                    TipoSanguineo = leitor.GetString("tipo_sanguineo"),
                    Alergias = leitor.GetString("alergias"),
                    Medicamentos = leitor.GetString("medicamentos"),
                    Historico = leitor.GetString("historico"),
                    Observacoes = leitor.GetString("observacoes")
                });
            }

            return lista;
        }
    }
}