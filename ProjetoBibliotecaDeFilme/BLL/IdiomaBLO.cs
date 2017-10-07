using ProjetoBibliotecaDeFilme.Context;
using ProjetoBibliotecaDeFilme.DAL;
using ProjetoBibliotecaDeFilme.Model;
using System.Collections.Generic;
using System;
using System.Text;
using ProjetoBibliotecaDeFilme.Utils;

namespace ProjetoBibliotecaDeFilme.BLL
{
    /// <summary>
    /// Classe de Negocios de idioma.
    /// </summary>
    public class IdiomaBLO
    {
        /// <summary>
        /// Armazena o Context do entity.
        /// </summary>
        private readonly ContextBibliotecaDeFilme _context;

        /// <summary>
        /// Armazena instancia da classe de dados de Idioma.
        /// </summary>
        private readonly IdiomaDAO _idiomaDAO;

        /// <summary>
        /// Construtor Padrão.
        /// </summary>
        public IdiomaBLO()
        {
            _context = new ContextBibliotecaDeFilme();
            _idiomaDAO = new IdiomaDAO(_context);
        }

        /// <summary>
        /// Retorna uma Lista de Idiomas Cadastrados.
        /// </summary>
        /// <returns>Retorna Lista de idiomas.</returns>
        public IEnumerable<Idioma> Listar()
        {
            return _idiomaDAO.Listar();
        }

        /// <summary>
        /// Busca Idioma por Id.
        /// </summary>
        /// <param name="idiomaId">Valor a ser Comparado.</param>
        /// <returns>Retorna Idioma encontrado</returns>
        public Idioma BuscarPorId(string idiomaId)
        {
            return _idiomaDAO.BuscarPorId(idiomaId);
        }

        /// <summary>
        /// Salva Idioma.
        /// </summary>
        /// <param name="idioma">Valor a ser salvo.</param>
        public void Salvar(Idioma idioma)
        {
            ValidaIdioma(idioma);
            JaExiste(idioma);

            _idiomaDAO.Salvar(idioma);
        }

        /// <summary>
        /// Verifica se Idioma recebido já existe no Context.
        /// </summary>
        /// <param name="idioma">Valor a ser Comparado.</param>
        /// <returns>True = Verdadeiro, False = False.</returns>
        public void JaExiste(Idioma idioma)
        {
            var jaExiste = _idiomaDAO.JaExiste(idioma);
            if (jaExiste)
            {
                throw new ProjetoException(String.Format("O Idioma {0} - {1} Já Exite", 
                                                        idioma.IdiomaId, idioma.Descricao));
            }
        }

        /// <summary>
        /// Edita Idioma.
        /// </summary>
        /// <param name="idioma">Valor a ser Editado.</param>
        public void Editar(Idioma idioma)
        {
            ValidaIdioma(idioma);
            _idiomaDAO.Editar(idioma);
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
            var idioma = _idiomaDAO.BuscarPorId(idIdioma);
            _idiomaDAO.Excluir(idioma);
        }
    }
}
