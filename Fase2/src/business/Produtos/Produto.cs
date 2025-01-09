namespace Produtos {

    public class Produto {

        public float Preco {get; set;}
        public string? Descricao {get; set;}
        public string Nome {get; set;}

        //TODO: adicionar materiais

        Produto(string nome, float preco) {

            this.Nome = nome;
            this.Preco = preco;
            this.Descricao = null;

        }

        Produto(string nome, float preco, string? descricao) : this(nome,preco) {
            if (descricao is not null)
                this.Descricao = descricao;
        }

        public Produto Clone() {
            return new Produto(this.Nome,this.Preco,this.Descricao);
        }

    }

}