namespace business {

    public interface IFacade {

        public bool isFuncionario(string email);
        //FIXME:
        public bool IniciarSessao(string email, string senha);
        public bool TerminarSessao();

        //TODO:
        public bool RegistarCliente(string email, string nome, string senha, string? tele, string? morada);
        public bool RegistarFuncionario(string email, string nome, string senha);
        public bool AlterarSenha(string email, string senha);
        public ISet<Produto> ObterTartes();
        public Produto? ObterTarte(string tarte);
        public CarrinhoCompras ObterCarrinhoCompras(string email);
        public void AddProdutoCarrinhoCompras(string email, string produto);
        public void RemoveProdutoCarrinhoCompras(string email, string produto);
        public bool PagarEncomenda(string email);
        public bool CancelarEncomenda(int encomenda);
        public ISet<Encomenda> GetEncomendasCliente(string email, Filtro? filtro);
        public void AddAvaliacao(string email, int rating, string? comentario);
        public ISet<Avaliacao> GetAvaliacoes();
        public void AddProdutoFavoritos(string email, string produto);
        public void RemoveProdutoFavoritos(string email, string produto);
        public ListaDeFavoritos GetFavoritos(string email);
        //TODO: receber notificação em caso de stock
        public void ModifyClienteNome(string email, string nome);
        public bool ModifyClienteEmail(string email, string novo_email);
        public void ModifyClienteMorada(string email, string morada);
        public void ModifyClienteTelefone(string email, string telefone);
        public ISet<FAQ> GetFAQ();
        public Encomenda? GetEncomenda(int encomenda);
        public ISet<Encomenda> GetEncomendasDoing(Filtro? filtro);
        public ISet<Encomenda> GetEncomendasDone(Filtro? filtro);
        public ISet<Encomenda> GetEncomendasQueue(Filtro? filtro);
        public ISet<Material> VerStock();
        public Relatorio? GetEncomendaRelatorio(int encomenda);
        public void InterromperEncomendas();
        public void RetomarEncomendas();
        //FIXME:
        public void AtualizarEstadoEncomenda(int encomenda);
        public void AtualizarProgressoEncomenda(int encomenda, int produto);
        public void SetCapMaxMaterial(string material, int capacidadeMax);
        //TODO: public void Notificar Funcionario
        public void SetMaterialQuantidade(string material, int quantidade);
        public void AddMaterialStock(string material, int quantidade_max);
        public void RemoveMaterialStock(string material);
        public void AddTarte(string id, string nome, float preco, string? imagem, string? descricao, ISet<Material> materiais, IList<string> procedimentos);
        public void RemoveTarte(string id);

    }

}