using ProjetoBibliotecaDeFilme.Context;
using ProjetoBibliotecaDeFilme.Model;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ProjetoBibliotecaDeFilme.DAL
{
    public class FilmeDAO
    {
        /// <summary>
        /// Classe para manipulação de Dados do Filme.
        /// </summary>
        private readonly ContextBibliotecaDeFilme _context;

        /// <summary>
        /// Construtor para bloquear a Inicialização sem parametro.
        /// </summary>
        private FilmeDAO() { }

        /// <summary>
        /// Construtor Padrão
        /// </summary>
        /// <param name="context">Objeto com o contexto do entity</param>
        public FilmeDAO(ContextBibliotecaDeFilme context)
        {
            _context = context;
        }

        /// <summary>
        /// Listar Filmes.
        /// </summary>
        /// <returns>Lista de Filmes.</returns>
        public  IEnumerable<Filme> Listar()
        {
            return _context.Filmes.ToList();
        }

        /// <summary>
        /// Buscar Filme por Id.
        /// </summary>
        /// <param name="id">Valor a ser Comparado.</param>
        /// <returns>Valor Encontrado.</returns>
        public Filme BuscarPorId(int id)
        {
            return _context.Filmes.Find(id);
        }

        /// <summary>
        /// Salvar Filme.
        /// </summary>
        /// <param name="filme">Filme a ser Salvo.</param>
        public void Salvar(Filme filme)
        {
            _context.Filmes.Add(filme);
            _context.SaveChanges();
        }

        /// <summary>
        /// Editar Filme.
        /// </summary>
        /// <param name="filme">Filme a ser Editado.</param>
        public void Editar(Filme filme)
        {
            _context.Entry(filme).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Excluir(Filme filme)
        {
            _context.Filmes.Remove(filme);
            _context.SaveChanges();
        }

        /// <summary>
        /// Verifica se Filme Informado já existe no Context.
        /// </summary>
        /// <param name="filme">Filme a ser Comparado.</param>
        /// <returns>Retorna True se Verdadeiro e False se Falso.</returns>
        public bool JaExiste(Filme filme)
        {
            var jaExiste = false;
            var filmeId = _context.Filmes.Where(x => x.FilmeId == filme.FilmeId).FirstOrDefault();       
               
                if (filmeId != null)
                {
                    jaExiste = true;
                }            
            return jaExiste;
        }
    }
}
