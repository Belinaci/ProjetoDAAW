namespace ProjetoDAAW.Models
{
    public class Personagem
    {
        public int Id { get; set; }
        public string Prsnag { get; set; }
        public Filme? Filme { get; set; }
        public int FilmeId { get; set; }
        public Artista? Artista { get; set; }
        public int ArtistaId { get; set; }
    }
}
