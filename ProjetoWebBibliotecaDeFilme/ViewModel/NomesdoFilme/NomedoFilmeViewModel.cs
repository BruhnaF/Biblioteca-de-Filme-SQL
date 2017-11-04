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
            this.IdiomaNovo = new ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.Model.Idioma();
        }

        /// <summary>
        /// Construtor recebendo NomedoFilme
        /// </summary>
        /// <param name="nomedoFilme"></param>
        public NomedoFilmeViewModel(ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.Model.NomedoFilme nomedoFilme)
        {
            this.NomedoFilmeId = nomedoFilme.NomedoFilmeId;
            this.Nome = nomedoFilme.Nome;
            this.IdiomaId = nomedoFilme.IdiomaId;
           // this.IdiomaNovo = nomedoFilme.Idioma;
            this.FilmeId = nomedoFilme.FilmeId;
        }

        /// <summary>
        /// Construtor recebendo NomedoFilme
        /// </summary>
        /// <param name="nomedoFilme"></param>
        public NomedoFilmeViewModel(NomedoFilme nomedoFilme)
        {
            this.NomedoFilmeId = nomedoFilme.Id;
            this.Nome = nomedoFilme.Nome;
            this.IdiomaId = nomedoFilme.IdiomaId;
            this.Idioma = nomedoFilme.Idioma;            
        }

        /// <summary>
        /// Representa o NomedoFilmeId
        /// </summary>
        public int NomedoFilmeId { get; set; }

        public int FilmeId { get; set; }

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

        public ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.Model.Idioma IdiomaNovo { get; set; }

    }
}