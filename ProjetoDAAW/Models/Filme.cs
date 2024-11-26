using System.ComponentModel.DataAnnotations;

namespace ProjetoDAAW.Models
{
    public class Filme
    {
        public int Id { get; set; }

        [Required]
        public string Titulo { get; set; }

        [Required]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        [Required]
        [Display(Name = "Gênero")]
        public int GeneroId { get; set; }
        public List<Genero>? Genero { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Data de Lançamento")]
        public DateTime DtLancamento { get; set; }

        [Required]
        [Display(Name = "Artista")]
        public int ArtistaId { get; set; }
        public List<Artista>? Artista { get; set; }

        public List<Personagem>? Personagem { get; set; }

        [Required]
        public string Diretor { get; set; }

        [Required]
        [DataType(DataType.Url)]
        [Display(Name = "Capa do Filme")]
        public string FtCapaFilme { get; set; }
    }
}
