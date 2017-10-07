using ProjetoBibliotecaDeFilme.Model;
using ProjetoWebBibliotecaDeFilme.ViewModel.Generos;
using ProjetoWebBibliotecaDeFilme.ViewModel.Idiomas;
using ProjetoWebBibliotecaDeFilme.ViewModel.NomesdoFilme;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;

namespace ProjetoWebBibliotecaDeFilme.ViewModel.Filmes
{
    public class FilmeViewModel
    {
        /// <summary>
        /// Construtor Padrão.
        /// </summary>
        public FilmeViewModel()
        {
            var listaPadrao = new SelectListItem() { Text = "Selecione", Value = string.Empty };

            Generos = new List<SelectListItem>
            {
                listaPadrao
            };
            ListaGeneros = new List<GeneroViewModel>();

            Idiomas = new List<SelectListItem>
            {
                listaPadrao
            };
            ListaIdiomas = new List<IdiomaViewModel>();

            NomesdoFilme = new List<SelectListItem>
            {
                listaPadrao
            };

            ListaNomedoFilme = new List<NomedoFilmeViewModel>();
            
        }

        /// <summary>
        /// Construtor Recebendo Filme.
        /// </summary>
        /// <param name="filme"></param>
        public FilmeViewModel(Filme filme)
        {
            this.FilmeId = filme.FilmeId;
            this.Descricao = filme.Descricao;

            var listaPadrao = new SelectListItem() { Text = "Selecione", Value = string.Empty };

            Generos = new List<SelectListItem>
            {
                listaPadrao
            };
            ListaGeneros = new List<GeneroViewModel>();

            Idiomas = new List<SelectListItem>
            {
                listaPadrao
            };
            ListaIdiomas = new List<IdiomaViewModel>();

            NomesdoFilme = new List<SelectListItem>
            {
                listaPadrao
            };
            ListaNomedoFilme = new List<NomedoFilmeViewModel>();
        }

        /// <summary>
        /// Representa FilmeId
        /// </summary>
        [DisplayName("Código do Filme")]
        public int FilmeId { get; set; }

        /// <summary>
        /// Representa Descrição
        /// </summary>
        [DisplayName("Descrição do Filme")]
        public string Descricao { get; set; }

        /// <summary>
        /// Representa GeneroId
        /// </summary>
        [DisplayName("Generos")]
        public int GeneroId { get; set; }

        /// <summary>
        /// Representa IdiomaId
        /// </summary>
        [DisplayName("Idiomas")]
        public string IdiomaId { get; set; }

        /// <summary>
        /// Representa NomedoFilme
        /// </summary>
        [DisplayName("Nome do Filme")]
        public string NomedoFilme { get; set; }

        [DisplayName("Idioma")]
        public string IdiomaFilme { get; set; }


        public List<SelectListItem> Generos { get; set; }
        public List<GeneroViewModel> ListaGeneros { get; set; }

        public List<SelectListItem> Idiomas { get; set; }
        public List<IdiomaViewModel> ListaIdiomas { get; set; }
        
        public List<SelectListItem> NomesdoFilme { get; set; }
        public List<NomedoFilmeViewModel> ListaNomedoFilme { get; set; }



    }
}