using ProjetoBibliotecaDeFilme.Model;
using System.ComponentModel;

namespace ProjetoWebBibliotecaDeFilme.ViewModel.Idiomas
{
    public class IdiomaViewModel
    {
        /// <summary>
        /// Construtor Padrão
        /// </summary>
        public IdiomaViewModel()
        {

        }

        /// <summary>
        /// Construtor recebendo o Idioma
        /// </summary>
        /// <param name="idioma"></param>
        public IdiomaViewModel(ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.Model.Idioma idioma)
        {
            this.IdiomaId = idioma.IdiomaId;
            this.Descricao = idioma.Descricao;
        }

        public IdiomaViewModel(Idioma idioma)
        {
            this.IdiomaId = idioma.IdiomaId;
            this.Descricao = idioma.Descricao;
        }

        /// <summary>
        /// Representa o  IdiomaId
        /// </summary>
        [DisplayName("Código Idioma")]
        public string IdiomaId { get; set; }

        /// <summary>
        /// Representa a Descrição
        /// </summary>
        [DisplayName("Descrição")]
        public string Descricao { get; set; }
    }
}