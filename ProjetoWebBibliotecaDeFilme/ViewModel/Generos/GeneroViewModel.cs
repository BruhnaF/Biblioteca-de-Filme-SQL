using ProjetoBibliotecaDeFilme.Model;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;

namespace ProjetoWebBibliotecaDeFilme.ViewModel.Generos
{
    public class GeneroViewModel
    {
        /// <summary>
        /// Construtor Padrão.
        /// </summary>
        public GeneroViewModel()
        {

        }

        /// <summary>
        /// Construtor Recebendo o Genero
        /// </summary>
        /// <param name="genero"></param>
        public GeneroViewModel(Genero genero)
        {
            this.GeneroId = genero.GeneroId;
            this.Descricao = genero.Descricao;
        }

        /// <summary>
        /// Representa o GeneroId
        /// </summary>
        [DisplayName("Código do Genero")]
        public int GeneroId { get; set; }

        /// <summary>
        /// Representa a Descrição
        /// </summary>
        [DisplayName("Descrição")]
        public string Descricao { get; set; }

        public List<SelectListItem> Filmes { get; set; }

        public List<GeneroViewModel> ListaFilmes { get; set; }
    }
}