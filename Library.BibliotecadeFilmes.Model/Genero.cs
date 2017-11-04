using System;
using System.Collections.Generic;

namespace ProjetoBibliotecaDeFilme.Library.BibliotecadeFilme.Model
{
    public class Genero
    {
        public Genero()
        {

        }
        
        public int GeneroId { get; set; }

        public string Descricao { get; set; }

        public virtual List<Filme> Filmes { get; set; }        
    }
}
