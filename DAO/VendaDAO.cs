
using AppWebExemplo.Configs;
using HealthFlow.Models;

namespace HealthFlow.DAO
{
    public class VendaDAO
    {
        private readonly Conexao _conexao;

        public VendaDAO(Conexao conexao)
        {
            _conexao = conexao;
        }

        public List<Vendam> Listar()
        {
            try
            {
                var lista = new List<Vendam>();

                // Buscando e abrindo a conexão com o banco de dados
                using var con = _conexao.GetConnection();

                string sql = "SELECT * FROM venda";

                using var comando = con.CreateCommand();
                comando.CommandText = sql;

                using var leitor = comando.ExecuteReader();

                while (leitor.Read())
                {
                    var venda = new Vendam();

                    venda.IdVenda = leitor.GetInt32("id_venda");
                    venda.DataHora = leitor.GetDateTime("data_hora");
                    venda.TipoPagamento = leitor.GetString("tipo_pagamento");
                    venda.ValorTotal = leitor.GetDouble("valor_total");
                    venda.FkIdCliente = leitor.GetInt32("fk_id_cliente");
                    venda.FkIdCaixa = leitor.GetInt32("fk_id_caixa");

                    lista.Add(venda);
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

