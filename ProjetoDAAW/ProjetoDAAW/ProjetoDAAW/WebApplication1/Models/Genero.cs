using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Genero
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        public List<Filme>? Filme { get; set; }
    }
}
