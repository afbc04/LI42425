namespace business {

    public class GestorFAQ : IFAQ {

        private IList<FAQAux> _faq;
        public ISet<FAQ> FAQ {

            get {
                ISet<FAQ> lista = new HashSet<FAQ>();
                int i = 0;

                foreach (FAQAux a in _faq) {
                    i++;
                    lista.Add(new FAQ(i,a.Pergunta,a.Resposta));
                }

                return lista;

            }
            set {

                _faq = new List<FAQAux>();

                foreach(FAQ a in value) {
                    _faq.Add(new FAQAux(a.Pergunta,a.Resposta));
                }

            }

        }

        public GestorFAQ() {

            this._faq = new List<FAQAux>();

        }

        public int AddFAQ(string pergunta, string resposta) {
            FAQAux f = new FAQAux(pergunta,resposta);
            _faq.Add(f);
            return _faq.Count;
        }

        public void RemoveFAQ(int idFAQ) {
            _faq.RemoveAt(idFAQ - 1);
        }

        public ISet<FAQ> GetFAQ() {
            return this.FAQ;
        }

        private class FAQAux(string pergunta, string resposta)
        {
            public string Pergunta { get; set; } = pergunta;
            public string Resposta { get; set; } = resposta;
        }

    }


}