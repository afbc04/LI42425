using Produtos;

namespace Utilizadores {

    public class Cliente : Utilizador {

        public ListaDeFavoritos ListaDeFavoritos {get; set;}
        //public
        //TODO: Carrinho de Compras
        //TODO: Suas Encomendas

        public Cliente(string nome, string senha, string email) : base(nome,senha,email) {
            this.ListaDeFavoritos = new();
        }

    }


}