using ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.DAL;
using ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.DAL.Contexts;
using ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.BLL
{
    public class NomedoFilmeBLO
    {

        private readonly NomedoFilmeDAO nomedoFilmeDAO = new NomedoFilmeDAO();

        public void Salvar(NomedoFilme nomedoFilme)
        {
            using (SqlConnection objConexao = new SqlConnection(ContextGenero.strConexao))
            {
                using (SqlCommand objCommand = new SqlCommand())
                {
                    objConexao.Open();

                    nomedoFilmeDAO.Salvar(objCommand, objConexao, nomedoFilme);

                    objCommand.Clone();
                }
            }
        }

        public void Editar(NomedoFilme nomedoFilme)
        {           
            using (SqlConnection objConexao = new SqlConnection(ContextNomedoFilme.strConexao))
            {
                using (SqlCommand objCommand = new SqlCommand())
                {
                    objConexao.Open();

                    nomedoFilmeDAO.Editar(objCommand, objConexao, nomedoFilme);

                    objConexao.Close();
                }
            }
        }

        public void RemoverNomesFilme(int nomedoFilme)
        {
            using (SqlConnection objConexao = new SqlConnection(ContextNomedoFilme.strConexao))
            {
                using (SqlCommand objCommand = new SqlCommand())
                {
                    objConexao.Open();

                    nomedoFilmeDAO.RemoverNomesFilme(objCommand, objConexao, nomedoFilme);

                    objConexao.Close();
                }
            }
        }

        public List<NomedoFilme> BuscarporIdFilme(int idFilme)
        {
            var listanomedoFilme = new List<NomedoFilme>();
            using (SqlConnection objConexao = new SqlConnection(ContextNomedoFilme.strConexao))
            {
                using (SqlCommand objCommand = new SqlCommand())
                {
                    objConexao.Open();

                    listanomedoFilme = nomedoFilmeDAO.BuscarporIdFilme(objCommand, objConexao, idFilme);

                    objConexao.Close();
                }
            }
            return listanomedoFilme;
        }


        /// <summary>
        /// Buscar por Id.
        /// </summary>
        /// <param name="id">Valor a ser encontrado.</param>
        /// <returns>Valor encontrado.</returns>
        public NomedoFilme BuscarporId(int codNomeFilme)
        {
            var nomedoFilme = new NomedoFilme();
            using (SqlConnection objConexao = new SqlConnection(ContextNomedoFilme.strConexao))
            {
                using (SqlCommand objCommand = new SqlCommand())
                {
                    objConexao.Open();

                    nomedoFilme = nomedoFilmeDAO.BuscarporId(objCommand, objConexao, codNomeFilme);

                    objConexao.Close();
                }
            }
            return nomedoFilme;
        }

        /// <summary>
        /// Listar todos os Nomes de Filmes.
        /// </summary>
        /// <returns>Retorna Lista de Nomes de Filmes.</returns>
        public IEnumerable<NomedoFilme> Listar()
        {
            var listaNomedoFilme = new List<NomedoFilme>();
            using (SqlConnection objConexao = new SqlConnection(ContextFilme.strConexao))
            {
                using (SqlCommand objCommand = new SqlCommand())
                {
                    objConexao.Open();

                    listaNomedoFilme = nomedoFilmeDAO.Listar(objConexao, objCommand).ToList();

                    objConexao.Close();
                }
            }
            return listaNomedoFilme;
        }
    }
}
