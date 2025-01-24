using Fase2.Features.AreaPessoal;
using Produtos;

namespace business {

    public interface IFacade {

        //FIXME:
        public bool IniciarSessao(string email, string senha);
        public bool TerminarSessao();

        //TODO:
        public bool RegistarCliente(string email, string nome, string senha, string? tele, string? morada);
        public bool AlterarSenha(string email, string senha);
        public Encomenda? ObterEncomendaCliente(string email, string encomenda);
        public ISet<Produto> ObterTartes();
        public CarrinhoCompras ObterCarrinhoCompras(string email);
        public void AddProdutoCarrinhoCompras(string email, string produto);
        public void RemoveProdutoCarrinhoCompras(string email, string produto);
        public bool PagarEncomenda(string email);
        public bool CancelarEncomenda(string email);
        public ISet<Encomenda> GetEncomendasCliente(string email, Filtro? filtro);
        public void AddAvaliacao(string email, int rating, string? comentario);
        public ISet<Avaliacao> GetAvaliacoes();
        public void AddProdutoFavoritos(string email, string produto);
        public void RemoveProdutoFavoritos(string email, string produto);
        public ListaDeFavoritos GetFavoritos(string email);
        //TODO: receber notificação em caso de stock
        public void ModifyClienteNome(string email, string nome);
        //FIXME: public void ModifyClienteEmail()
        public void ModifyClienteMorada(string email, string morada);
        public void ModifyClienteTelefone(string email, string telefone);
        public ISet<FAQ> GetFAQ();
        public Encomenda? GetEncomenda(string encomenda);
        public ISet<Encomenda> GetEncomendasDoing(Filtro? filtro);
        public ISet<Encomenda> GetEncomendasDone(Filtro? filtro);
        public ISet<Encomenda> GetEncomendasQueue(Filtro? filtro);
        public ISet<Material> VerStock();
        public Relatorio? GetEncomendaRelatorio(string encomenda);
        public bool InterromperEncomendas();
        public bool RetomarEncomendas();
        //FIXME:
        public void AtualizarEstadoEncomenda(string encomenda);
        public void AtualizarProgressoEncomenda(string encomenda);
        public void SetCapMaxMaterial(string material, int capacidadeMax);
        //TODO: public void Notificar Funcionario
        public void SetMaterialQuantidade(string material, int quantidade);
        public void AddMaterialStock(string material, int quantidade_max);
        public void RemoveMaterialStock(string material);
        public void AddTarte(string id, string nome, float preco, string? imagem, string? descricao, ISet<Material> materiais, IList<string> procedimentos);
        public void RemoveTarte(string id);


    }

}