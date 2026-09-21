using AppWebExemplo.Configs;
using HealthFlow.Models;

namespace HealthFlow.DAO
{
    public class ItemDAO
    {
        private readonly Conexao _conexao;

        public ItemDAO(Conexao conexao)
        {
            _conexao = conexao;
        }

        public List<Itemm> Listar()
        {
            try
            {
                var lista = new List<Itemm>();

                // Buscando e abrindo a conexão com o banco de dados
                using var con = _conexao.GetConnection();

                string sql = "SELECT * FROM item";

                using var comando = con.CreateCommand();
                comando.CommandText = sql;

                using var leitor = comando.ExecuteReader();

                while (leitor.Read())
                {
                    var item = new Itemm();

                    item.IdItem = leitor.GetInt32("id_item");
                    item.Quantidade = leitor.GetDouble("quantidade");
                    item.ValorUnitario = leitor.GetDouble("valor_unitario");
                    item.FkIdServico = leitor.GetInt32("fk_id_servico");
                    item.FkIdVenda = leitor.GetInt32("fk_id_venda");

                    lista.Add(item);
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

