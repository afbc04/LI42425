namespace business {

    public interface IGestorEncomendas {

        public bool CancelarEncomenda(int id);
        public Encomenda? GetEncomenda(int id);
        public ISet<Encomenda> GetEncomendasDoing(Filtro? filtro);
        public ISet<Encomenda> GetEncomendasDone(Filtro? filtro);
        public ISet<Encomenda> GetEncomendasQueue(Filtro? filtro);
        public Relatorio? GetEncomendaRelatorio(int encomenda);
        public void InterromperEncomendas();
        public void RetomarEncomendas();
        public EncomendaUnidade? GetEncomendaProduto(int encomenda,int produto);
        public void IniciarEncomendaProduto(int encomenda,int produto);
        public void AtualizarEstadoEncomenda(int encomenda);
        public void AtualizarProgressoEncomenda(int encomenda, int produto);
        public void AddEncomendaCarrinhoCompras(string cliente, CarrinhoCompras carrinhoCompras, IDictionary<string,Produto> produtos);

        public bool ProducaoInterrompida();

    }

}