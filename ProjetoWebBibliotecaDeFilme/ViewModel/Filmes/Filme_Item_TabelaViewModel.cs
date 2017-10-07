using ProjetoWebBibliotecaDeFilme.ViewModel.NomesdoFilme;
using System.Collections.Generic;

namespace ProjetoWebBibliotecaDeFilme.ViewModel.Filmes
{
    public class Filme_Item_TabelaViewModel
    {
        /// <summary>
        /// Representa FilmeId.
        /// </summary>
        public int FilmeId { get; set; }

        /// <summary>
        /// Representa Descrição.
        /// </summary>
        public List<NomedoFilmeViewModel> listaNomeFilme { get; set; }
    }
}