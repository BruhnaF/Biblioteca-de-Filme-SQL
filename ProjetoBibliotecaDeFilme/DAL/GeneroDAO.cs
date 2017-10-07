using ProjetoBibliotecaDeFilme.Context;
using ProjetoBibliotecaDeFilme.Model;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ProjetoBibliotecaDeFilme.DAL
{
    class GeneroDAO
    {
        /// <summary>
        /// Classe para manipulação de Dados do Genero.
        /// </summary>
        private readonly ContextBibliotecaDeFilme _context;

        /// <summary>
        /// Construtor para bloquear a Inicialização sem parametro.
        /// </summary>
        private GeneroDAO() { }

        /// <summary>
        /// Construtor Padrão.
        /// </summary>
        /// <param name="context">Objeto com o contexto do entity.</param>
        public GeneroDAO(ContextBibliotecaDeFilme context)
        {
            _context = context;
        }

        /// <summary>
        /// Lista os Generos Cadastrados.
        /// </summary>
        /// <returns>Retorna Lista de Generos.</returns>
        public IEnumerable<Genero>Listar()
        {
            return _context.Generos.ToList();
        }

        /// <summary>
        /// Salva Genero.
        /// </summary>Idioma a ser salvo.</param>
        public void Salvar(Genero genero)
        {
            _context.Generos.Add(genero);
            _context.SaveChanges();
        }

        /// <summary>
        /// Editar Genero.
        /// </summary>
        /// <param name="genero">Genero a ser Editado.</param>
        public void Editar(Genero genero)
        {
            _context.Entry(genero).State = EntityState.Modified;
            _context.SaveChanges(); 
        }

        /// <summary>
        /// Busca Genero por Id no Context.
        /// </summary>
        /// <param name="id">Valor a ser Comparado.</param>
        /// <returns>Retorna Genero Encontrado</returns>
        public Genero BuscarPorId(int id)
        {
            return _context.Generos.Find(id);
        }

        /// <summary>
        /// Verifica se Genero Informado já existe no Context.
        /// </summary>
        /// <param name="genero">Genero a ser Comparado.</param>
        /// <returns>Retorna True se Verdadeiro e False se Falso.</returns>
        public bool JaExiste(Genero genero)
        {
            var jaExiste = false;
           // var generoId = _context.Generos.Where(x=>x.GeneroId == genero.GeneroId).FirstOrDefault();
            var descricao = _context.Generos.Where(x=>x.Descricao == genero.Descricao).FirstOrDefault();
            if (descricao != null)
            {
                jaExiste = true;
            }
            return jaExiste;
        }

        /// <summary>
        /// Excluir Genero.
        /// </summary>
        /// <param name="genero">Valor a ser Excluido.</param>
        public void Excluir(Genero genero)
        {
            _context.Generos.Remove(genero);
            _context.SaveChanges();
        }
    }
}
