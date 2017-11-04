using ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.DAL.Contexts;
using ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.DAL

{
    public class GeneroDAO
    {
        /// <summary>
        /// Construtor para bloquear a Inicialização sem parametro.
        /// </summary>
        public GeneroDAO() { }

        /// <summary>
        /// Lista os Generos Cadastrados.
        /// </summary>
        /// <returns>Retorna Lista de Generos.</returns>
        public IEnumerable<Genero> Listar(SqlConnection sqlConnection, SqlCommand objCommand)
        {
            var listaGeneros = new List<Genero>();

            objCommand.CommandText = ContextGenero.ListarTodosGeneros;
            objCommand.Connection = sqlConnection;

            SqlDataReader resultado = objCommand.ExecuteReader();
            if (resultado.HasRows)
            {
                while (resultado.Read())
                {
                    listaGeneros.Add(CarregarGenerodoReader(resultado));
                }
                resultado.Close();
            }

            return listaGeneros;
        }

        private Genero CarregarGenerodoReader(SqlDataReader resultado)
        {
            return new Genero
            {
                GeneroId = Convert.ToInt32(resultado["GeneroId"]),
                Descricao = resultado["Descricao"].ToString()
            };
        }

        /// <summary>
        /// Salva Genero.
        /// </summary>Idioma a ser salvo.</param>
        public void Salvar(SqlCommand objCommand, SqlConnection objConexao, Genero genero)
        {
            objCommand.CommandText = ContextGenero.SalvarGenero;
            objCommand.Connection = objConexao;

            objCommand.Parameters.AddWithValue("@Descricao", genero.Descricao);

            objCommand.ExecuteNonQuery();
        }

        /// <summary>
        /// Editar Genero.
        /// </summary>
        /// <param name="genero">Genero a ser Editado.</param>
        public void Editar(SqlCommand objCommand, SqlConnection objConexao, Genero genero)
        {
            objCommand.CommandText = ContextGenero.AdicionaFiltro(ContextGenero.AlterarGenero, genero.GeneroId, null);
            objCommand.Connection = objConexao;

            objCommand.Parameters.AddWithValue("@GeneroId", genero.GeneroId);
            objCommand.Parameters.AddWithValue("@Descricao", genero.Descricao);

            objCommand.ExecuteNonQuery();
        }

        /// <summary>
        /// Busca Genero por Id no Context.
        /// </summary>
        /// <param name="id">Valor a ser Comparado.</param>
        /// <returns>Retorna Genero Encontrado</returns>
        public Genero BuscarPorId(SqlCommand objCommand, SqlConnection objConexao, int id)
        {
            var genero = new Genero();
            objCommand.CommandText = ContextGenero.AdicionaFiltro(ContextGenero.ListarTodosGeneros, id, null);
            objCommand.Connection = objConexao;
            objCommand.Parameters.Clear();
            objCommand.Parameters.AddWithValue("@GeneroId", id);

            SqlDataReader resultado = objCommand.ExecuteReader();

            if (resultado.HasRows)
            {
                while (resultado.Read())
                {
                    genero = CarregarGenerodoReader(resultado);
                }

                resultado.Close();
            }
            return genero;
        }

        /// <summary>
        /// Verifica se Genero Informado já existe no Context.
        /// </summary>
        /// <param name="genero">Genero a ser Comparado.</param>
        /// <returns>Retorna True se Verdadeiro e False se Falso.</returns>
        public bool JaExiste(SqlCommand objCommand, SqlConnection objConexao, Genero genero)
        {
            var jaExiste = false;

            objCommand.CommandText = ContextGenero.AdicionaFiltro(ContextGenero.ListarTodosGeneros, 0, genero.Descricao);
            objCommand.Connection = objConexao;

            objCommand.Parameters.AddWithValue("@Descricao", genero.Descricao);

            SqlDataReader resultado = objCommand.ExecuteReader();
            if (resultado.HasRows)
            {
                jaExiste = true;
            }
            resultado.Close();

            return jaExiste;
        }

        /// <summary>
        /// Excluir Genero.
        /// </summary>
        /// <param name="genero">Valor a ser Excluido.</param>
        public void Excluir(SqlCommand objCommand, SqlConnection objConexao, int generoId)
        {
            objCommand.CommandText = ContextGenero.AdicionaFiltro(ContextGenero.ExcluirGenero, generoId, null);

            objCommand.Connection = objConexao;
            objCommand.Parameters.AddWithValue("@GeneroId", generoId);

            objCommand.ExecuteNonQuery();
        }
    }
}
