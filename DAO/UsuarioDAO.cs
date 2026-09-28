using AppWebExemplo.Configs;
using HealthFlow.Models;

namespace HealthFlow.DAO
{
    public class UsuarioDAO
    {
        private readonly Conexao _conexao;

        public UsuarioDAO(Conexao conexao)
        {
            _conexao = conexao;
        }

        public List<UsuarioModel> Listar()
        {
            var lista = new List<UsuarioModel>();

            using var con = _conexao.GetConnection();
            using var comando = con.CreateCommand();

            comando.CommandText = "SELECT * FROM Usuario";

            using var leitor = comando.ExecuteReader();

            while (leitor.Read())
            {
                lista.Add(new UsuarioModel
                {
                    IdUsuario = leitor.GetInt32("id_usuario"),
                    NomeUsuario = leitor.GetString("nome_usuario"),
                    Email = leitor.GetString("email"),
                    Senha = leitor.GetString("senha"),
                    TipoUsuario = leitor.GetString("tipo_usuario")
                });
            }

            return lista;
        }
    }
}