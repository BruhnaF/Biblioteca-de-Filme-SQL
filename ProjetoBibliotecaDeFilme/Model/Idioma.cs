using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoBibliotecaDeFilme.Model
{
    public class Idioma
    {
        public Idioma()
        {
                
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [MaxLength(9)]
        public string IdiomaId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Descricao { get; set; }

        public virtual List<Filme> Filmes { get; set; }

    }
}
