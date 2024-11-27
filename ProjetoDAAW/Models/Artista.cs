using System.ComponentModel.DataAnnotations;

namespace ProjetoDAAW.Models
{
    public class Artista
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Data de Nascimento")]
        public DateTime DtNascimento { get; set; }

        [Required]
        [Display(Name = "País")]
        public string Pais { get; set; }

        [Required]
        [DataType(DataType.Url)]
        [Display(Name = "Foto do Artista")]

        public string FtArtista { get; set; }

        public List<Filme>? Filme { get; set; }
        public List<Personagem>? Personagem { get; set; }
    }
}
