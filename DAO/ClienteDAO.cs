
using AppWebExemplo.Configs;
using HealthFlow.Models;

namespace HealthFlow.DAO
{
    public class ClienteDAO
    {
        private readonly Conexao _conexao;

        public ClienteDAO(Conexao conexao)
        {
            _conexao = conexao;
        }

        public List<Clientem> Listar()
        {
            try
            {
                var lista = new List<Clientem>();

                // Buscando e abrindo a conexão com o banco de dados
                using var con = _conexao.GetConnection();

                string sql = "SELECT * FROM cliente";

                using var comando = con.CreateCommand();
                comando.CommandText = sql;

                using var leitor = comando.ExecuteReader();

                while (leitor.Read())
                {
                    var cliente = new Clientem();

                    cliente.IdCliente = leitor.GetInt32("id_cliente");
                    cliente.Nome = leitor.GetString("nome");
                    cliente.Cpf = leitor.GetString("cpf");
                    cliente.Email = leitor.GetString("email");
                    cliente.Sexo = leitor.GetString("sexo");
                    cliente.DataNasc = leitor.GetDateTime("data_nasc");

                    lista.Add(cliente);
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

