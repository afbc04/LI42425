namespace business {

    public class GestorProdutos : IGestorProdutos {

        private IDictionary<string,Produto> _produtos;
        public ISet<Produto> Produtos {

            get {

                ISet<Produto> lista = new HashSet<Produto>();
                foreach (Produto p in _produtos.Values) {
                    lista.Add(p.Clone());
                }
                return lista;

            }
            private set {

                _produtos = new Dictionary<string,Produto>();
                foreach(Produto p in value) {

                    string id = p.Nome;
                    _produtos[id] = p.Clone();

                }

            }

        }

        public GestorProdutos() {
            _produtos = new Dictionary<string,Produto>();
        }

        public GestorProdutos(ISet<Produto> produtos) : this() {
            Produtos = produtos;
        }


        public void AddProduto(string nome, float preco, string id, string? imagem, string? descricao, ISet<Material> materiais, IList<string> procedimentos) {
            _produtos[id] = new Produto(nome,preco,id,imagem,descricao,materiais,procedimentos);
        }
        public void AddProduto(string nome, float preco, string id, string? imagem, string? descricao) {
            _produtos[id] = new Produto(nome,preco,id,imagem,descricao);
        }
        public void RemoveProduto(string id) {
            _produtos.Remove(id);
        }

        public void SetProdutoPreco(string id, float preco) {
            _produtos[id].Preco = preco;
        }
        public void SetProdutoDescricao(string id, string descricao) {
            _produtos[id].Descricao = descricao;
        }
        public void RemoveProdutoDescricao(string id) {
            _produtos[id].Descricao = null;
        }

        public void SetProdutoNome(string id, string nome) {
            _produtos[id].Nome = nome;
        }

        public void SetProdutoImagem(string id, string imagem) {
            _produtos[id].Imagem = imagem;
        }
        public void RemoveProdutoImagem(string id) {
            _produtos[id].Imagem = null;
        }

        public IList<string> GetProdutoProcedimentos(string id) {
            return _produtos[id].Procedimentos;
        }
        public string? GetProdutoProcedimento(string id, int index) {

            IList<string> p = _produtos[id].Procedimentos;
            return p[index];

        }
        public ISet<Material> GetProdutoMaterial(string id) {
            return _produtos[id].Materiais;
        }

        public ISet<Produto> GetProdutos() {
            return Produtos;
        }
        public Produto? GetProduto(string id) {

            if (_produtos.ContainsKey(id))
                return  _produtos[id];
            else
                return null;
        }

        public ISet<Material> GetMaterialCarrinhoCompras(CarrinhoCompras carrinhoCompras) {

            IDictionary<string,int> map = new Dictionary<string,int>();

            foreach ((string s, int i) in carrinhoCompras.Produtos) {

                Produto p = _produtos[s];

                foreach(Material m in p.Materiais) {

                    if (map.ContainsKey(m.Tipo)) {
                        map[m.Tipo] += m.Quantidade;
                    }
                    else {
                        map[m.Tipo] = m.Quantidade;
                    }

                }

            }

            ISet<Material> lista = new HashSet<Material>();
            foreach(string s in map.Keys) {

                lista.Add(new Material(s,map[s]));

            }

            return lista;

        }

        public ISet<Produto> GetProdutosCarrinhoCompras(CarrinhoCompras carrinhoCompras) {

            ISet<Produto> lista = new HashSet<Produto>();

            foreach ((string s, int i) in carrinhoCompras.Produtos) {

                Produto p = _produtos[s];
                lista.Add(p.Clone());
            }

            return lista;

        }


    }

}