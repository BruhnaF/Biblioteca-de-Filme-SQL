using ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.DAL.Contexts;
using ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.Model;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.DAL
{
    /// <summary>
    /// Classe para manipulação de Dados do Idioma.
    /// </summary>
    public class IdiomaDAO
    {
        /// <summary>
        /// Construtor para bloquear a Inicialização sem parametro.
        /// </summary>
        public IdiomaDAO() { }

        /// <summary>
        /// Lista os idiomas Cadastrados.
        /// </summary>
        /// <param name="sqlConnection">Objeto de Conexao do Banco.</param>
        /// <returns>Lista de Idiomas.</returns>
        public IEnumerable<Idioma> Listar(SqlConnection sqlConnection, SqlCommand objCommand)
        {
            var listaIdiomas = new List<Idioma>();

            objCommand.CommandText = ContextIdioma.ListarTodosIdiomas;
            objCommand.Connection = sqlConnection;

            SqlDataReader resultado = objCommand.ExecuteReader();
            if (resultado.HasRows)
            {
                while (resultado.Read())
                {
                    listaIdiomas.Add(CarregarIdiomadoReader(resultado));
                }
                resultado.Close();
            }

            return listaIdiomas;
        }

        private Idioma CarregarIdiomadoReader(SqlDataReader resultado)
        {
            return new Idioma
            {
                IdiomaId = resultado["IdiomaId"].ToString(),
                Descricao = resultado["Descricao"].ToString()
            };
        }

        /// <summary>
        /// Salva Idioma.
        /// </summary>
        /// <param name="idioma">Idioma a ser Salvo.</param>
        public void Salvar(Idioma idioma, SqlConnection sqlConnection, SqlCommand objCommand)
        {
            objCommand.CommandText = ContextIdioma.SalvarIdioma;
            objCommand.Connection = sqlConnection;

            objCommand.Parameters.AddWithValue("@IdiomaId", idioma.IdiomaId);
            objCommand.Parameters.AddWithValue("@Descricao", idioma.Descricao);

            objCommand.ExecuteNonQuery();
        }

        /// <summary>
        /// Busca no Context por Id.
        /// </summary>
        /// <param name="idioma">Valor a ser Comparado.</param>
        /// <returns></returns>
        public Idioma BuscarPorId(SqlCommand objCommand, SqlConnection objConexao, string idiomaId)
        {
            var idioma = new Idioma();
            objCommand.Connection = objConexao;
            objCommand.CommandText = ContextIdioma.AdicionaFiltro(ContextIdioma.ListarTodosIdiomas, idiomaId, null);
            objCommand.Parameters.AddWithValue("@IdiomaId", idiomaId);

            SqlDataReader resultado = objCommand.ExecuteReader();

            if (resultado.HasRows)
            {
                while (resultado.Read())
                {
                    idioma = CarregarIdiomadoReader(resultado);
                }

                resultado.Close();
            }
            return idioma;
        }

        /// <summary>
        /// Verifica se dados recebidos, ja existem no Context. (Cadastrar)
        /// </summary>
        /// <param name="idioma">Valor a ser Comparado.</param>
        /// <returns></returns>
        public bool JaExiste(SqlCommand objCommand, SqlConnection objConexao, Idioma idioma)
        {
            var jaExiste = false;
            objCommand.CommandText = ContextIdioma.AdicionaFiltro(ContextIdioma.ListarTodosIdiomas, idioma.IdiomaId, idioma.Descricao);
            objCommand.Connection = objConexao;

            objCommand.Parameters.AddWithValue("@IdiomaId", idioma.IdiomaId);
            objCommand.Parameters.AddWithValue("@Descricao", idioma.Descricao);

            SqlDataReader resultado = objCommand.ExecuteReader();
            if (resultado.HasRows)
            {
                jaExiste = true;
            }
            resultado.Close();

            return jaExiste;
        }

        /// <summary>
        /// Verifica se dados recebidos, ja existem no Context. (Editar)
        /// </summary>
        /// <param name="idioma">Valor a ser Comparado.</param>
        /// <returns></returns>
        public bool JaExisteEditar(SqlCommand objCommand, SqlConnection objConexao, Idioma idioma)
        {
            var jaExiste = false;
            objCommand.CommandText = ContextIdioma.AdicionaFiltro(ContextIdioma.ListarTodosIdiomas, null, idioma.Descricao);
            objCommand.Connection = objConexao;

            // objCommand.Parameters.AddWithValue("@IdiomaId", idioma.IdiomaId);
            objCommand.Parameters.AddWithValue("@Descricao", idioma.Descricao);

            SqlDataReader resultado = objCommand.ExecuteReader();
            if (resultado.HasRows)
            {
                jaExiste = true;
            }
            resultado.Close();

            return jaExiste;
        }

        /// <summary>
        /// Edita Idioma.
        /// </summary>
        /// <param name="idioma">Idioma a ser Editado. </param>
        public void Editar(SqlCommand objCommand, SqlConnection objConexao, Idioma idioma)
        {
            var IdiomaId = idioma.IdiomaId;

            objCommand.CommandText = ContextIdioma.AdicionaFiltro(ContextIdioma.AlterarIdioma, idioma.IdiomaId, null);
            objCommand.Connection = objConexao;

            objCommand.Parameters.AddWithValue("@IdiomaId", idioma.IdiomaId);
            objCommand.Parameters.AddWithValue("@Descricao", idioma.Descricao);

            objCommand.ExecuteNonQuery();
        }

        /// <summary>
        /// Excluir Idioma.
        /// </summary>
        /// <param name="idioma">Idioma a ser Excluido.</param>
        public void Excluir(SqlCommand objCommand, SqlConnection objConexao, string IdiomaId)
        {
            objCommand.CommandText = ContextIdioma.AdicionaFiltro(ContextIdioma.ExcluirIdioma, IdiomaId, null);

            objCommand.Connection = objConexao;
            objCommand.Parameters.AddWithValue("@IdiomaId", IdiomaId);

            objCommand.ExecuteNonQuery();
        }

    }
}
