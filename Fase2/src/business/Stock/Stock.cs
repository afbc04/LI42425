using System.Collections.Generic;

namespace Materiais {

    public class Stock {

        private ISet<Material> _materiais;
        public ISet<Material> Materiais {
            get {
                ISet<Material> materiais = new HashSet<Material>();
                
                foreach(Material p in _materiais)
                    materiais.Add(p.Clone());

                return materiais;

            }
            set {
                if (value is null || value is not ISet<Material>)
                    _materiais = new HashSet<Material>();
                else {

                    ISet<Material> materiais = new HashSet<Material>();
                
                    foreach(Material p in value)
                        materiais.Add(p.Clone());

                    _materiais = materiais;

                }
            }
        }

        public Stock() {

            _materiais = new HashSet<Material>();

        }

        public bool Contains(string material) {
            Material m = new Material(material,0);
            return _materiais.Contains(m);
        }

        //FIXME: corrigir


    }


}