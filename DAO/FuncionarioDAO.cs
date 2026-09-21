
using AppWebExemplo.Configs;
using HealthFlow.Models;

namespace HealthFlow.DAO
{
    public class FuncionarioDAO
    {
        private readonly Conexao _conexao;

        public FuncionarioDAO(Conexao conexao)
        {
            _conexao = conexao;
        }

        public List<Funcionariom> Listar()
        {
            try
            {
                var lista = new List<Funcionariom>();

                // Buscando e abrindo a conexão com o banco de dados
                using var con = _conexao.GetConnection();

                string sql = "SELECT * FROM funcionario";

                using var comando = con.CreateCommand();
                comando.CommandText = sql;

                using var leitor = comando.ExecuteReader();

                while (leitor.Read())
                {
                    var funcionario = new Funcionariom();

                    funcionario.IdFuncionario = leitor.GetInt32("id_funcionario");
                    funcionario.Nome = leitor.GetString("nome");
                    funcionario.Cpf = leitor.GetString("cpf");
                    funcionario.Email = leitor.GetString("email");
                    funcionario.Sexo = leitor.GetString("sexo");
                    funcionario.DataNasc = leitor.GetDateTime("data_nasc");
                    funcionario.SenhaAcesso = leitor.GetString("senha_acesso");

                    lista.Add(funcionario);
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

