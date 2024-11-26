using System.ComponentModel.DataAnnotations;

namespace ProjetoDAAW.Models
{
    public class Personagem
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Personagem")]
        public string Prsnag { get; set; }
        public Filme? Filme { get; set; }
        [Required]
        [Display(Name = "Filme")]
        public int FilmeId { get; set; }
        public Artista? Artista { get; set; }
        [Required]
        [Display(Name = "Artista")]
        public int ArtistaId { get; set; }
    }
}
