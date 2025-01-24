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
        public void AtualizarEstadoEncomenda(int encomenda);
        public void AtualizarProgressoEncomenda(int encomenda, int produto);

    }

}