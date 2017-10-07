using ProjetoBibliotecaDeFilme.Model;
using System.Collections.Generic;

namespace ProjetoWebBibliotecaDeFilme.ViewModel.NomesdoFilme
{
    public class NomedoFilmeViewModel
    {
        /// <summary>
        /// Construtor Padrão
        /// </summary>
        public NomedoFilmeViewModel()
        {
            this.Idioma = new Idioma();
        }

        /// <summary>
        /// Construtor recebendo NomedoFilme
        /// </summary>
        /// <param name="nomedoFilme"></param>
        public NomedoFilmeViewModel(NomedoFilme nomedoFilme)
        {
            this.Id = nomedoFilme.Id;
            this.Nome = nomedoFilme.Nome;
            this.IdiomaId = nomedoFilme.IdiomaId;
            this.Idioma = nomedoFilme.Idioma;            
        }

        /// <summary>
        /// Representa o NomedoFilmeId
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Representa o Nome
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Representa o IdiomaId
        /// </summary>
        public string IdiomaId { get; set; }

        /// <summary>
        /// Representa o Idioma
        /// </summary>
        public Idioma Idioma { get; set; }
    }
}