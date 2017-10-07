using System.Collections.Generic;
using System.ComponentModel;

namespace ProjetoWebBibliotecaDeFilme.ViewModel.Generos
{
    /// <summary>
    ///  Representa tela Index Genero Cadastrados.
    /// </summary>
    public class GeneroIndexViewModel
    {
        /// <summary>
        /// Construtor Padrão.
        /// </summary>
        public GeneroIndexViewModel()
        {
            Itens = new List<Genero_Item_TabelaViewModel>();
        }

        /// <summary>
        /// Representa o campo de busca da pagina.
        /// </summary>
        [DisplayName("Descrição do Genero")]
        public string Descricao { get; set; }

        /// <summary>
        /// Representa Lista de Generos.
        /// </summary>
        public List<Genero_Item_TabelaViewModel> Itens { get; set; }
    }
}