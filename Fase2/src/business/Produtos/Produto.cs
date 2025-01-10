using System.Collections;

namespace Produtos {

    public class Produto {

        public float Preco {get; set;}
        public string? Descricao {get; set;}
        public string Nome {get; set;}

        private IList<string> _procedimentos;

        //TODO: adicionar materiais

        Produto(string nome, float preco) {

            this.Nome = nome;
            this.Preco = preco;
            this.Descricao = null;
            _procedimentos = new List<string>();

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