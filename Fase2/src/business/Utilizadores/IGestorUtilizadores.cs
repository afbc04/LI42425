namespace business {

    public interface IGestorUtilizadores {

        public bool isFuncionario(string email);
        public bool ValidarSessao(string email, string senha);
        public bool RegistarCliente(string email, string nome, string senha, string? tele, string? morada);
        public bool RegistarFuncionario(string email, string nome, string senha);
        public bool AlterarSenha(string email, string senha);
        public CarrinhoCompras ObterCarrinhoCompras(string email);
        public void AddProdutoCarrinhoCompras(string email, string produto);
        public void RemoveProdutoCarrinhoCompras(string email, string produto);
        public void AddEncomenda(string email, int encomenda);
        public void RemoverEncomenda(string email, int encomenda);
        public ISet<int> GetEncomendasCliente(string email);
        public void AddProdutoFavoritos(string email, string produto);
        public void RemoveProdutoFavoritos(string email, string produto);
        public ListaDeFavoritos GetFavoritos(string email);
        public void ModifyClienteNome(string email, string nome);
        public bool ModifyClienteEmail(string email, string novo_email);
        public void ModifyClienteMorada(string email, string morada);
        public void ModifyClienteTelefone(string email, string telefone);
        public Utilizador? GetUtilizador(string email);
        public void EsvaziaCarrinhoCompras(string email);

    }

}