using System.Configuration;

namespace ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.DAL.Contexts
{
    public abstract class Context
    {
        /// <summary>
        ///  pegar connection string do arquivo App.config.
        /// </summary>
        public static string strConexao = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
    }
}
