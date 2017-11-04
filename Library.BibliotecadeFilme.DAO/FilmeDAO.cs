using ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.DAL.Contexts;
using ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.DAL
{
    public class FilmeDAO
    {
        /// <summary>
        /// Construtor para bloquear a Inicialização sem parametro.
        /// </summary>
        public FilmeDAO() { }

        /// <summary>
        /// Listar Filmes.
        /// </summary>
        /// <returns>Lista de Filmes.</returns>
        public IEnumerable<Filme> Listar(SqlConnection sqlConnection, SqlCommand objCommand)
        {
            var listaFilmes = new List<Filme>();

            objCommand.CommandText = ContextFilme.ListarTodosFilmes;
            objCommand.Connection = sqlConnection;
            using (SqlDataReader resultado = objCommand.ExecuteReader())
            {
                if (resultado.HasRows)
                {
                    while (resultado.Read())
                    {
                        listaFilmes.Add(CarregarFilmedoReader(resultado));
                    }
                    resultado.Close();

                }
            }

            return listaFilmes;
        }

        private Filme CarregarFilmedoReader(SqlDataReader resultado)
        {
            return new Filme
            {
                FilmeId = Convert.ToInt32(resultado["FilmeId"]),
                Descricao = resultado["Descricao"].ToString()
            };
        }


        /// <summary>
        /// Buscar Filme por Id.
        /// </summary>
        /// <param name="id">Valor a ser Comparado.</param>
        /// <returns>Valor Encontrado.</returns>
        public Filme BuscarPorId(SqlCommand objCommand, SqlConnection objConexao, int id)
        {
            var filme = new Filme();
            objCommand.CommandText = ContextFilme.AdicionaFiltro(ContextFilme.ListarTodosFilmes, id, null);
            objCommand.Connection = objConexao;
            objCommand.Parameters.AddWithValue("@FilmeId", id);

            SqlDataReader resultado = objCommand.ExecuteReader();

            if (resultado.HasRows)
            {
                while (resultado.Read())
                {
                    filme = CarregarFilmedoReader(resultado);
                }

                resultado.Close();
            }
            return filme;
        }

        /// <summary>
        /// Salvar Filme.
        /// </summary>
        /// <param name="filme">Filme a ser Salvo.</param>
        public int Salvar(SqlCommand objCommand, SqlConnection objConexao, Filme filme)
        {
            objCommand.CommandText = ContextFilme.SalvarFilme;
            objCommand.Connection = objConexao;
            objCommand.Parameters.Clear();
            objCommand.Parameters.AddWithValue("@Descricao", filme.Descricao);
            var idFilme = objCommand.ExecuteScalar().ToString();

            int idTemp = 0;
            int.TryParse(idFilme, out idTemp);

            return idTemp;
        }

        /// <summary>
        /// Editar Filme.
        /// </summary>
        /// <param name="filme">Filme a ser Editado.</param>
        public void Editar(SqlCommand objCommand, SqlConnection objConexao, Filme filme)
        {
            objCommand.CommandText = ContextFilme.AdicionaFiltro(ContextFilme.AlterarFilme, filme.FilmeId, filme.Descricao);
            objCommand.Connection = objConexao;

            objCommand.Parameters.Clear();
            objCommand.Parameters.AddWithValue("@FilmeId", filme.FilmeId);
            objCommand.Parameters.AddWithValue("@Descricao", filme.Descricao);

            objCommand.ExecuteNonQuery();
        }

        public void Excluir(SqlCommand objCommand, SqlConnection objConexao, int idFilme)
        {
            objCommand.CommandText = ContextFilme.AdicionaFiltro(ContextFilme.ExcluirFilme, idFilme, null);
            objCommand.Connection = objConexao;

            objCommand.Parameters.Clear();
            objCommand.Parameters.AddWithValue("@FilmeId", idFilme);            

            objCommand.ExecuteNonQuery();
        }

        /// <summary>
        /// Verifica se Filme Informado já existe no Context.
        /// </summary>
        /// <param name="filme">Filme a ser Comparado.</param>
        /// <returns>Retorna True se Verdadeiro e False se Falso.</returns>
        public bool JaExiste(Filme filme)
        {
            throw new NotImplementedException();
        }
    }
}
