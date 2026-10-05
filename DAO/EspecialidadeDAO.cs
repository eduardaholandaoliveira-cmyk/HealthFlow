using AppWebExemplo.Configs;
using HealthFlow.Models;

namespace HealthFlow.DAO
{
    public class EspecialidadeDAO
    {
        private readonly Conexao _conexao;

        public EspecialidadeDAO(Conexao conexao)
        {
            _conexao = conexao;
        }

        public List<EspecialidadeModel> Listar()
        {
            var lista = new List<EspecialidadeModel>();

            using var con = _conexao.GetConnection();
            using var comando = con.CreateCommand();

            comando.CommandText = @"
                SELECT
                    id_especialidade,
                    nome_especialidade,
                    descricao
                FROM Especialidade
                ORDER BY nome_especialidade";

            using var leitor = comando.ExecuteReader();

            while (leitor.Read())
            {
                lista.Add(new EspecialidadeModel
                {
                    IdEspecialidade = leitor.GetInt32("id_especialidade"),
                    NomeEspecialidade = leitor.GetString("nome_especialidade"),
                    Descricao = leitor.IsDBNull(leitor.GetOrdinal("descricao"))
                        ? ""
                        : leitor.GetString("descricao")
                });
            }

            return lista;
        }
    }
}