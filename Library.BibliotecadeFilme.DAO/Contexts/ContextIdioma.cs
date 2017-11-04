using System.Collections.Generic;

namespace ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.DAL.Contexts
{
    public class ContextIdioma : Context
    {
        public static string ListarTodosIdiomas = "SELECT IdiomaId, Descricao FROM Idioma ";
        public static string SalvarIdioma = "INSERT INTO Idioma (IdiomaId, Descricao) VALUES (@IdiomaId, @Descricao) ";
        public static string AlterarIdioma = "UPDATE Idioma SET IdiomaId = @IdiomaId, Descricao = @Descricao ";
        public static string ExcluirIdioma = "DELETE FROM Idioma ";


        public static string AdicionaFiltro(string consulta, string parametroIdiomaId, string parametroDescricao)
        {
            List<string> filtros = new List<string>();

            if (!string.IsNullOrEmpty(parametroIdiomaId))
                filtros.Add(" IdiomaId = @IdiomaId ");

            if (!string.IsNullOrEmpty(parametroDescricao))
                filtros.Add(" Descricao = @Descricao ");

            if (filtros.Count > 0)
                return string.Format("{0} WHERE {1} ", consulta, string.Join(" AND ", filtros));

            return consulta;
        }
    }
}
