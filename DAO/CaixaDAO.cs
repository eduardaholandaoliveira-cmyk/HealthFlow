
using AppWebExemplo.Configs;
using HealthFlow.Models;

namespace HealthFlow.DAO
{
    public class CaixaDAO
    {
        private readonly Conexao _conexao;

        public CaixaDAO(Conexao conexao)
        {
            _conexao = conexao;
        }

        public List<Caixam> Listar()
        {
            try
            {
                var lista = new List<Caixam>();

                using var con = _conexao.GetConnection();

                string sql = "SELECT * FROM caixa";

                using var comando = con.CreateCommand();
                comando.CommandText = sql;

                using var leitor = comando.ExecuteReader();

                while (leitor.Read())
                {
                    var caixa = new Caixam();

                    caixa.IdCaixa = leitor.GetInt32("id_caixa");
                    caixa.Abertura = leitor.GetDateTime("abertura");

                    if (!leitor.IsDBNull(leitor.GetOrdinal("fechamento")))
                    {
                        caixa.Fechamento = leitor.GetDateTime("fechamento");
                    }

                    caixa.SaldoInicial = leitor.GetDouble("saldo_inicial");

                    if (!leitor.IsDBNull(leitor.GetOrdinal("saldo_final")))
                    {
                        caixa.SaldoFinal = leitor.GetDouble("saldo_final");
                    }

                    caixa.TotalEntradas = leitor.GetDouble("total_entradas");
                    caixa.TotalSaidas = leitor.GetDouble("total_saidas");
                    caixa.FkIdFuncionario = leitor.GetInt32("fk_id_funcionario");
                    caixa.StatusCaixa = leitor.GetString("status_caixa");

                    lista.Add(caixa);
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
