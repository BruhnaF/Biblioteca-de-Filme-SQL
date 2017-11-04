using System.Collections.Generic;

namespace ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.DAL.Contexts
{
    public class ContextFilme : Context
    {
        public static string ListarTodosFilmes = "SELECT FilmeId, Descricao FROM Filme ";
        public static string SalvarFilme = "INSERT INTO Filme (Descricao) VALUES (@Descricao); SELECT SCOPE_IDENTITY() ";
        public static string AlterarFilme = "UPDATE Filme SET Descricao = @Descricao";
        public static string ExcluirFilme = "DELETE FROM Filme";

        public static string AdicionaFiltro(string consulta, int parametroFilmeId, string parametroDescricao)
        {
            List<string> filtros = new List<string>();

            if (parametroFilmeId > 0)
                filtros.Add(" FilmeId = @FilmeId ");

            if (!string.IsNullOrEmpty(parametroDescricao))
                filtros.Add(" Descricao = @Descricao ");

            if (filtros.Count > 0)
                return string.Format("{0} WHERE {1} ", consulta, string.Join(" AND ", filtros));

            return consulta;
        }
    }
}
