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

        public UsuarioModel? Login(string email, string senha)
        {
            using var con = _conexao.GetConnection();
            using var comando = con.CreateCommand();

            comando.CommandText = @"
            SELECT *
            FROM Usuario
            WHERE email = @email
            AND senha = @senha
            LIMIT 1";

            comando.Parameters.AddWithValue("@email", email);
            comando.Parameters.AddWithValue("@senha", senha);

            using var leitor = comando.ExecuteReader();

            if (leitor.Read())
            {
                return new UsuarioModel
                {
                    IdUsuario = leitor.GetInt32("id_usuario"),
                    NomeUsuario = leitor.GetString("nome_usuario"),
                    Email = leitor.GetString("email"),
                    Senha = leitor.GetString("senha"),
                    TipoUsuario = leitor.GetString("tipo_usuario")
                };
            }

            return null;
        }

        public bool Cadastrar(string nome, string email, string senha)
        {
            using var con = _conexao.GetConnection();

            using var verificar = con.CreateCommand();

            verificar.CommandText = @"
            SELECT COUNT(*)
            FROM Usuario
            WHERE email = @email";

            verificar.Parameters.AddWithValue("@email", email);

            int quantidade = Convert.ToInt32(
                verificar.ExecuteScalar()
            );

            if (quantidade > 0)
            {
                return false;
            }

            int novoId = ObterProximoId(con);

            using var comando = con.CreateCommand();

            comando.CommandText = @"
            INSERT INTO Usuario
            (
                id_usuario,
                nome_usuario,
                email,
                senha,
                tipo_usuario
            )
            VALUES
            (
                @id,
                @nome,
                @email,
                @senha,
                @tipo
            )";

            comando.Parameters.AddWithValue("@id", novoId);
            comando.Parameters.AddWithValue("@nome", nome);
            comando.Parameters.AddWithValue("@email", email);
            comando.Parameters.AddWithValue("@senha", senha);
            comando.Parameters.AddWithValue("@tipo", "Paciente");

            comando.ExecuteNonQuery();

            return true;
        }

        private int ObterProximoId(
            MySql.Data.MySqlClient.MySqlConnection con)
        {
            using var comando = con.CreateCommand();

            comando.CommandText = @"
            SELECT COALESCE(MAX(id_usuario), 0) + 1
            FROM Usuario";

            return Convert.ToInt32(
                comando.ExecuteScalar()
            );
        }
    }


}
