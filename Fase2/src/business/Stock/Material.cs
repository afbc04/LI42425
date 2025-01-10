namespace Materiais {

    public class Material {

        public string Tipo {get; set;}
        private int _quantidade;
        public int Quantidade {
            get {
            return _quantidade;
            } 
            set {

            if (value <= 0)
                _quantidade = 0;
            else
                Quantidade = value;
                
            }
        }

        public Material(string tipo, int quantidade) {
            Tipo = tipo;
            Quantidade = quantidade;
        }

        public override bool Equals(object? obj) {

            if (obj == null || GetType() != obj.GetType()) {
                return false;
            }

            Material m = (Material) obj;
            return this.Tipo.Equals(m.Tipo);
        }
        
        public override int GetHashCode() {
            return this.Tipo.GetHashCode();
        }

        public Material Clone() {
            return new Material(this.Tipo,this.Quantidade);
        }

    }


}