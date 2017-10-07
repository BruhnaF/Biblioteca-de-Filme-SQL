using ProjetoBibliotecaDeFilme.Context;
using ProjetoBibliotecaDeFilme.DAL;
using ProjetoBibliotecaDeFilme.Model;
using System.Collections.Generic;

namespace ProjetoBibliotecaDeFilme.BLL
{
    public class NomedoFilmeBLO
    {
        /// <summary>
        ///  Classe para manipulação de Dados do NomedoFilme.
        /// </summary>
        private readonly ContextBibliotecaDeFilme _context;

        /// <summary>
        /// Representa NomedoFilmeDAO.
        /// </summary>
        private readonly NomedoFilmeDAO _nomedoFilmeDAO;

        /// <summary>
        /// Construtor Padrão;
        /// </summary>
        public NomedoFilmeBLO()
        {
            _context = new ContextBibliotecaDeFilme();
            _nomedoFilmeDAO = new NomedoFilmeDAO(_context);
        }

        /// <summary>
        /// Buscar por Id.
        /// </summary>
        /// <param name="id">Valor a ser encontrado.</param>
        /// <returns>Valor encontrado.</returns>
        public NomedoFilme BuscarporId(int codNomeFilme)
        {
            return _nomedoFilmeDAO.BuscarporId(codNomeFilme);
        }

        /// <summary>
        /// Buscar por Nome do Filme.
        /// </summary>
        /// <param name="nomeFilme">Nome do Filme a ser encontrado.</param>
        /// <returns>Retorna Nome do filme encontrado.</returns>
        public NomedoFilme BurcarporNomedoFilme(string nomeFilme)
        {
            return _nomedoFilmeDAO.BurcarporNomedoFilme(nomeFilme);
        }

        /// <summary>
        /// Listar todos os Nomes de Filmes.
        /// </summary>
        /// <returns>Retorna Lista de Nomes de Filmes.</returns>
        public IEnumerable<NomedoFilme> Listar()
        {
            return _nomedoFilmeDAO.Listar();
        }
    }
}
