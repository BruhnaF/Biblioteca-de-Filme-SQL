using ProjetoBibliotecaDeFilme.Context;
using ProjetoBibliotecaDeFilme.Model;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ProjetoBibliotecaDeFilme.DAL
{
    public class NomedoFilmeDAO
    {
        /// <summary>
        /// Classe para manipulação de Dados do NomedoFilme.
        /// </summary>
        private readonly ContextBibliotecaDeFilme _context;

        /// <summary>
        ///  Construtor para bloquear a Inicialização sem parametro.
        /// </summary>
        public NomedoFilmeDAO() { }

        /// <summary>
        /// Construtor Padrão.
        /// </summary>
        /// <param name="context">Objeto com o context do Entity.</param>
        public NomedoFilmeDAO(ContextBibliotecaDeFilme context)
        {
            _context = context;
        }

        /// <summary>
        /// Buscar por Id.
        /// </summary>
        /// <param name="id">Valor a ser encontrado.</param>
        /// <returns>Valor encontrado.</returns>
        public NomedoFilme BuscarporId(int codNomeFilme)
        {
            return _context.NomesdoFilme.Find(codNomeFilme);
        }

        /// <summary>
        /// Listar todos os Nomes de Filmes.
        /// </summary>
        /// <returns>Retorna Lista de Nomes de Filmes.</returns>
        public IEnumerable<NomedoFilme> Listar()
        {
            return _context.NomesdoFilme.ToList();
        }

        /// <summary>
        /// Buscar por nome do Filme. 
        /// </summary>
        /// <param name="nomeFilme">Nome do Filme a ser encontrado.</param>
        /// <returns>Retorna nome encontrado.</returns>
        public NomedoFilme BurcarporNomedoFilme(string nomeFilme)
        {
            return _context.NomesdoFilme.Where(x => x.Nome == nomeFilme).FirstOrDefault(); 
        }
    }

}
