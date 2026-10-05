using AppWebExemplo.Configs;
using AppWebExemplo.Configs;
using HealthFlow.Models;

namespace HealthFlow.DAO
{
    public class FinanceiroDAO
    {
        private readonly Conexao _conexao;

        public FinanceiroDAO(Conexao conexao)
        {
            _conexao = conexao;
        }

        public List<FinanceiroModel> Listar()
        {
            var lista = new List<FinanceiroModel>();

            using var con = _conexao.GetConnection();
            using var comando = con.CreateCommand();

            comando.CommandText = @"
                SELECT
                    id_financeiro,
                    id_paciente,
                    id_agendamento,
                    descricao,
                    valor,
                    forma_pagamento,
                    status
                FROM Financeiro
                ORDER BY id_financeiro DESC";

            using var leitor = comando.ExecuteReader();

            while (leitor.Read())
            {
                lista.Add(new FinanceiroModel
                {
                    IdFinanceiro = leitor.GetInt32("id_financeiro"),

                    IdPaciente = leitor.IsDBNull(
                        leitor.GetOrdinal("id_paciente"))
                        ? null
                        : leitor.GetInt32("id_paciente"),

                    IdAgendamento = leitor.IsDBNull(
                        leitor.GetOrdinal("id_agendamento"))
                        ? null
                        : leitor.GetInt32("id_agendamento"),

                    Descricao = leitor.IsDBNull(
                        leitor.GetOrdinal("descricao"))
                        ? ""
                        : leitor.GetString("descricao"),

                    Valor = leitor.GetDecimal("valor"),

                    FormaPagamento = leitor.IsDBNull(
                        leitor.GetOrdinal("forma_pagamento"))
                        ? ""
                        : leitor.GetString("forma_pagamento"),

                    Status = leitor.IsDBNull(
                        leitor.GetOrdinal("status"))
                        ? ""
                        : leitor.GetString("status")
                });
            }

            return lista;
        }

        public void Inserir(FinanceiroModel financeiro)
        {
            using var con = _conexao.GetConnection();

            using var comando = con.CreateCommand();

            comando.CommandText = @"
                INSERT INTO Financeiro
                (
                    id_financeiro,
                    id_paciente,
                    id_agendamento,
                    descricao,
                    valor,
                    forma_pagamento,
                    status
                )
                VALUES
                (
                    @id,
                    @paciente,
                    @agendamento,
                    @descricao,
                    @valor,
                    @forma,
                    @status
                )";

            comando.Parameters.AddWithValue(
                "@id",
                ObterProximoId()
            );

            comando.Parameters.AddWithValue(
                "@paciente",
                financeiro.IdPaciente.HasValue
                    ? financeiro.IdPaciente.Value
                    : DBNull.Value
            );

            comando.Parameters.AddWithValue(
                "@agendamento",
                financeiro.IdAgendamento.HasValue
                    ? financeiro.IdAgendamento.Value
                    : DBNull.Value
            );

            comando.Parameters.AddWithValue(
                "@descricao",
                financeiro.Descricao
            );

            comando.Parameters.AddWithValue(
                "@valor",
                financeiro.Valor
            );

            comando.Parameters.AddWithValue(
                "@forma",
                financeiro.FormaPagamento
            );

            comando.Parameters.AddWithValue(
                "@status",
                financeiro.Status
            );

            comando.ExecuteNonQuery();
        }

        public decimal TotalRecebido()
        {
            using var con = _conexao.GetConnection();

            using var comando = con.CreateCommand();

            comando.CommandText = @"
                SELECT COALESCE(SUM(valor), 0)
                FROM Financeiro
                WHERE status = 'Recebido'";

            return Convert.ToDecimal(
                comando.ExecuteScalar()
            );
        }

        public int QuantidadePagamentos()
        {
            using var con = _conexao.GetConnection();

            using var comando = con.CreateCommand();

            comando.CommandText = @"
                SELECT COUNT(*)
                FROM Financeiro
                WHERE status = 'Recebido'";

            return Convert.ToInt32(
                comando.ExecuteScalar()
            );
        }

        public decimal MediaPagamento()
        {
            using var con = _conexao.GetConnection();

            using var comando = con.CreateCommand();

            comando.CommandText = @"
                SELECT COALESCE(AVG(valor), 0)
                FROM Financeiro
                WHERE status = 'Recebido'";

            return Convert.ToDecimal(
                comando.ExecuteScalar()
            );
        }

        private int ObterProximoId()
        {
            using var con = _conexao.GetConnection();

            using var comando = con.CreateCommand();

            comando.CommandText = @"
                SELECT COALESCE(MAX(id_financeiro), 0) + 1
                FROM Financeiro";

            return Convert.ToInt32(
                comando.ExecuteScalar()
            );
        }
    }
}