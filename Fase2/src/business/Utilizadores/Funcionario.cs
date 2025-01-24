
namespace business {

    public class Funcionario : Utilizador {

        public Funcionario(string nome, string senha, string email) : base(nome,senha,email) {
        }

        public Funcionario Clone() {
            return new Funcionario(Nome,Senha,Email);
        }

    }


}