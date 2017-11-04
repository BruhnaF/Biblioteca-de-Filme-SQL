using ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.DAL.Contexts;
using ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.DAL

{
    public class NomedoFilmeDAO
    {        
        /// <summary>
        ///  Construtor para bloquear a Inicialização sem parametro.
        /// </summary>
        public NomedoFilmeDAO() { }

        public void Salvar(SqlCommand objCommand, SqlConnection objConexao, NomedoFilme nomedoFilme)
        {
            objCommand.CommandText = ContextNomedoFilme.SalvarNomedoFilme;
            objCommand.Connection = objConexao;
            objCommand.Parameters.Clear();
            objCommand.Parameters.AddWithValue("@Nome", nomedoFilme.Nome);
            objCommand.Parameters.AddWithValue("@IdiomaId", nomedoFilme.IdiomaId);
            objCommand.Parameters.AddWithValue("@FilmeId", nomedoFilme.FilmeId);

            objCommand.ExecuteNonQuery();
        }

        public void Editar(SqlCommand objCommand, SqlConnection objConexao, NomedoFilme nomedoFilme)
        {
            objCommand.CommandText = ContextNomedoFilme.AdicionaFiltro(ContextNomedoFilme.AlterarNomedoFilme, nomedoFilme.NomedoFilmeId, nomedoFilme.Nome, nomedoFilme.FilmeId, nomedoFilme.IdiomaId);
            objCommand.Connection = objConexao;

            objCommand.Parameters.AddWithValue("@NomedoFilmeId", nomedoFilme.NomedoFilmeId);
            objCommand.Parameters.AddWithValue("@Nome", nomedoFilme.Nome);
            objCommand.Parameters.AddWithValue("@FilmeId", nomedoFilme.FilmeId);
            objCommand.Parameters.AddWithValue("@IdiomaId", nomedoFilme.IdiomaId);

            objCommand.ExecuteNonQuery();
        }

        /// <summary>
        /// Buscar por Id.
        /// </summary>
        /// <param name="id">Valor a ser encontrado.</param>
        /// <returns>Valor encontrado.</returns>
        public NomedoFilme BuscarporId(SqlCommand objCommand, SqlConnection objConexao, int IdNomeFilme)
        {
            var nomedoFilme = new NomedoFilme();
            objCommand.CommandText = ContextNomedoFilme.AdicionaFiltro(ContextNomedoFilme.ListarTodosNomedoFilme, IdNomeFilme, null, 0 ,null);
            objCommand.Connection = objConexao;
            objCommand.Parameters.AddWithValue("@NomedoFilmeId", IdNomeFilme);

            SqlDataReader resultado = objCommand.ExecuteReader();

            if (resultado.HasRows)
            {
                while (resultado.Read())
                {
                    nomedoFilme = CarregarNomedoFilmeReader(resultado);
                }

                resultado.Close();
            }
            return nomedoFilme;
        }


        public List<NomedoFilme> BuscarporIdFilme(SqlCommand objCommand, SqlConnection objConexao, int IdFilme)
        {
            var listaNomedoFilme = new List<NomedoFilme>();
            objCommand.CommandText = ContextNomedoFilme.AdicionaFiltro(ContextNomedoFilme.ListarTodosNomedoFilme, 0, null, IdFilme, null);
            objCommand.Connection = objConexao;
            objCommand.Parameters.Clear();
            objCommand.Parameters.AddWithValue("@FilmeId", IdFilme);

            using (SqlDataReader resultado = objCommand.ExecuteReader())
            {
                if (resultado.HasRows)
                {
                    while (resultado.Read())
                    {
                        listaNomedoFilme.Add(CarregarNomedoFilmeReader(resultado));
                    }

                    resultado.Close();
                }
            }
            return listaNomedoFilme;
        }

        private NomedoFilme CarregarNomedoFilmeReader(SqlDataReader resultado)
        {
            return new NomedoFilme
            {
                NomedoFilmeId = Convert.ToInt32(resultado["Id"]),
                Nome = resultado["Nome"].ToString(),
                FilmeId = Convert.ToInt32(resultado["Filme_FilmeId"]),
                IdiomaId = resultado["IdiomaId"].ToString()
            };
        }

        /// <summary>
        /// Listar todos os Nomes de Filmes.
        /// </summary>
        /// <returns>Retorna Lista de Nomes de Filmes.</returns>
        public IEnumerable<NomedoFilme> Listar(SqlConnection sqlConnection, SqlCommand objCommand)
        {
            var listaNomedoFilmes = new List<NomedoFilme>();

            objCommand.CommandText = ContextNomedoFilme.ListarTodosNomedoFilme;
            objCommand.Connection = sqlConnection;

            SqlDataReader resultado = objCommand.ExecuteReader();
            if (resultado.HasRows)
            {
                while (resultado.Read())
                {
                    listaNomedoFilmes.Add(CarregarNomedoFilmeReader(resultado));
                }
                resultado.Close();
            }

            return listaNomedoFilmes;
        }

        public void RemoverNomesFilme(SqlCommand objCommand, SqlConnection objConexao, int IdNomeFilme)
        {         
            objCommand.CommandText = ContextNomedoFilme.AdicionaFiltro(ContextNomedoFilme.DeletarNomedoFilme, IdNomeFilme, null, 0, null);
            objCommand.Connection = objConexao;
            objCommand.Parameters.AddWithValue("@NomedoFilmeId", IdNomeFilme);

            SqlDataReader resultado = objCommand.ExecuteReader();          
        }
    }

}
