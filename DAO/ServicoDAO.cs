using AppWebExemplo.Configs;
using HealthFlow.Models;

namespace HealthFlow.DAO
{
    public class ServicoDAO
    {
        private readonly Conexao _conexao;

        public ServicoDAO(Conexao conexao)
        {
            _conexao = conexao;
        }

        public List<Servicom> Listar()
        {
            try
            {
                var lista = new List<Servicom>();

                // Buscando e abrindo a conexão com o banco de dados
                using var con = _conexao.GetConnection();

                string sql = "SELECT * FROM servico";

                using var comando = con.CreateCommand();
                comando.CommandText = sql;

                using var leitor = comando.ExecuteReader();

                while (leitor.Read())
                {
                    var servico = new Servicom();

                    servico.IdServico = leitor.GetInt32("id_servico");
                    servico.Nome = leitor.GetString("nome");
                    servico.ValorEstimado = leitor.GetDouble("valor_estimado");
                    servico.GarantiaMeses = leitor.GetInt32("garantia_meses");
                    servico.TempoEstimado = leitor.GetTimeSpan("tempo_estimado");

                    lista.Add(servico);
                }

                return lista;
            }
            catch
            {
                throw;
            }
        }
    }
}
