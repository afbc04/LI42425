
namespace business {

    public class Avaliacao {

        public string Autor {get; private set;}
        public int Rating {get; private set;}
        public string? Comentario {get; private set;}

        public Avaliacao(string autor, int rating, string? comentario) {
            Autor = autor;
            Rating = rating;
            Comentario = comentario;
        }

        public Avaliacao Clone() {
            return new Avaliacao(this.Autor,this.Rating,this.Comentario);
        }

    }

}