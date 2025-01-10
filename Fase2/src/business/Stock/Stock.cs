using System.Collections.Generic;

namespace Materiais {

    public class Stock : IStock{

        private IDictionary<string,Material> _materiais;
        public ISet<Material> Materiais {
            get {
                HashSet<Material> materiais = new HashSet<Material>();
                
                foreach(Material p in _materiais.Values)
                    materiais.Add(p.Clone());

                return materiais;

            }
            set {
                if (value is null)
                    _materiais = new Dictionary<string,Material>();
                else {

                    Dictionary<string,Material> materiais = new();
                
                    foreach(Material p in value)
                        materiais[p.Tipo] = p.Clone();

                    _materiais = materiais;

                }
            }
        }

        public Stock() {

            _materiais = new Dictionary<string,Material>();

        }

        public bool Contains(string material) {
            return _materiais.ContainsKey(material);
        }

        public int Quantidade(string material) {
            return _materiais[material].Quantidade;
        }

        public bool QuantidadeSuficiente(string material, int limite) {
            return _materiais[material].Quantidade >= limite;
        }

        public void Add(Material material) {
            this._materiais.Add(material.Tipo,material.Clone());
        }

        public void Remove(string material) {
            this._materiais.Remove(material);
        }

        public void AddQuantidade(string material, int q) {
            _materiais[material].Quantidade += q;
        }

        public void RemoveQuantidade(string material, int q) {
            _materiais[material].Quantidade -= q;
        }


    }


}