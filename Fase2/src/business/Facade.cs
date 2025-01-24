using Produtos;

namespace business {

    public class Facade : IFacade {

        private IFAQ _faq;
        private IGestorAvaliacao _avaliacoes;
        private IGestorProdutos _produtos;
        private IStock _stock;

        public Facade() {

            _faq = new GestorFAQ();
            _avaliacoes = new GestorAvaliacao();
            _produtos = new GestorProdutos();
            _stock = new Stock();

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
        public Encomenda? ObterEncomendaCliente(string email, string encomenda) {
            return null; //TODO:
        }
        public ISet<Produto> ObterTartes() {
            return _produtos.GetProdutos();
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
        public bool CancelarEncomenda(string email) {
            //TODO:
            return false;
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
        public Encomenda? GetEncomenda(string encomenda) {
            //TODO:
            return null;
        }
        public ISet<Encomenda> GetEncomendasDoing(Filtro? filtro) {
            //TODO:
            return null;
        }
        public ISet<Encomenda> GetEncomendasDone(Filtro? filtro) {
            //TODO:
            return null;
        }
        public ISet<Encomenda> GetEncomendasQueue(Filtro? filtro) {
            //TODO:
            return null;
        }
        public ISet<Material> VerStock() {
            //TODO:
            return null;
        }
        public Relatorio? GetEncomendaRelatorio(string encomenda) {
            //TODO:
            return null;
        }
        public bool InterromperEncomendas() {
            //TODO:
            return false;
        }
        public bool RetomarEncomendas() {
            //TODO:
            return false;
        }
        public void AtualizarEstadoEncomenda(string encomenda) {
            //TODO:
        }
        public void AtualizarProgressoEncomenda(string encomenda) {
            //TODO:
        }
        public void SetCapMaxMaterial(string material, int capacidadeMax) {
            //TODO:
        }
        public void SetMaterialQuantidade(string material, int quantidade) {
            //TODO:
        }
        public void AddMaterialStock(string material) {
            //TODO:
        }
        public void RemoveMaterialStock(string material) {
            //TODO:
        }
        public void AddTarte(string id, string nome, float preco, string? imagem, string? descricao, ISet<Material> materiais, IList<string> procedimentos) {
            _produtos.AddProduto(nome,preco,id,imagem,descricao,materiais,procedimentos);
        }
        public void RemoveTarte(string id) {
            _produtos.RemoveProduto(id);
        }
        

    }

}