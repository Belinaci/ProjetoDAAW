using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoDAAW.Models
{

    [Table("Contatos")]
    public class Contato
    {
        public int Id { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-ZÀ-ÿ\s]{5,}$", ErrorMessage = "O nome deve ser completo (mínimo de 5 caracteres, incluindo caracteres acentuados).")]
        public string Nome { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Insira um e-mail válido.")]
        public string Email { get; set; }
        [Required]
        public string Assunto { get; set; }
        [Required]
        public string Mensagem { get; set; }
    }   
}
