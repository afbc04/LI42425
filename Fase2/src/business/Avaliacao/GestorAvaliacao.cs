namespace business {

    public class GestorAvaliacao : IGestorAvaliacao {

        private IDictionary<string,ISet<Avaliacao>> _avaliacoes;
        public ISet<Avaliacao> Avaliacoes {

            set {

                _avaliacoes = new Dictionary<string,ISet<Avaliacao>>();

                foreach (Avaliacao a in value) {

                    string autor = a.Autor;

                    ISet<Avaliacao> lista = _avaliacoes[autor];
                    if (lista is null) {
                        lista = new HashSet<Avaliacao>();
                        _avaliacoes[autor] = lista;
                    }

                    lista.Add(a);

                }

            }
            get {

                ISet<Avaliacao> lista = new HashSet<Avaliacao>();
                foreach (ISet<Avaliacao> a1 in _avaliacoes.Values) {

                    foreach (Avaliacao a2 in a1) {
                        lista.Add(a2.Clone());
                    }

                }

                return lista;

            }

        }

        public GestorAvaliacao() {
            _avaliacoes = new Dictionary<string,ISet<Avaliacao>>();
        }

        public Avaliacao AddAvaliacao(string autor, int rating, string? comentario) {

            if (_avaliacoes.ContainsKey(autor) == false) {
                _avaliacoes[autor] = new HashSet<Avaliacao>();
            }
            ISet<Avaliacao> lista = _avaliacoes[autor];
            Avaliacao a = new Avaliacao(autor,rating,comentario);

            lista.Add(a);

            return a.Clone();

        }

        public void RemoveAvaliacoes(string autor) {
            _avaliacoes[autor].Clear();
        }

        public ISet<Avaliacao> GetAvaliacoes(string autor) {

            ISet<Avaliacao> lista = new HashSet<Avaliacao>();
            foreach (Avaliacao a in _avaliacoes[autor]) {
                lista.Add(a.Clone());
            }

            return lista;

        }

        public ISet<Avaliacao> GetAvaliacoes() {

            return this.Avaliacoes;

        }


    }

}