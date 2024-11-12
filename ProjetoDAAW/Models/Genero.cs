namespace ProjetoDAAW.Models
{
    public class Genero
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public List<Filme> Filme { get; set; }
    }
}
