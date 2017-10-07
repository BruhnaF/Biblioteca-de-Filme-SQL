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
    /// Classe de Negocios do Filme
    /// </summary>
    public class FilmeBLO
    {
        /// <summary>
        /// Armazena o Context.
        /// </summary>
        private readonly ContextBibliotecaDeFilme _context;

        /// <summary>
        /// Armazena Instancia do FilmeDAO.
        /// </summary>
        private readonly FilmeDAO _filmeDAO;

        /// <summary>
        /// Armazena Instancia do GeneroDAO.
        /// </summary>
        private readonly GeneroDAO _generoDAO;

        /// <summary>
        /// Armazena Instancia do IdiomaDAO.
        /// </summary>
        private readonly IdiomaDAO _idiomaDAO;

        /// <summary>
        /// Armazena Instancia do NomeFilme.
        /// </summary>
        private readonly NomedoFilmeDAO _nomeFilmeDAO;


        /// <summary>
        /// Construtor Padrão.
        /// </summary>
        public FilmeBLO()
        {
            _context = new ContextBibliotecaDeFilme();
            _filmeDAO = new FilmeDAO(_context);
            _generoDAO = new GeneroDAO(_context);
            _idiomaDAO = new IdiomaDAO(_context);
            _nomeFilmeDAO = new NomedoFilmeDAO(_context);
        }

        /// <summary>
        /// Busca por Filmes.
        /// </summary>
        /// <returns>Lista de Filmes.</returns>
        public IEnumerable<Filme> Listar()
        {
            return _filmeDAO.Listar();
        }

        /// <summary>
        /// Buscar por Id.
        /// </summary>
        /// <param name="id">Id a ser Comparado.</param>
        /// <returns>Valor Encontrado.</returns>
        public Filme BuscarPorId(int id)
        {
            return _filmeDAO.BuscarPorId(id);
        }

        /// <summary>
        /// Salvar Filme.
        /// </summary>
        /// <param name="filme">Filme a ser Salvo.</param>
        public void Salvar(Filme filme)
        {
            var novoFilme = new Filme { FilmeId = filme.FilmeId, Descricao = filme.Descricao,
                    Generos = new List<Genero>(), Idiomas = new List<Idioma>(),  Nomes = new List<NomedoFilme>()};

            if (novoFilme != null)
            {


                foreach (var item in filme.Nomes)
                {                  
                    novoFilme.Nomes.Add(new NomedoFilme { IdiomaId = item.IdiomaId, Nome = item.Nome});
                }

                foreach (var item in filme.Generos)
                {
                    novoFilme.Generos.Add(_generoDAO.BuscarPorId(item.GeneroId));
                }

                foreach (var item in filme.Idiomas)
                {
                    novoFilme.Idiomas.Add(_idiomaDAO.BuscarPorId(item.IdiomaId));
                }
            }

            ValidaFilme(filme);
           //JaExiste(filme);

            _filmeDAO.Salvar(novoFilme);
        }

        /// <summary>
        /// Editar Filme.
        /// </summary>
        /// <param name="filme">Filme a ser Editado.</param>
        public void Editar(Filme filme)
        {
            var novoFilme = BuscarPorId(filme.FilmeId);
            novoFilme.Descricao = filme.Descricao;           
                     

            if (filme.Generos.Count > 0)
            {
                if (novoFilme.Generos == null)
                    novoFilme.Generos = new List<Genero>();

                foreach (var item in filme.Generos)
                {
                    var generos = _generoDAO.BuscarPorId(item.GeneroId);
                    novoFilme.Generos.Add(generos);
                }           
            }

            if (filme.Idiomas.Count > 0)
            {
                if (novoFilme.Idiomas == null)
                    novoFilme.Idiomas = new List<Idioma>();

                foreach (var item in filme.Idiomas)
                {
                    var idiomas = _idiomaDAO.BuscarPorId(item.IdiomaId);
                    novoFilme.Idiomas.Add(idiomas);
                }
            }

            ValidaFilme(filme);

            _filmeDAO.Editar(novoFilme);
            filme = novoFilme;
        }

        public void Excluir(int idFilme)
        {
            var filme = _filmeDAO.BuscarPorId(idFilme);
            _filmeDAO.Excluir(filme);
        }

        public void RemoverItensFilme(Filme filme)
        {
            _filmeDAO.Editar(filme);          
        }

        /// <summary>
        /// Verifica se Filme já existe no Context.
        /// </summary>
        /// <param name="filme">Filme a ser Comparado.</param>
        public void JaExiste(Filme filme)
        {
            var jaExiste = _filmeDAO.JaExiste(filme);
            if (jaExiste)
            {
                throw new ProjetoException("O Filme Já Existe.");
            }
        }

        /// <summary>
        /// Valida Filme.
        /// </summary>
        /// <param name="filme">Filme a ser Validado.</param>
        public void ValidaFilme(Filme filme)
        {
            var mensagem = new StringBuilder();
            var codigoEhNulo = Validacao.EhVazio(filme.FilmeId.ToString());
            var descricaoEhNulo = Validacao.EhVazio(filme.Descricao);
            var tamanhoDescricaoEhMAior = Validacao.TamanhoEhMaior(filme.Descricao, 50);

            if (codigoEhNulo)
                mensagem.AppendLine("Codigo não pode ser Vazio.<br />");

            if (descricaoEhNulo)
                mensagem.Append("Descrição não pode ser Vazio. <br />");
            if (tamanhoDescricaoEhMAior)
                mensagem.Append("Descrição não pode ser maior que 50 caracteres. <br />");

            var EhOk = !codigoEhNulo && !descricaoEhNulo && !tamanhoDescricaoEhMAior;

            if (!EhOk)
            {
                throw new ProjetoException(mensagem.ToString());
            }
        }
    }
}
