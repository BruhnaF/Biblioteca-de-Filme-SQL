using System.Collections.Generic;

namespace ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.Model
{
    public class Filme
    {
        public Filme()
        {

        }

     
        public int FilmeId { get; set; }

        public string Descricao { get; set; }

        public virtual List<Genero> Generos { get; set; }
        public virtual List<Idioma> Idiomas { get; set; }
        public virtual List<NomedoFilme> Nomes { get; set; }
    }
}
