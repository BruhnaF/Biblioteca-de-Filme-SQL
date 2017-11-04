using ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.DAL.Contexts;
using ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.DAL
{
    public class FilmeGeneroDAO
    {
        public void Salvar(SqlCommand objCommand, SqlConnection objConexao, int filmeId, int generoId)
        {       
            objCommand.CommandText = ContextFilmeGenero.SalvarFilmeGenero;
            objCommand.Connection = objConexao;
            objCommand.Parameters.Clear();
            objCommand.Parameters.AddWithValue("@GeneroId", generoId);
            objCommand.Parameters.AddWithValue("@FilmeId", filmeId);

            objCommand.ExecuteNonQuery();
        }

        public void Editar(SqlCommand objCommand, SqlConnection objConexao, int filmeId, int generoId)
        {
            objCommand.CommandText = ContextFilmeGenero.AdicionaFiltro(ContextFilmeGenero.AlterarFilmeGenero, filmeId, generoId);
            objCommand.Connection = objConexao;

            objCommand.Parameters.AddWithValue("@GeneroId", generoId);
            objCommand.Parameters.AddWithValue("@FilmeId", filmeId);

            objCommand.ExecuteNonQuery();
        }

        public List<int> BuscarporIdFilme(SqlCommand objCommand, SqlConnection objConexao, int IdFilme)
        {
            var listaGeneroFilme = new List<int>();
            objCommand.CommandText = ContextFilmeGenero.AdicionaFiltro(ContextFilmeGenero.ListarTodosGeneroFilmes, IdFilme, 0);
            objCommand.Connection = objConexao;
            objCommand.Parameters.Clear();
            objCommand.Parameters.AddWithValue("@FilmeId", IdFilme);

            using (SqlDataReader resultado = objCommand.ExecuteReader())
            {
                if (resultado.HasRows)
                {
                    while (resultado.Read())
                    {
                        listaGeneroFilme.Add(Convert.ToInt32(resultado["Genero_GeneroId"]));
                    }

                    resultado.Close();
                }
            }
            return listaGeneroFilme;
        }

        public void RemoverGeneroFilme(SqlCommand objCommand, SqlConnection objConexao, int generoId, int filmeId)
        {
            objCommand.CommandText = ContextFilmeGenero.AdicionaFiltro(ContextFilmeGenero.RemoverGeneroFilme, filmeId, generoId);
            objCommand.Connection = objConexao;

            objCommand.Parameters.AddWithValue("@GeneroId", generoId);
            objCommand.Parameters.AddWithValue("@FilmeId", filmeId);

            objCommand.ExecuteNonQuery();
        }
    }
}
