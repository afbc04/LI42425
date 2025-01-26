namespace business {

    public class Encomenda {
        
        public int ID {get; private set;}

        private IList<EncomendaUnidade> _produtos;
        public IList<EncomendaUnidade> Produtos {
            set {

                _produtos = new List<EncomendaUnidade>();
                foreach (EncomendaUnidade u in value)
                    _produtos.Add(u.Clone());

            }
            get {

                IList<EncomendaUnidade> lista = new List<EncomendaUnidade>();
                foreach (EncomendaUnidade u in _produtos)
                    lista.Add(u.Clone());

                return lista;

            }
        }

        public EncomendaEstado Estado {get; private set;}
        public string Cliente {get; private set;}
        private Relatorio? _relatorio;
        public Relatorio? Relatorio {

            get {
                //TODO:
                return null;
            }

        }

        private int ProdutosPorFazer;

        public Encomenda(int id, IList<Produto> produtos, string cliente) : this(id,cliente) {

            foreach (Produto p in produtos) {
                _produtos.Add(new EncomendaUnidade(p));
            }

        }

        public Encomenda(int id, string cliente) {

            ID = id;
            _produtos = new List<EncomendaUnidade>();
            ProdutosPorFazer = _produtos.Count;
            Estado = EncomendaEstado.QUEUE;
            Cliente = cliente;
            _relatorio = null;

        }

        public void AtualizarEstadoEncomenda(EncomendaEstado estado) {
            Estado = estado;
        }

        public void AtualizarEstadoEncomenda() {

            if (Estado == EncomendaEstado.INTERRUPTED || Estado == EncomendaEstado.DONE)
                return;

            if (Estado == EncomendaEstado.DOING)
                Estado = EncomendaEstado.DONE;

            if (Estado == EncomendaEstado.QUEUE)
                Estado = EncomendaEstado.DOING;

        }

        public void AddRelatorio(Relatorio r) {
            _relatorio = r.Clone();
        }

        public IList<EncomendaUnidade> VerProdutos() {
            return Produtos;
        }

        public IList<EncomendaUnidade> VerProdutosPorFazer() {
            return Produtos.Where(u => u.Finalizado == false).ToList();
        }

        public EncomendaUnidade? VerProduto(int index) {
            
            if (index < 0 || index >= _produtos.Count) 
                return null;
                
            return _produtos[index];

        }

        public void AtualizarProgressoEncomenda(int index) {

            EncomendaUnidade u = _produtos[index];

            if (u.Finalizado == false && u.Iniciado == true) {

                u.AtualizarProgresso();
                string? novo_progresso = u.ProcedimentoAtual;

                if (novo_progresso is null) {
                    ProdutosPorFazer--;

                    if (ProdutosPorFazer == 0)
                        Estado = EncomendaEstado.DONE;

                }

            }
            
        }

        public void IniciarProduto(int index) {
            _produtos[index].Iniciar();
        }

        public Encomenda Clone() {

            Encomenda e = new Encomenda(this.ID,this.Cliente);
            e.Estado = Estado;
            e.ProdutosPorFazer = ProdutosPorFazer;

            if (Relatorio is not null)
                e._relatorio = Relatorio.Clone();

            foreach(EncomendaUnidade u in _produtos)
                e._produtos.Add(u.Clone());

            return e;

        }

    }

}