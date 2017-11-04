using System.Collections.Generic;

namespace ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.DAL.Contexts
{
    public class ContextFilmeGenero : Context
    {
        public static string SalvarFilmeGenero = "INSERT INTO GeneroFilme (Genero_GeneroId, Filme_FilmeId) VALUES (@GeneroId, @FilmeId) ";
        public static string AlterarFilmeGenero = "UPDATE GeneroFilme SET Genero_GeneroId = @GeneroId";
        public static string ListarTodosGeneroFilmes = "SELECT Genero_GeneroId, Filme_FilmeId FROM GeneroFilme";
        public static string RemoverGeneroFilme = "DELETE FROM GeneroFilme";

        public static string AdicionaFiltro(string consulta, int parametroFilmeId, int parametroGeneroId)
        {
            List<string> filtros = new List<string>();

            if (parametroFilmeId > 0)
                filtros.Add(" Filme_FilmeId = @FilmeId ");

            if (parametroGeneroId > 0)
                filtros.Add(" Genero_GeneroId = @GeneroId ");

            if (filtros.Count > 0)
                return string.Format("{0} WHERE {1} ", consulta, string.Join(" AND ", filtros));

            return consulta;
        }
    }
}