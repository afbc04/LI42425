namespace business {

    public interface IFacade {

        public bool SessaoIniciada();
        public string? GetEmailLogin();
        public bool isFuncionario();
        public bool IniciarSessao(string email, string senha);
        public bool TerminarSessao();

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
        public bool ConseguePagarEncomenda(string email);
        public void ModifyClienteNome(string email, string nome);
        public bool ModifyClienteEmail(string email, string novo_email);
        public void ModifyClienteMorada(string email, string morada);
        public void ModifyClienteTelefone(string email, string telefone);
        public ISet<FAQ> GetFAQ();
        public void AddFAQ(string pergunta,string resposta);
        public void RemoveFAQ(int index);
        public Encomenda? GetEncomenda(int encomenda);
        public bool FazerEncomendaProduto(int encomenda, int produto);
        public ISet<Encomenda> GetEncomendasDoing(Filtro? filtro);
        public ISet<Encomenda> GetEncomendasDone(Filtro? filtro);
        public ISet<Encomenda> GetEncomendasQueue(Filtro? filtro);
        public ISet<MaterialStock> VerStock();
        public Relatorio? GetEncomendaRelatorio(int encomenda);
        public void InterromperEncomendas();
        public void RetomarEncomendas();
        public void AtualizarEstadoEncomenda(int encomenda);
        public void AtualizarProgressoEncomenda(int encomenda, int produto);
        public void SetCapMaxMaterial(string material, int capacidadeMax);
        public ISet<MaterialStock> GetMaterialBaixoStock();
        public void SetMaterialQuantidade(string material, int quantidade);
        public void AddMaterialStock(string material, int quantidade_max);
        public void RemoveMaterialStock(string material);
        public void AddTarte(string id, string nome, float preco, string? imagem, string? descricao, ISet<Material> materiais, IList<string> procedimentos);
        public void RemoveTarte(string id);
        public bool MaterialExiste(string material);
        public Utilizador? GetUtilizador(string email);

        //UI
        public bool ProducaoInterrompida();
        //public int QuantidadeEncomendas();

    }

}   