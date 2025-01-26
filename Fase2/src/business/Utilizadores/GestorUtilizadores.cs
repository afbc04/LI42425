namespace business {

    public class GestorUtilizadores : IGestorUtilizadores {

        private IDictionary<string,Utilizador> _utilizadores;
        public ISet<Utilizador> Utilizadores {

            get {

                ISet<Utilizador> lista = new HashSet<Utilizador>();

                foreach(Utilizador u in _utilizadores.Values) {
                    
                    if (u is Funcionario)
                        lista.Add(((Funcionario) u).Clone());
                    if (u is Cliente)
                        lista.Add(((Cliente) u).Clone());

                }

                return lista;

            }
            set {

                _utilizadores = new Dictionary<string,Utilizador>();

                foreach (Utilizador u in value) {

                    if (u is Funcionario)
                        _utilizadores[u.Email] = ((Funcionario) u).Clone();
                    if (u is Cliente)
                        _utilizadores[u.Email] = ((Cliente) u).Clone();

                }

            }

        } 

        public GestorUtilizadores() {
            _utilizadores = new Dictionary<string,Utilizador>();
        }

        public bool isFuncionario(string email) {

            return _utilizadores[email] is Funcionario;

        }

        public bool ValidarSessao(string email, string senha) {

            if (_utilizadores.ContainsKey(email) == false)
                return false;

            return _utilizadores[email].Senha == senha;

        }

        public bool RegistarCliente(string email, string nome, string senha, string? tele, string? morada) {
            
            if (_utilizadores.ContainsKey(email) == true)
                return false;

            _utilizadores[email] = new Cliente(nome,senha,email,tele,morada);
            return true;

        }

        public bool RegistarFuncionario(string email, string nome, string senha) {

            if (_utilizadores.ContainsKey(email) == true)
                return false;

            _utilizadores[email] = new Funcionario(nome,senha,email);
            return true;

        }

        public bool AlterarSenha(string email, string senha) {
            _utilizadores[email].Senha = senha;
            return true;
        }

        public CarrinhoCompras ObterCarrinhoCompras(string email) {
            return ((Cliente) _utilizadores[email]).GetCarrinhoCompras();
        }

        public void AddProdutoCarrinhoCompras(string email, string produto) {
            ((Cliente) _utilizadores[email]).AddProduto(produto);
        }

        public void RemoveProdutoCarrinhoCompras(string email, string produto) {
            ((Cliente) _utilizadores[email]).RemoveProduto(produto);
        }

        public void AddEncomenda(string email, int encomenda) {
            ((Cliente) _utilizadores[email]).AddEncomenda(encomenda);
        }

        public void RemoverEncomenda(string email, int encomenda) {
            ((Cliente) _utilizadores[email]).RemoveEncomenda(encomenda);
        }

        public ISet<int> GetEncomendasCliente(string email) {
            return ((Cliente) _utilizadores[email]).GetEncomendas();
        }

        public void AddProdutoFavoritos(string email, string produto) {
            ((Cliente) _utilizadores[email]).AddFavorito(produto);
        }

        public void RemoveProdutoFavoritos(string email, string produto) {
            ((Cliente) _utilizadores[email]).RemoveFavorito(produto);
        }

        public ListaDeFavoritos GetFavoritos(string email) {
            return ((Cliente) _utilizadores[email]).GetListaFavoritos();
        }

        public void ModifyClienteNome(string email, string nome) {
            _utilizadores[email].Nome = nome;
        }

        public bool ModifyClienteEmail(string email, string novo_email) {

            if (_utilizadores.ContainsKey(novo_email))
                return false;

            _utilizadores[novo_email] = _utilizadores[email];
            _utilizadores[novo_email].Email = novo_email;
            _utilizadores.Remove(email);

            return true;

        }

        public void ModifyClienteMorada(string email, string morada) {
            _utilizadores[email].Morada = morada;
        }

        public void ModifyClienteTelefone(string email, string telefone) {
            _utilizadores[email].Telemovel = telefone;
        }

        public Utilizador? GetUtilizador(string email) {

            if (_utilizadores.ContainsKey(email)) {
                Utilizador u = _utilizadores[email];
                if (u is Cliente)
                    return ((Cliente) u).Clone();
                if (u is Funcionario)
                    return ((Funcionario) u).Clone();
            }
                
            return null;

        }

        public void EsvaziaCarrinhoCompras(string email) {

            if (_utilizadores.ContainsKey(email)) {

                Utilizador u = _utilizadores[email];
                if (u is Cliente c)
                    c.EsvaziarCarrinhoCompras();

            }

        }

    }

}