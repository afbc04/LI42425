namespace business {

    public class GestorEncomendas : IGestorEncomendas {

        private IDictionary<int,Encomenda> _encomendas;
        public ISet<Encomenda> Encomendas {

            get {

                ISet<Encomenda> lista = new HashSet<Encomenda>();

                foreach(Encomenda e in _encomendas.Values)
                    lista.Add(e.Clone());

                return lista;

            }
            private set {

                _encomendas = new Dictionary<int,Encomenda>();

                foreach (Encomenda e in value) {
                    _encomendas[e.ID] = e.Clone();
                }

                _encomenda_Numero = _encomendas.Count +1;

            }

        }
        private int _encomenda_Numero;

        public bool Interrompido {get; private set;}

        public GestorEncomendas() {

            _encomenda_Numero = 1;
            _encomendas = new Dictionary<int,Encomenda>();

        }

        public bool CancelarEncomenda(int id) {

            if (_encomendas[id].Estado == EncomendaEstado.QUEUE) {
                _encomendas[id].AtualizarEstadoEncomenda(EncomendaEstado.INTERRUPTED);
                return true;
            }
            else
                return false;

        }

        public Encomenda? GetEncomenda(int id) {
            return _encomendas[id];
        }

        public ISet<Encomenda> GetEncomendasDoing(Filtro? filtro) {

            ISet<Encomenda> lista = new HashSet<Encomenda>();

            foreach(Encomenda e in _encomendas.Values) {

                if (e.Estado == EncomendaEstado.DOING) {

                    if (filtro is null || filtro.Filtrar(e) == true)
                        lista.Add(e.Clone());

                }

            }

            return lista;

        }

        public ISet<Encomenda> GetEncomendasDone(Filtro? filtro) {

            ISet<Encomenda> lista = new HashSet<Encomenda>();

            foreach(Encomenda e in _encomendas.Values) {

                if (e.Estado == EncomendaEstado.DONE) {

                    if (filtro is null || filtro.Filtrar(e) == true)
                        lista.Add(e.Clone());

                }

            }

            return lista;

        }

        public ISet<Encomenda> GetEncomendasQueue(Filtro? filtro) {

            ISet<Encomenda> lista = new HashSet<Encomenda>();

            foreach(Encomenda e in _encomendas.Values) {

                if (e.Estado == EncomendaEstado.QUEUE) {

                    if (filtro is null || filtro.Filtrar(e) == true)
                        lista.Add(e.Clone());

                }

            }

            return lista;

        }

        public Relatorio? GetEncomendaRelatorio(int encomenda) {
            return _encomendas[encomenda].Relatorio;
        }

        public void InterromperEncomendas() {
            Interrompido = true;
        }

        public void RetomarEncomendas() {
            Interrompido = false;
        }

        public void AtualizarEstadoEncomenda(int encomenda) {
            _encomendas[encomenda].AtualizarEstadoEncomenda();
        }
        
        public void AtualizarProgressoEncomenda(int encomenda, int produto) {
            _encomendas[encomenda].AtualizarProgressoEncomenda(produto);
        }

    }

}