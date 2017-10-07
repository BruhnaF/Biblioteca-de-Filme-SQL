using ProjetoWebBibliotecaDeFilme.ViewModel.NomesdoFilme;
using System.Collections.Generic;
using System.ComponentModel;

namespace ProjetoWebBibliotecaDeFilme.ViewModel.Filmes
{
    /// <summary>
    /// Representa a Tela Index de Filmes Cadastrados
    /// </summary>
    public class FilmeIndexViewModel
    {
        /// <summary>
        /// Construtor Padrão
        /// </summary>
        public FilmeIndexViewModel()
        {
            Itens = new List<Filme_Item_TabelaViewModel>();
        }

        /// <summary>
        /// Representa a Lista de Nomes de Filme
        /// </summary>
        [DisplayName("Lista de Nome do Filme")]
        public List<NomedoFilmeViewModel> listaNomeFilme { get; set; }

        /// <summary>
        /// Representa um Nome do Filme
        /// </summary>
        [DisplayName("Nome do Filme")]
        public string Nome { get; set; }

        /// <summary>
        /// Representa Lista de Filmes
        /// </summary>
        public List<Filme_Item_TabelaViewModel> Itens { get; set; }
    }
}