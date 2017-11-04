using System.Collections.Generic;

namespace ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.DAL.Contexts
{
    public class ContextNomedoFilme : Context
    {
        public static string ListarTodosNomedoFilme = "SELECT Id, Nome, Filme_FilmeId, IdiomaId FROM NomedoFilme ";
        public static string SalvarNomedoFilme = "INSERT INTO NomedoFilme  (Nome, IdiomaId, Filme_FilmeId) VALUES (@Nome, @IdiomaId, @FilmeId)";
        public static string AlterarNomedoFilme = "UPDATE NomedoFilme SET Nome = @Nome, IdiomaId = @IdiomaId";
        public static string DeletarNomedoFilme = "DELETE FROM NomedoFilme";

        public static string AdicionaFiltro(string consulta, int parametroNomedoFilmeId, string parametroNomedoFilme, int parametroFilmeId, string parametroIdioma)
        {
            List<string> filtros = new List<string>();

            if (parametroNomedoFilmeId > 0)
                filtros.Add(" Id = @NomedoFilmeId ");

            if (!string.IsNullOrEmpty(parametroNomedoFilme))
                filtros.Add(" Nome = @Nome ");

            if (!string.IsNullOrEmpty(parametroNomedoFilme))
                filtros.Add(" IdiomaId = @IdiomaId ");

            if (parametroFilmeId > 0)
                filtros.Add(" Filme_FilmeId = @FilmeId ");

            if (filtros.Count > 0)
                return string.Format("{0} WHERE {1} ", consulta, string.Join(" AND ", filtros));

            return consulta;
        }
    }
}