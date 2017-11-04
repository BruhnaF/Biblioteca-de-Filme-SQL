using System.Collections.Generic;

namespace ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.DAL.Contexts
{
    public class ContextFilmeIdioma : Context
    {
        public static string SalvarFilmeIdioma = "INSERT INTO IdiomaFilme (Idioma_IdiomaId, Filme_FilmeId) VALUES (@IdiomaId, @FilmeId) ";
        public static string AlterarFilmeIdioma = "UPDATE IdiomaFilme SET Idioma_IdiomaId = @IdiomaId";
        public static string ListarTodosIdiomaFilmes = "SELECT Idioma_IdiomaId, Filme_FilmeId FROM IdiomaFilme";
        public static string RemoverIdiomaFilme = "DELETE FROM IdiomaFilme";

        public static string AdicionaFiltro(string consulta, int parametroFilmeId, string parametroIdiomaId)
        {
            List<string> filtros = new List<string>();

            if (parametroFilmeId > 0)
                filtros.Add(" Filme_FilmeId = @FilmeId ");

            if (!string.IsNullOrEmpty(parametroIdiomaId))
                filtros.Add(" Idioma_IdiomaId = @IdiomaId ");

            if (filtros.Count > 0)
                return string.Format("{0} WHERE {1} ", consulta, string.Join(" AND ", filtros));

            return consulta;
        }
    }
}
