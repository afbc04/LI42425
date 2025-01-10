

namespace Utilizadores {

    public abstract class Utilizador {

        public string? Morada {get; set;}
        public string Nome {get; set;}
        public string? Telemovel {get; set;}
        public string Senha {get; set;}
        public string Email {get; set;}

        public Utilizador(string nome, string senha, string email) {

            this.Nome = nome;
            this.Senha = senha;
            this.Email = email;

        }

        public Utilizador(string nome, string senha, string email, string morada, string telemovel) : this(nome,senha,email) {

            this.Morada = morada;
            this.Telemovel = telemovel;

        }

    }


}