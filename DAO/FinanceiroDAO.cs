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

            comando.CommandText = "SELECT * FROM Financeiro";

            using var leitor = comando.ExecuteReader();

            while (leitor.Read())
            {
                lista.Add(new FinanceiroModel
                {
                    IdFinanceiro = leitor.GetInt32("id_financeiro"),
                    IdPaciente = leitor.GetInt32("id_paciente"),
                    IdAgendamento = leitor.GetInt32("id_agendamento"),
                    Descricao = leitor.GetString("descricao"),
                    Valor = leitor.GetDecimal("valor"),
                    FormaPagamento = leitor.GetString("forma_pagamento"),
                    Status = leitor.GetString("status")
                });
            }

            return lista;
        }
    }
}
