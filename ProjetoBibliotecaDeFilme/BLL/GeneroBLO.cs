using ProjetoBibliotecaDeFilme.Context;
using ProjetoBibliotecaDeFilme.DAL;
using ProjetoBibliotecaDeFilme.Model;
using ProjetoBibliotecaDeFilme.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoBibliotecaDeFilme.BLL
{
    /// <summary>
    /// Classe de Negocios de Genero.
    /// </summary>
    public class GeneroBLO
    {
        /// <summary>
        /// Armazena o Context do entity.
        /// </summary>
        private readonly ContextBibliotecaDeFilme _context;

        /// <summary>
        /// Armazena instancia da classe de dados de Genero.
        /// </summary>
        private readonly GeneroDAO _generoDAO;

        /// <summary>
        /// Construtor Padrão.
        /// </summary>
        public GeneroBLO()
        {
            _context = new ContextBibliotecaDeFilme();
            _generoDAO = new GeneroDAO(_context);
        }

        /// <summary>
        /// Retorna uma Lista de Generos Cadastrados.
        /// </summary>
        /// <returns>Retorna Lista de Generos.</returns>
        public IEnumerable<Genero> Listar()
        {
            return _generoDAO.Listar();
        }

        /// <summary>
        /// Busca Genero por Id.
        /// </summary>
        /// <param name="id">Valor a ser Comparado.</param>
        /// <returns>Retorna Genero Encontrado</returns>
        public Genero BuscarPorId(int id)
        {
            return _generoDAO.BuscarPorId(id);
        }

        /// <summary>
        /// Salva Genero. 
        /// </summary>
        /// <param name="genero">Genero a ser Salvo</param>
        public void Salvar(Genero genero)
        {
            ValidaGenero(genero);
            JaExiste(genero);

            _generoDAO.Salvar(genero);
        }

        /// <summary>
        /// Editar Genero.
        /// </summary>
        /// <param name="genero">Genero a ser Editado.</param>
        public void Editar(Genero genero)
        {
            ValidaGenero(genero);
            _generoDAO.Editar(genero);
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
            var jaExiste = _generoDAO.JaExiste(genero);
            if (jaExiste)
            {
                throw new ProjetoException(String.Format("O Genero {0} - {1} Já Existe.", 
                                                         genero.GeneroId, genero.Descricao));
            }
        }

        /// <summary>
        /// Excluir Genero.
        /// </summary>
        /// <param name="idGenero">Valor a ser Excluido.</param>
        public void Excluir(int idGenero)
        {
            var genero = _generoDAO.BuscarPorId(idGenero);
            _generoDAO.Excluir(genero);
        }
    }
}
