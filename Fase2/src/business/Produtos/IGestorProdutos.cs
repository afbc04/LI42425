namespace business {

    public interface IGestorProdutos {

        public void AddProduto(string nome, float preco, string id, string? imagem, string? descricao, ISet<Material> materiais, IList<string> procedimentos);
        public void AddProduto(string nome, float preco, string id, string? imagem, string? descricao);
        public void RemoveProduto(string id);

        public void SetProdutoPreco(string id, float preco);
        public void SetProdutoDescricao(string id, string descricao);
        public void RemoveProdutoDescricao(string id);
        public void SetProdutoNome(string id, string nome);
        public void SetProdutoImagem(string id, string imagem);
        public void RemoveProdutoImagem(string id);

        public IList<string> GetProdutoProcedimentos(string id);
        public string? GetProdutoProcedimento(string id, int index);
        public ISet<Material> GetProdutoMaterial(string id);

        public ISet<Produto> GetProdutos();
        public Produto? GetProduto(string id);
        public ISet<Material> GetMaterialCarrinhoCompras(CarrinhoCompras carrinhoCompras);
        public ISet<Produto> GetProdutosCarrinhoCompras(CarrinhoCompras carrinhoCompras);

    }


}