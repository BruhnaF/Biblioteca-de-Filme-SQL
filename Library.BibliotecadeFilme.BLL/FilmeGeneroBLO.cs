using ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.DAL;
using ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.DAL.Contexts;
using ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.Model;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.BLL
{
    public class FilmeGeneroBLO
    {
        private readonly FilmeGeneroDAO _filmeGeneroDAO = new FilmeGeneroDAO();
        private readonly GeneroDAO _generoDAO = new GeneroDAO();

        public void Salvar(int filmeId, int generoId)
        {
            using (SqlConnection objConexao = new SqlConnection(ContextFilmeGenero.strConexao))
            {
                using (SqlCommand objCommand = new SqlCommand())
                {
                    objConexao.Open();
                    _filmeGeneroDAO.Salvar(objCommand, objConexao, filmeId, generoId);
                    objConexao.Close();
                }
            }
        }

        public void Editar(int filmeId, int generoId)
        {
            using (SqlConnection objConexao = new SqlConnection(ContextFilmeGenero.strConexao))
            {
                using (SqlCommand objCommand = new SqlCommand())
                {
                    objConexao.Open();
                    _filmeGeneroDAO.Editar(objCommand, objConexao, filmeId, generoId);
                    objConexao.Close();
                }
            }
        }

        public void RemoverGeneroFilme(int filmeId, int generoId)
        {
            using (SqlConnection objConexao = new SqlConnection(ContextFilmeGenero.strConexao))
            {
                using (SqlCommand objCommand = new SqlCommand())
                {
                    objConexao.Open();
                    _filmeGeneroDAO.RemoverGeneroFilme(objCommand, objConexao, filmeId, generoId);
                    objConexao.Close();
                }
            }
        }

        public List<Genero> BuscarporFilmeId(int filmeId)
        {
            var listaGenero = new List<Genero>();
            using (SqlConnection objConexao = new SqlConnection(ContextFilmeGenero.strConexao))
            {
                using (SqlCommand objCommand = new SqlCommand())
                {
                    objConexao.Open();
                    var generoId = _filmeGeneroDAO.BuscarporIdFilme(objCommand, objConexao, filmeId);
                    foreach (var item in generoId)
                    {
                        var genero = _generoDAO.BuscarPorId(objCommand, objConexao, item);
                        listaGenero.Add(genero);
                    }
                    objConexao.Close();
                }
            }
            return listaGenero;
        }
    }
}
