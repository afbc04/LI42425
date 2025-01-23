using System.Collections;

namespace business {

    public class Produto {

        public float Preco {get; set;}
        public string? Descricao {get; set;}
        public string Nome {get; set;}

        private IList<string> _procedimentos;
        public IList<string> Procedimentos {

            get {

                IList<string> lista = new List<string>();
                foreach (string s in _procedimentos)
                    lista.Add(s);

                return lista;

            }

            private set {

                _procedimentos = new List<string>();
                foreach (string s in value)
                    _procedimentos.Add(s);

            }

        }

        private ISet<Material> _materiais;
        public ISet<Material> Materiais {

            get {
                ISet<Material> lista = new HashSet<Material>();
                foreach (Material m in _materiais)
                {
                    lista.Add(m.Clone());
                }

                return lista;
            }

            private set {
                _materiais = new HashSet<Material>();
                foreach (Material m in value)
                {
                    _materiais.Add(m.Clone());
                }
            }

        }


        public Produto(string nome, float preco) {

            this.Nome = nome;
            this.Preco = preco;
            this.Descricao = null;
            _procedimentos = new List<string>();
            _materiais = new HashSet<Material>();

        }

        public Produto(string nome, float preco, string? descricao) : this(nome,preco) {
            if (descricao is not null)
                this.Descricao = descricao;
        }

        public Produto(string nome, float preco, string? descricao, ISet<Material> materiais, IList<string> procedimentos) : this(nome,preco,descricao) {

            Materiais = materiais;
            Procedimentos = procedimentos;

        }

        public void AddMaterial(Material m) {
            _materiais.Add(m);
        }

        public void RemoveMaterial(string material) {
            _materiais.Remove(new Material(material,0));
        }

        public Produto Clone() {
            return new Produto(this.Nome,this.Preco,this.Descricao,this.Materiais,this.Procedimentos);
        }

    }

}