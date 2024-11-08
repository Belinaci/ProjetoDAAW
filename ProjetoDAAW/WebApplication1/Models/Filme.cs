using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Filme
    {
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        public string Genero { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DtLancamento { get; set; }
        [Required]
        public string Atores { get; set; }
        [Required]
        public string Diretor { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public string FtCapaFilme { get; set; }
    }
}
