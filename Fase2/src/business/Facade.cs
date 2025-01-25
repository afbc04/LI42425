namespace business {

    public class Facade : IFacade {

        private IFAQ _faq;
        private IGestorAvaliacao _avaliacoes;
        private IGestorProdutos _produtos;
        private IStock _stock;
        private IGestorEncomendas _encomendas;
        private IGestorUtilizadores _utilizadores;

        private string? email;

        public Facade() {

            _faq = new GestorFAQ();
            _avaliacoes = new GestorAvaliacao();
            _produtos = new GestorProdutos();
            _stock = new Stock();
            _encomendas = new GestorEncomendas();
            _utilizadores = new GestorUtilizadores();
            email = null;

        }

        public bool isFuncionario() {
            if (email is not null)
                return _utilizadores.isFuncionario(email);
            else
                return false;
        }

        public bool SessaoIniciada() {
            return this.email != null;
        }

        public string? GetEmailLogin() {
            return email;
        }

        public bool IniciarSessao(string email, string senha) {
            bool res = _utilizadores.ValidarSessao(email,senha);
            if (res)
                this.email = email;
            
            return res;
        }

        public bool TerminarSessao() {
            this.email = null;
            return true;
        }

        public bool RegistarCliente(string email, string nome, string senha, string? tele, string? morada) {
            return _utilizadores.RegistarCliente(email,nome,senha,tele,morada);
        }

        public bool RegistarFuncionario(string email, string nome, string senha) {
            return _utilizadores.RegistarFuncionario(email,nome,senha);
        }

        public bool AlterarSenha(string email, string senha) {
            return _utilizadores.AlterarSenha(email,senha);
        }

        public ISet<Produto> ObterTartes() {
            return _produtos.GetProdutos();
        }

        public Produto? ObterTarte(string produto) {
            return _produtos.GetProduto(produto);
        }

        public CarrinhoCompras ObterCarrinhoCompras(string email) {
            return _utilizadores.ObterCarrinhoCompras(email);
        }

        public void AddProdutoCarrinhoCompras(string email, string produto) {
            _utilizadores.AddProdutoCarrinhoCompras(email,produto);
        }

        public void RemoveProdutoCarrinhoCompras(string email, string produto) {
            _utilizadores.RemoveProdutoCarrinhoCompras(email,produto);
        }

        public bool ConseguePagarEncomenda(string email) {
            return _stock.TemMaterialSuficiente(_produtos.GetMaterialCarrinhoCompras(_utilizadores.ObterCarrinhoCompras(email)));
        }

        public bool PagarEncomenda(string email) {
            
            CarrinhoCompras carrinhoCompras = _utilizadores.ObterCarrinhoCompras(email);

            if (_stock.TemMaterialSuficiente(_produtos.GetMaterialCarrinhoCompras(carrinhoCompras))) {

                IDictionary<string,Produto> lista = new Dictionary<string,Produto>();
                foreach (Produto p in _produtos.GetProdutosCarrinhoCompras(carrinhoCompras)) {
                    lista[p.Id] = p;
                }

                _encomendas.AddEncomendaCarrinhoCompras(email,carrinhoCompras,lista);
                return true;
            }
            else {
                return false;
            }

        }

        public bool CancelarEncomenda(int encomenda) {
            return _encomendas.CancelarEncomenda(encomenda);
        }
        public ISet<Encomenda> GetEncomendasCliente(string email, Filtro? filtro) {
            
            ISet<int> encomendasCliente = _utilizadores.GetEncomendasCliente(email);
            ISet<Encomenda> lista = new HashSet<Encomenda>();

            foreach (int e in encomendasCliente) {

                Encomenda? encomenda = _encomendas.GetEncomenda(e);

                if (encomenda is not null) {

                    if (filtro is null || filtro.Filtrar(encomenda))
                        lista.Add(encomenda.Clone());

                }

            }

            return lista;

        }

        public void AddAvaliacao(string email, int rating, string? comentario) {
            _avaliacoes.AddAvaliacao(email,rating,comentario);
        }

        public ISet<Avaliacao> GetAvaliacoes() {
            return _avaliacoes.GetAvaliacoes();
        }

        public void AddProdutoFavoritos(string email, string produto) {
            _utilizadores.AddProdutoFavoritos(email,produto);
        }

        public void RemoveProdutoFavoritos(string email, string produto) {
            _utilizadores.RemoveProdutoFavoritos(email,produto);
        }

        public ListaDeFavoritos GetFavoritos(string email) {
            return _utilizadores.GetFavoritos(email);
        }

        public void ModifyClienteNome(string email, string nome) {
            _utilizadores.ModifyClienteNome(email, nome);
        }

        public bool ModifyClienteEmail(string email, string novo_email) {
            return _utilizadores.ModifyClienteEmail(email,novo_email);
        }

        public void ModifyClienteMorada(string email, string morada) {
            _utilizadores.ModifyClienteMorada(email,morada);
        }
        public void ModifyClienteTelefone(string email, string telefone) {
            _utilizadores.ModifyClienteTelefone(email,telefone);
        }
        public ISet<FAQ> GetFAQ() {
            return _faq.GetFAQ();
        }

        public void AddFAQ(string pergunta,string resposta) {
            _faq.AddFAQ(pergunta,resposta);
        }

        public void RemoveFAQ(int index) {
            _faq.RemoveFAQ(index);
        }

        public Encomenda? GetEncomenda(int encomenda) {
            return _encomendas.GetEncomenda(encomenda);
        }

        public bool FazerEncomendaProduto(int encomenda, int produto) {

            EncomendaUnidade? e = _encomendas.GetEncomendaProduto(encomenda,produto);

            if (e is not null) {

                if (_stock.ProduzirProduto(e.Produto)) {
                    _encomendas.IniciarEncomendaProduto(encomenda,produto);
                    return true;
                }
                else {
                    return false;
                }

            }
            else {
                return false;
            }

        }

        public ISet<Encomenda> GetEncomendasDoing(Filtro? filtro) {
            return _encomendas.GetEncomendasDoing(filtro);
        }
        public ISet<Encomenda> GetEncomendasDone(Filtro? filtro) {
            return _encomendas.GetEncomendasDone(filtro);
        }
        public ISet<Encomenda> GetEncomendasQueue(Filtro? filtro) {
            return _encomendas.GetEncomendasQueue(filtro);
        }
        public ISet<Material> VerStock() {
            return _stock.GetStock();
        }

        public ISet<Material> GetMaterialBaixoStock() {
            return _stock.GetMaterialBaixoStock();
        }

        public Relatorio? GetEncomendaRelatorio(int encomenda) {
            return _encomendas.GetEncomendaRelatorio(encomenda);
        }
        public void InterromperEncomendas() {
            _encomendas.InterromperEncomendas();
        }
        public void RetomarEncomendas() {
            _encomendas.RetomarEncomendas();
        }
        public void AtualizarEstadoEncomenda(int encomenda) {
            _encomendas.AtualizarEstadoEncomenda(encomenda);
        }
        public void AtualizarProgressoEncomenda(int encomenda, int produto) {
            _encomendas.AtualizarProgressoEncomenda(encomenda,produto);
        }
        public void SetCapMaxMaterial(string material, int capacidadeMax) {
            _stock.ModifyMaterialQuantidadeMaxima(material,capacidadeMax);
        }
        public void SetMaterialQuantidade(string material, int quantidade) {
            _stock.ModifyMaterialQuantidade(material,quantidade);
        }
        public void AddMaterialStock(string material, int quantidade_max) {
            _stock.AddMaterial(material,quantidade_max);
        }
        public void RemoveMaterialStock(string material) {
            _stock.RemoveMaterial(material);
        }
        public void AddTarte(string id, string nome, float preco, string? imagem, string? descricao, ISet<Material> materiais, IList<string> procedimentos) {
            _produtos.AddProduto(nome,preco,id,imagem,descricao,materiais,procedimentos);
        }
        public void RemoveTarte(string id) {
            _produtos.RemoveProduto(id);
        }
        

    }

}