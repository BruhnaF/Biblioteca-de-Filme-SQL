using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoBibliotecaDeFilme.Model
{
    public class NomedoFilme
    {
        public NomedoFilme()
        {

        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nome { get; set; }

        public string IdiomaId { get; set; }

        [ForeignKey("IdiomaId")]
        public Idioma Idioma { get; set; }
    }
}
