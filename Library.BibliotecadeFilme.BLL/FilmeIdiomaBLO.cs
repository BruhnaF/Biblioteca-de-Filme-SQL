using ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.DAL;
using ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.DAL.Contexts;
using ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.Model;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.BLL
{
    public class FilmeIdiomaBLO
    {
        private readonly FilmeIdiomaDAO _filmeIdiomaDAO = new FilmeIdiomaDAO();
        private readonly IdiomaDAO _idiomaDAO = new IdiomaDAO();

        public void Salvar(int filmeId, string idiomaId)
        {
            using (SqlConnection objConexao = new SqlConnection(ContextFilmeIdioma.strConexao))
            {
                using (SqlCommand objCommand = new SqlCommand())
                {
                    objConexao.Open();
                        _filmeIdiomaDAO.Salvar(objCommand, objConexao, filmeId, idiomaId);
                    objConexao.Close();
                }
            }
        }

        public void Editar(int filmeId, string idiomaId)
        {
            using (SqlConnection objConexao = new SqlConnection(ContextFilmeIdioma.strConexao))
            {
                using (SqlCommand objCommand = new SqlCommand())
                {
                    objConexao.Open();
                    _filmeIdiomaDAO.Editar(objCommand, objConexao, filmeId, idiomaId);
                    objConexao.Close();
                }
            }
        }

        public void RemoverIdiomaFilme(int filmeId, string idiomaId)
        {
            using (SqlConnection objConexao = new SqlConnection(ContextFilmeGenero.strConexao))
            {
                using (SqlCommand objCommand = new SqlCommand())
                {
                    objConexao.Open();
                    _filmeIdiomaDAO.RemoverIdiomaFilme(objCommand, objConexao, idiomaId, filmeId);
                    objConexao.Close();
                }
            }
        }

        public List<Idioma> BuscarporIdFilme(int filmeId)
        {
            var listaIdioma = new List<Idioma>();
            using (SqlConnection objConexao = new SqlConnection(ContextFilmeIdioma.strConexao))
            {
                using (SqlCommand objCommand = new SqlCommand())
                {
                    objConexao.Open();
                    var idiomaId = _filmeIdiomaDAO.BuscarporIdFilme(objCommand, objConexao, filmeId);
                    foreach (var item in idiomaId)
                    {
                        var idioma = _idiomaDAO.BuscarPorId(objCommand, objConexao, item);
                        listaIdioma.Add(idioma);
                    }
                    objConexao.Close();
                }
            }
            return listaIdioma;
        }
    }
}
