using System.Collections.Generic;

namespace business {

    public class CarrinhoCompras {

        private IDictionary<string,int> _produtos;
        public ISet<(string,int)> Produtos {
            get {
                ISet<(string,int)> produtos = new HashSet<(string,int)>();
                
                foreach(string s in _produtos.Keys) {

                    produtos.Add((s,_produtos[s]));

                }

                return produtos;

            }
            set {
                if (value is null)
                    _produtos = new Dictionary<string,int>();
                else {

                    _produtos = new Dictionary<string,int>();
                
                    foreach((string s,int i) in value) {

                        _produtos[s] = i;

                    }

                }
            }
        }

        public CarrinhoCompras() {

            _produtos = new Dictionary<string,int>();

        }

        public CarrinhoCompras(ISet<(string,int)> produtos) : this() {
            Produtos = produtos;
        }

        public void AddProduto(string p) {
            
            if (_produtos.ContainsKey(p)) {
                _produtos[p]++;
            }
            else {
                _produtos[p] = 1;
            }

        }

        public void RemoveProduto(string p) {
        
            int quantidade = _produtos[p];

            if (quantidade <= 1)
                _produtos.Remove(p);
            else
                _produtos[p]--;

        }

        public int TemProduto(string p) {
            
            if (_produtos.ContainsKey(p))
                return _produtos[p];
            else
                return 0;

        }

        public ISet<(string,int)> GetProdutos() {
            return Produtos;
        }

        public CarrinhoCompras Clone() {
            return new CarrinhoCompras(this.Produtos);
        }

        public int Size() {
            return this.Produtos.Count();
        }

    }


}