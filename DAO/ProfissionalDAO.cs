using AppWebExemplo.Configs;
using HealthFlow.Models;

namespace HealthFlow.DAO
{
    public class ProfissionalDAO
    {
        private readonly Conexao _conexao;

        public ProfissionalDAO(Conexao conexao)
        {
            _conexao = conexao;
        }

        public List<ProfissionalModel> Listar()
        {
            var lista = new List<ProfissionalModel>();

            using var con = _conexao.GetConnection();
            using var comando = con.CreateCommand();

            comando.CommandText = "SELECT * FROM Profissional";

            using var leitor = comando.ExecuteReader();

            while (leitor.Read())
            {
                lista.Add(new ProfissionalModel
                {
                    IdProfissional = leitor.GetInt32("id_profissional"),
                    NomeProfissional = leitor.GetString("nome_profissional"),
                    Cpf = leitor.GetString("cpf"),
                    Crm = leitor.GetString("crm"),
                    Email = leitor.GetString("email"),
                    Telefone = leitor.GetString("telefone"),
                    IdEspecialidade = leitor.GetInt32("id_especialidade")
                });
            }

            return lista;
        }
    }
}