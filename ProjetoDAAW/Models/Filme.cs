using System.ComponentModel.DataAnnotations;

namespace ProjetoDAAW.Models
{
    public class Filme
    {
        public int Id { get; set; }

        [Required]
        public string Titulo { get; set; }

        [Required]
        public string Descricao { get; set; }
        public int GeneroId { get; set; }
        public List<Genero>? Genero { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DtLancamento { get; set; }
        public int ArtistaId { get; set; }
        public List<Artista>? Artista { get; set; }

        public List<Personagem>? Personagem { get; set; }

        [Required]
        public string Diretor { get; set; }

        [Required]
        [DataType(DataType.Url)]
        public string FtCapaFilme { get; set; }
    }
}
