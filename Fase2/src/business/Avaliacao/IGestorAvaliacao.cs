namespace business {

    public interface IGestorAvaliacao {

        public Avaliacao AddAvaliacao(string autor, int rating, string? comentario);
        public void RemoveAvaliacoes(string autor);
        public ISet<Avaliacao> GetAvaliacoes(string autor);
        public ISet<Avaliacao> GetAvaliacoes();

    }


}