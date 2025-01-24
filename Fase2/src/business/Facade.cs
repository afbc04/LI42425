using Produtos;

namespace business {

    public class Facade : IFacade {

        private IFAQ _faq;
        private IGestorAvaliacao _avaliacoes;
        private IGestorProdutos _produtos;
        private IStock _stock;
        private IGestorEncomendas _encomendas;

        public Facade() {

            _faq = new GestorFAQ();
            _avaliacoes = new GestorAvaliacao();
            _produtos = new GestorProdutos();
            _stock = new Stock();
            _encomendas = new GestorEncomendas();

        }


        //FIXME:
        public bool IniciarSessao(string email, string senha) {
            return false;
        }
        //FIXME:
        public bool TerminarSessao() {
            return false;
        }

        public bool RegistarCliente(string email, string nome, string senha, string? tele, string? morada) {
            return false; //TODO:
        }
        public bool AlterarSenha(string email, string senha) {
            return false; //TODO:
        }
        public Encomenda? ObterEncomendaCliente(string email, int encomenda) {
            return null; //TODO:
        }
        public ISet<Produto> ObterTartes() {
            return _produtos.GetProdutos();
        }

        public Produto? ObterTarte(string produto) {
            return _produtos.GetProduto(produto);
        }
        public CarrinhoCompras ObterCarrinhoCompras(string email) {
            return null; //TODO:
        }
        public void AddProdutoCarrinhoCompras(string email, string produto) {
            //TODO:
        }
        public void RemoveProdutoCarrinhoCompras(string email, string produto) {
            //TODO:
        }
        public bool PagarEncomenda(string email) {
            //TODO:
            return false;
        }
        public bool CancelarEncomenda(int encomenda) {
            return _encomendas.CancelarEncomenda(encomenda);
        }
        public ISet<Encomenda> GetEncomendasCliente(string email, Filtro? filtro) {
            //TODO:
            return null;
        }
        public void AddAvaliacao(string email, int rating, string? comentario) {
            _avaliacoes.AddAvaliacao(email,rating,comentario);
        }
        public ISet<Avaliacao> GetAvaliacoes() {
            return _avaliacoes.GetAvaliacoes();
        }
        public void AddProdutoFavoritos(string email, string produto) {
            //TODO:
        }
        public void RemoveProdutoFavoritos(string email, string produto) {
            //TODO:
        }
        public ListaDeFavoritos GetFavoritos(string email) {
            //TODO:
            return null;
        }
        public void ModifyClienteNome(string email, string nome) {
            //TODO:
        }
        public void ModifyClienteMorada(string email, string morada) {
            //TODO:
        }
        public void ModifyClienteTelefone(string email, string telefone) {
            //TODO:
        }
        public ISet<FAQ> GetFAQ() {
            return _faq.GetFAQ();
        }
        public Encomenda? GetEncomenda(int encomenda) {
            return _encomendas.GetEncomenda(encomenda);
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