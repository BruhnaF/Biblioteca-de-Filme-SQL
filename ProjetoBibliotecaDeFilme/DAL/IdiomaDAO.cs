using ProjetoBibliotecaDeFilme.Context;
using ProjetoBibliotecaDeFilme.Model;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Data.Entity;

namespace ProjetoBibliotecaDeFilme.DAL
{
    /// <summary>
    /// Classe para manipulação de Dados do Idioma.
    /// </summary>
    public class IdiomaDAO
    {
        /// <summary>
        /// armazena o context do entity framework.
        /// </summary>
        private readonly ContextBibliotecaDeFilme _context;

        /// <summary>
        /// Construtor para bloquear a Inicialização sem parametro.
        /// </summary>
        public IdiomaDAO() { }

        /// <summary>
        /// Construtor Padrão.
        /// </summary>
        /// <param name="context">Objeto com o contexto do entity.</param>
        public IdiomaDAO(ContextBibliotecaDeFilme context)
        {
            _context = context;
        }

        /// <summary>
        /// Lista os idiomas Cadastrados.
        /// </summary>
        /// <returns>Lista de Idiomas.</returns>
        public IEnumerable<Idioma> Listar()
        {
            return _context.Idiomas.ToList();
        }

        /// <summary>
        /// Salva Idioma.
        /// </summary>
        /// <param name="idioma">Idioma a ser Salvo.</param>
        public void Salvar(Idioma idioma)
        {
            _context.Idiomas.Add(idioma);
            _context.SaveChanges();
        }

        /// <summary>
        /// Busca no Context por Id.
        /// </summary>
        /// <param name="idioma">Valor a ser Comparado.</param>
        /// <returns></returns>
        public Idioma BuscarPorId(string idiomaId)
        {
            var idioma = _context.Idiomas.Where(x => x.IdiomaId == idiomaId).FirstOrDefault();
            return idioma;
        }

        /// <summary>
        /// Verifica se dados recebidos, ja existem no Context.
        /// </summary>
        /// <param name="idioma">Valor a ser Comparado.</param>
        /// <returns></returns>
        public bool JaExiste(Idioma idioma)
        {
            var jaExiste = false;
            var idiomaId = _context.Idiomas.Where(x => x.IdiomaId == idioma.IdiomaId).FirstOrDefault();
            var descricao = _context.Idiomas.Where(x => x.Descricao == idioma.Descricao).FirstOrDefault();

            if (idiomaId != null || descricao != null)
            {
                jaExiste = true;
            }

            return jaExiste;
        }

        /// <summary>
        /// Edita Idioma.
        /// </summary>
        /// <param name="idioma">Idioma a ser Editado. </param>
        public void Editar(Idioma idioma)
        {
            _context.Entry(idioma).State = EntityState.Modified;
            _context.SaveChanges();
        }

        /// <summary>
        /// Excluir Idioma.
        /// </summary>
        /// <param name="idioma">Idioma a ser Excluido.</param>
        public void Excluir(Idioma idioma)
        {
            _context.Idiomas.Remove(idioma);
            _context.SaveChanges();
        }
    }
}
