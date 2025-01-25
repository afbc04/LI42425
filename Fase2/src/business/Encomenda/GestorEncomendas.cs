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
            Interrompido = false;

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
            if (_encomendas.ContainsKey(id))
                return _encomendas[id];
            else
                return null;
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

        public EncomendaUnidade? GetEncomendaProduto(int encomenda, int produto) {
            return _encomendas[encomenda].Produtos[produto].Clone();
        }

        public void IniciarEncomendaProduto(int encomenda, int produto) {
            _encomendas[encomenda].Produtos[produto].Iniciar();
        }

        public void AtualizarEstadoEncomenda(int encomenda) {
            _encomendas[encomenda].AtualizarEstadoEncomenda();
        }
        
        public void AtualizarProgressoEncomenda(int encomenda, int produto) {
            _encomendas[encomenda].AtualizarProgressoEncomenda(produto);
        }

        public void AddEncomendaCarrinhoCompras(string cliente, CarrinhoCompras carrinhoCompras, IDictionary<string,Produto> produtos) {

            IList<Produto> lista = new List<Produto>();

            foreach((string p, int q) in carrinhoCompras.Produtos) {

                int quantidade = q;

                while (quantidade > 0) {
                    lista.Add(produtos[p]);
                    quantidade--;
                }

            }

            Encomenda e = new Encomenda(_encomenda_Numero,lista,cliente);
            _encomenda_Numero++;
            _encomendas[e.ID] = e;

        }

        public bool ProducaoInterrompida() {
            return this.Interrompido;
        }

    }

}