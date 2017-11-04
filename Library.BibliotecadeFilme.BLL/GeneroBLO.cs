using ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.Model;
using ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.Utils;
using ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.DAL;
using System;
using System.Collections.Generic;
using System.Text;
using ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.DAL.Contexts;
using System.Data.SqlClient;
using System.Linq;

namespace ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.BLL

{
    /// <summary>
    /// Classe de Negocios de Genero.
    /// </summary>
    public class GeneroBLO
    {
        private readonly GeneroDAO generoDAO = new GeneroDAO();

        /// <summary>
        /// Retorna uma Lista de Generos Cadastrados.
        /// </summary>
        /// <returns>Retorna Lista de Generos.</returns>
        public IEnumerable<Genero> Listar()
        {
            var listaGeneros = new List<Genero>();
            using (SqlConnection objConexao = new SqlConnection(ContextGenero.strConexao))
            {
                using (SqlCommand objCommand = new SqlCommand())
                {
                    objConexao.Open();

                    listaGeneros = generoDAO.Listar(objConexao, objCommand).ToList();

                    objConexao.Close();
                }
            }
            return listaGeneros;
        }

        /// <summary>
        /// Busca Genero por Id.
        /// </summary>
        /// <param name="id">Valor a ser Comparado.</param>
        /// <returns>Retorna Genero Encontrado</returns>
        public Genero BuscarPorId(int id)
        {
            var genero = new Genero();
            using (SqlConnection objConexao = new SqlConnection(ContextGenero.strConexao))
            {
                using (SqlCommand objCommand = new SqlCommand())
                {
                    objConexao.Open();

                    genero = generoDAO.BuscarPorId(objCommand, objConexao, id);

                    objConexao.Close();
                }
            }
            return genero;
        }

        /// <summary>
        /// Salva Genero. 
        /// </summary>
        /// <param name="genero">Genero a ser Salvo</param>
        public void Salvar(Genero genero)
        {
            ValidaGenero(genero);
            JaExiste(genero);
            using (SqlConnection objConexao = new SqlConnection(ContextGenero.strConexao))
            {
                using (SqlCommand objCommand = new SqlCommand())
                {
                    objConexao.Open();

                    generoDAO.Salvar(objCommand, objConexao, genero);

                    objConexao.Close();
                }
            }
        }

        /// <summary>
        /// Editar Genero.
        /// </summary>
        /// <param name="genero">Genero a ser Editado.</param>
        public void Editar(Genero genero)
        {
            ValidaGenero(genero);
            JaExiste(genero);

            using (SqlConnection objConexao = new SqlConnection(ContextGenero.strConexao))
            {
                using (SqlCommand objCommand = new SqlCommand())
                {
                    objConexao.Open();

                    generoDAO.Editar(objCommand, objConexao, genero);

                    objConexao.Close();
                }
            }
        }

        /// <summary>
        /// Valida Genero.
        /// </summary>
        /// <param name="genero">Genero a ser Validado.</param>
        public void ValidaGenero(Genero genero)
        {
            var mensagem = new StringBuilder();
            //var codigoEhNulo = Validacao.EhVazio(genero.GeneroId.ToString());
            var descricaoEhNulo = Validacao.EhVazio(genero.Descricao);
            var tamanhoDescricaoEhMAior = Validacao.TamanhoEhMaior(genero.Descricao, 50);

            //if (codigoEhNulo)
            //    mensagem.AppendLine("Codigo não pode ser Vazio.<br />");
          
            if (descricaoEhNulo)
                mensagem.Append("Descrição não pode ser Vazio. <br />");
            if (tamanhoDescricaoEhMAior)
                mensagem.Append("Descrição não pode ser maior que 50 caracteres. <br />");

            var EhOk = /*!codigoEhNulo &&*/ !descricaoEhNulo && !tamanhoDescricaoEhMAior;

            if (!EhOk)
            {
                throw new ProjetoException(mensagem.ToString());
            }
        }

        /// <summary>
        /// Verifica se Genero já existe no Context.
        /// </summary>
        /// <param name="genero">Genero a ser Comparado.</param>
        public void JaExiste(Genero genero)
        {
            using (SqlConnection objConexao = new SqlConnection(ContextGenero.strConexao))
            {
                using (SqlCommand objCommand = new SqlCommand())
                {
                    objConexao.Open();

                    var jaExiste = generoDAO.JaExiste(objCommand, objConexao, genero);
                    if (jaExiste)
                    {
                        throw new ProjetoException(String.Format("O Genero {0} - {1} Já Exite",
                                                                genero.GeneroId, genero.Descricao));
                    }
                    objConexao.Close();
                }
            }
        }

        /// <summary>
        /// Excluir Genero.
        /// </summary>
        /// <param name="idGenero">Valor a ser Excluido.</param>
        public void Excluir(int idGenero)
        {
            using (SqlConnection objConexao = new SqlConnection(ContextGenero.strConexao))
            {
                using (SqlCommand objCommand = new SqlCommand())
                {
                    objConexao.Open();

                    generoDAO.Excluir(objCommand, objConexao, idGenero);

                    objConexao.Close();
                }
            }
        }
    }
}
