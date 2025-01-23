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


        public void AddProduto(string nome, float preco, string? descricao, ISet<Material> materiais, IList<string> procedimentos) {
            _produtos[nome] = new Produto(nome,preco,descricao,materiais,procedimentos);
        }
        public void AddProduto(string nome, float preco, string? descricao) {
            _produtos[nome] = new Produto(nome,preco,descricao);
        }
        public void RemoveProduto(string nome) {
            _produtos.Remove(nome);
        }

        public void SetProdutoPreco(string nome, float preco) {
            _produtos[nome].Preco = preco;
        }
        public void SetProdutoDescricao(string nome, string descricao) {
            _produtos[nome].Descricao = descricao;
        }
        public void RemoveProdutoDescricao(string nome) {
            _produtos[nome].Descricao = null;
        }

        public IList<string> GetProdutoProcedimentos(string nome) {
            return _produtos[nome].Procedimentos;
        }
        public string? GetProdutoProcedimento(string nome, int index) {

            IList<string> p = _produtos[nome].Procedimentos;
            return p[index];

        }
        public ISet<Material> GetProdutoMaterial(string nome) {
            return _produtos[nome].Materiais;
        }

        public ISet<Produto> GetProdutos() {
            return Produtos;
        }
        public Produto? GetProduto(string nome) {
            Produto? p = _produtos[nome];
            return p;
        }


    }

}