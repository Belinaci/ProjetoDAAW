using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
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
        public string Nacionalidade { get; set; }

        [Required]
        [DataType (DataType.Url)]
        public string FtArtista { get; set; }
    }
}
