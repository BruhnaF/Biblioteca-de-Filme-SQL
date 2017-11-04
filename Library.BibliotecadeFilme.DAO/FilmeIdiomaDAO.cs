using ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.DAL.Contexts;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.DAL
{
    public class FilmeIdiomaDAO
    {
        public void Salvar(SqlCommand objCommand, SqlConnection objConexao, int filmeId, string idiomaId)
        {
            objCommand.CommandText = ContextFilmeIdioma.SalvarFilmeIdioma;
            objCommand.Connection = objConexao;
            objCommand.Parameters.Clear();
            objCommand.Parameters.AddWithValue("@IdiomaId", idiomaId);
            objCommand.Parameters.AddWithValue("@FilmeId", filmeId);

            objCommand.ExecuteNonQuery();
        }

        public void Editar(SqlCommand objCommand, SqlConnection objConexao, int filmeId, string idiomaId)
        {
            objCommand.CommandText = ContextFilmeIdioma.AdicionaFiltro(ContextFilmeIdioma.AlterarFilmeIdioma, filmeId, idiomaId);
            objCommand.Connection = objConexao;

            objCommand.Parameters.AddWithValue("@IdiomaId", idiomaId);
            objCommand.Parameters.AddWithValue("@FilmeId", filmeId);

            objCommand.ExecuteNonQuery();
        }

        public List<string> BuscarporIdFilme(SqlCommand objCommand, SqlConnection objConexao, int IdFilme)
        {
            var listaIdiomaFilme = new List<string>();
            objCommand.CommandText = ContextFilmeIdioma.AdicionaFiltro(ContextFilmeIdioma.ListarTodosIdiomaFilmes, IdFilme, null);
            objCommand.Connection = objConexao;
            objCommand.Parameters.Clear();
            objCommand.Parameters.AddWithValue("@FilmeId", IdFilme);

            using (SqlDataReader resultado = objCommand.ExecuteReader())
            {
                if (resultado.HasRows)
                {
                    while (resultado.Read())
                    {
                        listaIdiomaFilme.Add(resultado["Idioma_IdiomaId"].ToString());
                    }

                    resultado.Close();
                }
            }
            return listaIdiomaFilme;
        }

        public void RemoverIdiomaFilme(SqlCommand objCommand, SqlConnection objConexao, string idiomaId, int filmeId)
        {
            objCommand.CommandText = ContextFilmeIdioma.AdicionaFiltro(ContextFilmeIdioma.RemoverIdiomaFilme, filmeId, idiomaId);
            objCommand.Connection = objConexao;

            objCommand.Parameters.AddWithValue("@IdiomaId", idiomaId);
            objCommand.Parameters.AddWithValue("@FilmeId", filmeId);

            objCommand.ExecuteNonQuery();
        }
    }
}