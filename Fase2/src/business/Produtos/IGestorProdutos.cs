namespace business {

    public interface IGestorProdutos {

        public void AddProduto(string nome, float preco, string? descricao, ISet<Material> materiais, IList<string> procedimentos);
        public void AddProduto(string nome, float preco, string? descricao);
        public void RemoveProduto(string nome);

        public void SetProdutoPreco(string nome, float preco);
        public void SetProdutoDescricao(string nome, string descricao);
        public void RemoveProdutoDescricao(string nome);

        public IList<string> GetProdutoProcedimentos(string nome);
        public string? GetProdutoProcedimento(string nome, int index);
        public ISet<Material> GetProdutoMaterial(string nome);

        public ISet<Produto> GetProdutos();
        public Produto? GetProduto(string nome);
    }


}