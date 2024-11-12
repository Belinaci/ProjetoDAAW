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
        public DateTime DtNascimento { get; set; }

        [Required]
        public string Pais { get; set; }

        [Required]
        [DataType(DataType.Url)]
        public string FtArtista { get; set; }
    }
}
