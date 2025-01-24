using System.Collections.Generic;

namespace business {

    public class ListaDeFavoritos {

        private ISet<string> _produtos;
        public ISet<string> Produtos {
            get {
                return new HashSet<string>(_produtos);
            }
            set {
                _produtos = new HashSet<string>(value);
            }
        }

        public ListaDeFavoritos() {

            _produtos = new HashSet<string>();

        }

        public ListaDeFavoritos(ISet<string> produtos) : this() {
            Produtos = produtos;
        }

        public void AddProduto(string id) {
            _produtos.Add(id);
        }

        public void RemoveProduto(string id) {
            _produtos.Remove(id);
        }

        public ISet<string> GetProdutos() {
            return Produtos;
        }

        public ListaDeFavoritos Clone() {
            return new ListaDeFavoritos(this._produtos);
        }

        public int Size() {
            return this.Produtos.Count();
        }

        public bool TemProduto(string id) {
            return _produtos.Contains(id);
        }

    }


}