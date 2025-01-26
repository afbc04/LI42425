namespace business {

    public class Cliente : Utilizador {

        private ListaDeFavoritos _favs;
        public ListaDeFavoritos ListaDeFavoritos {
            get {
                return _favs.Clone();
            }
            set {
                _favs = value.Clone();
            }
        }

        private CarrinhoCompras _carr;
        public CarrinhoCompras CarrinhoCompras {

            get {
                return _carr.Clone();
            }
            set {
                _carr = value.Clone();
            }

        }

        private ISet<int> _encomendas;
        public ISet<int> Encomendas {

            get {
                return new HashSet<int>(_encomendas);
            }
            set {
                _encomendas = new HashSet<int>(value);
            }

        }
        

        public Cliente(string nome, string senha, string email) : base(nome,senha,email) {
            this._favs = new ListaDeFavoritos();
            this._encomendas = new HashSet<int>();
            this._carr = new CarrinhoCompras();
        }

        public Cliente(string nome, string senha, string email, string? telemovel, string? morada) : this(nome,senha,email) {
            Telemovel = telemovel;
            Morada = morada;
        }

        public void AddFavorito(string p) {
            _favs.AddProduto(p);
        }

        public void RemoveFavorito(string p) {
            _favs.RemoveProduto(p);
        }

        public ListaDeFavoritos GetListaFavoritos() {
            return ListaDeFavoritos;
        }

        public void AddProduto(string p) {
            _carr.AddProduto(p);
        }

        public void RemoveProduto(string p) {
            _carr.RemoveProduto(p);
        }

        public CarrinhoCompras GetCarrinhoCompras() {
            return CarrinhoCompras;
        }   

        public void AddEncomenda(int encomenda) {
            _encomendas.Add(encomenda);
        }

        public void RemoveEncomenda(int encomenda) {
            _encomendas.Remove(encomenda);
        }

        public ISet<int> GetEncomendas() {
            return Encomendas;
        }

        public void EsvaziarCarrinhoCompras() {
            this.CarrinhoCompras = new CarrinhoCompras();
        }

        public Cliente Clone() {
            Cliente c = new Cliente(Nome,Senha,Email,Telemovel,Morada);
            c.ListaDeFavoritos = _favs;
            c.Encomendas = _encomendas;
            c.CarrinhoCompras = _carr;
            return c;
        }

    }


}