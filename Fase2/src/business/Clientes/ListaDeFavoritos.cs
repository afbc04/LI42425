using System.Collections.Generic;
using business;

namespace Produtos {

    public class ListaDeFavoritos {

        private ISet<Produto> _produtos;
        public ISet<Produto> Produtos {
            get {
                ISet<Produto> produtos = new HashSet<Produto>();
                
                foreach(Produto p in _produtos)
                    produtos.Add(p.Clone());

                return produtos;

            }
            set {
                if (value is null || value is not ISet<Produto>)
                    _produtos = new HashSet<Produto>();
                else {

                    ISet<Produto> produtos = new HashSet<Produto>();
                
                    foreach(Produto p in value)
                        produtos.Add(p.Clone());

                    _produtos = produtos;

                }
            }
        }

        public ListaDeFavoritos() {

            _produtos = new HashSet<Produto>();

        }

        public int Size() {
            return this.Produtos.Count();
        }

        public bool Contains(Produto p) {
            return _produtos.Contains(p);
        }

    }


}