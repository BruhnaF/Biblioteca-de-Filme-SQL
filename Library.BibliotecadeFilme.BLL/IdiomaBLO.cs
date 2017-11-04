using ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.DAL;
using ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.DAL.Contexts;
using ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.Model;
using ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.Utils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.BLL
{
    /// <summary>
    /// Classe de Negocios de idioma.
    /// </summary>
    public class IdiomaBLO
    {
        private readonly IdiomaDAO idiomaDAO = new IdiomaDAO();

        /// <summary>
        /// Retorna uma Lista de Idiomas Cadastrados.
        /// </summary>
        /// <returns>Retorna Lista de idiomas.</returns>
        public IEnumerable<Idioma> Listar()
        {
            var listaIdiomas = new List<Idioma>();
            using (SqlConnection objConexao = new SqlConnection(ContextIdioma.strConexao))
            {
                using (SqlCommand objCommand = new SqlCommand())
                {
                    objConexao.Open();

                    listaIdiomas = idiomaDAO.Listar(objConexao, objCommand).ToList();

                    objConexao.Close();
                }
            }

            return listaIdiomas;
        }

        /// <summary>
        /// Busca Idioma por Id.
        /// </summary>
        /// <param name="idiomaId">Valor a ser Comparado.</param>
        /// <returns>Retorna Idioma encontrado</returns>
        public Idioma BuscarPorId(string idiomaId)
        {
            var idioma = new Idioma();

            using (SqlConnection objConexao = new SqlConnection(ContextIdioma.strConexao))
            {
                using (SqlCommand objCommand = new SqlCommand())
                {
                    objConexao.Open();

                   idioma = idiomaDAO.BuscarPorId(objCommand, objConexao, idiomaId);

                    objConexao.Close();
                }
            }

            return idioma;
        }

        /// <summary>
        /// Salva Idioma.
        /// </summary>
        /// <param name="idioma">Valor a ser salvo.</param>
        public void Salvar(Idioma idioma)
        {
            ValidaIdioma(idioma);
            JaExiste(idioma);
            using (SqlConnection objConexao = new SqlConnection(ContextIdioma.strConexao))
            {
                using (SqlCommand objCommand = new SqlCommand())
                {
                    objConexao.Open();

                    idiomaDAO.Salvar(idioma, objConexao, objCommand);

                    objConexao.Close();
                }
            }
        }

        /// <summary>
        /// Verifica se Idioma recebido já existe no Context.(Cadastrar)
        /// </summary>
        /// <param name="idioma">Valor a ser Comparado.</param>
        /// <returns>True = Verdadeiro, False = False.</returns>
        public void JaExiste(Idioma idioma)
        {
            using (SqlConnection objConexao = new SqlConnection(ContextIdioma.strConexao))
            {
                using (SqlCommand objCommand = new SqlCommand())
                {
                    objConexao.Open();

                    var jaExiste = idiomaDAO.JaExiste(objCommand, objConexao, idioma);
                    if (jaExiste)
                    {
                        throw new ProjetoException(String.Format("O Idioma {0} - {1} Já Exite",
                                                                idioma.IdiomaId, idioma.Descricao));
                    }
                    objConexao.Close();
                }
            }                      
        }

        /// <summary>
        /// Verifica se Idioma recebido já existe no Context. (Editar)
        /// </summary>
        /// <param name="idioma">Valor a ser Comparado.</param>
        /// <returns>True = Verdadeiro, False = False.</returns>
        public void JaExisteEditar(Idioma idioma)
        {
            using (SqlConnection objConexao = new SqlConnection(ContextIdioma.strConexao))
            {
                using (SqlCommand objCommand = new SqlCommand())
                {
                    objConexao.Open();

                    var jaExiste = idiomaDAO.JaExisteEditar(objCommand, objConexao, idioma);
                    if (jaExiste)
                    {
                        throw new ProjetoException(String.Format("O Idioma {0} - {1} Já Exite",
                                                                idioma.IdiomaId, idioma.Descricao));
                    }
                    objConexao.Close();
                }
            }
        }

        /// <summary>
        /// Edita Idioma.
        /// </summary>
        /// <param name="idioma">Valor a ser Editado.</param>
        public void Editar(Idioma idioma)
        {
            ValidaIdioma(idioma);
            JaExisteEditar(idioma);

            using (SqlConnection objConexao = new SqlConnection(ContextIdioma.strConexao))
            {
                using (SqlCommand objCommand = new SqlCommand())
                {
                    objConexao.Open();

                    idiomaDAO.Editar(objCommand, objConexao, idioma);
                   
                    objConexao.Close();
                }
            }           
        }

        private void ValidaIdioma(Idioma idioma)
        {
            var mensagem = new StringBuilder();

            var codigoEhNulo = Validacao.EhVazio(idioma.IdiomaId);
            var tamanhoCodigoMaior = Validacao.TamanhoEhMaior(idioma.IdiomaId, 9);
            var descricaoEhNulo = Validacao.EhVazio(idioma.Descricao);
            var tamanhoDescricaoMaior = Validacao.TamanhoEhMaior(idioma.Descricao, 50);

            if (codigoEhNulo)
                mensagem.AppendLine("Codigo não pode ser Vazio.<br />");

            if (tamanhoCodigoMaior)
                mensagem.AppendLine("Codigo não pode ser maior que 9 caracteres.<br />");

            if (descricaoEhNulo)
                mensagem.AppendLine("Descrição não pode ser Vazia.<br />");

            if (tamanhoDescricaoMaior)
                mensagem.AppendLine("Descrição nao poder ser maior que 50 caracteres.<br />");

            var ehOK = !codigoEhNulo && !tamanhoCodigoMaior && !descricaoEhNulo && !tamanhoDescricaoMaior;

            if (!ehOK)
            {
                throw new ProjetoException(mensagem.ToString());
            }
        }

        /// <summary>
        /// Excluir Idioma.
        /// </summary>
        /// <param name="idIdioma">Valor a ser Excluido.</param>
        public void Excluir(string idIdioma)
        {
            using (SqlConnection objConexao = new SqlConnection(ContextIdioma.strConexao))
            {
                using (SqlCommand objCommand = new SqlCommand())
                {
                    objConexao.Open();

                    idiomaDAO.Excluir(objCommand, objConexao, idIdioma);

                    objConexao.Close();
                }
            }
        }
    }
}
