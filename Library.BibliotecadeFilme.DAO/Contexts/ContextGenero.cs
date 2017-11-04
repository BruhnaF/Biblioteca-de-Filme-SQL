using System.Collections.Generic;

namespace ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.DAL.Contexts
{
    public class ContextGenero : Context
    {
        public static string ListarTodosGeneros = "SELECT GeneroId, Descricao FROM Genero ";
        public static string SalvarGenero = "INSERT INTO Genero (Descricao) VALUES (@Descricao) ";
        public static string AlterarGenero = "UPDATE Genero SET Descricao = @Descricao ";
        public static string ExcluirGenero = "DELETE FROM Genero ";

        public static string AdicionaFiltro(string consulta, int parametroGeneroId, string parametroDescricao)
        {
            List<string> filtros = new List<string>();

            if (parametroGeneroId > 0) 
                filtros.Add(" GeneroId = @GeneroId ");

            if (!string.IsNullOrEmpty(parametroDescricao))
                filtros.Add(" Descricao = @Descricao ");

            if (filtros.Count > 0)
                return string.Format("{0} WHERE {1} ", consulta, string.Join(" AND ", filtros));

            return consulta;
        }
    }
}
