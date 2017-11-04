using System.Collections.Generic;

namespace ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.Model
{
    public class Idioma
    {
        public Idioma()
        {
                
        }
       
        public string IdiomaId { get; set; }

        public string Descricao { get; set; }

        public string FilmeId { get; set; }

        public virtual List<Filme> Filmes { get; set; }

    }
}
