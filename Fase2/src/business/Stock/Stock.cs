using System.Collections.Generic;

namespace business {

    public class Stock : IStock{

        private IDictionary<string,MaterialStock> _materiais;
        public ISet<MaterialStock> Materiais {
            get {
                HashSet<MaterialStock> materiais = new HashSet<MaterialStock>();
                
                foreach (MaterialStock m in _materiais.Values)
                    materiais.Add(m.Clone());

                return materiais;

            }
        }

        public Stock() {

            _materiais = new Dictionary<string,MaterialStock>();

        }

        public ISet<MaterialStock> GetStock() {
            return this.Materiais;
        }

        public int GetMaterialQuantidade(string material) {
            return _materiais[material.ToUpperInvariant()].Quantidade;
        }

        public bool GetMaterialQuantidadeSuficiente(string material, int limite) {
            return _materiais[material.ToUpperInvariant()].Quantidade >= limite;
        }

        public void AddMaterial(string material, int quantidade_maxima) {
            _materiais[material.ToUpperInvariant()] = new MaterialStock(0,quantidade_maxima,material.ToUpperInvariant());
        }

        public void RemoveMaterial(string material) {
            this._materiais.Remove(material.ToUpperInvariant());
        }

        public void AddMaterialQuantidade(string material, int q) {

            int quantidade = _materiais[material.ToUpperInvariant()].Quantidade += q;
            int quantidade_max = _materiais[material.ToUpperInvariant()].QuantidadeMaxima;

            _materiais[material.ToUpperInvariant()].Quantidade = (quantidade > quantidade_max) ? quantidade_max : quantidade;

        }

        public void RemoveMaterialQuantidade(string material, int q) {

            int quantidade = _materiais[material.ToUpperInvariant()].Quantidade -= q;

            _materiais[material.ToUpperInvariant()].Quantidade = (quantidade < 0) ? 0 : quantidade;

        }

        public void ModifyMaterialQuantidade(string material, int q) {

            if (q < 0)
                q = 0;
            if (q > _materiais[material.ToUpperInvariant()].QuantidadeMaxima)
                q = _materiais[material.ToUpperInvariant()].QuantidadeMaxima;

            _materiais[material.ToUpperInvariant()].Quantidade = q;

        }

        public void ModifyMaterialQuantidadeMaxima(string material, int max) {
            _materiais[material.ToUpperInvariant()].QuantidadeMaxima = max;
        }

        public bool ConsegueProduzir(Produto produto) {

            ISet<Material> materiais = produto.Materiais;

            foreach(Material m in materiais) {

                if (GetMaterialQuantidadeSuficiente(m.Tipo,m.Quantidade) == false)
                    return false;

            }

            return true;

        }

        public bool TemMaterialSuficiente(ISet<Material> materiais) {

            foreach (Material m in materiais) {

                if (m.Quantidade > _materiais[m.Tipo].Quantidade)
                    return false;

            }

            return true;

        }

        public bool ProduzirProduto(Produto produto) {

            if (ConsegueProduzir(produto) == false)
                return false;

            ISet<Material> materiais = produto.Materiais;

            foreach(Material m in materiais) {
                RemoveMaterialQuantidade(m.Tipo,m.Quantidade);
            }

            return true;

        }

        public ISet<MaterialStock> GetMaterialBaixoStock() {

            ISet<MaterialStock> lista = new HashSet<MaterialStock>();

            foreach (string s in _materiais.Keys) {

                MaterialStock e = _materiais[s];
                float perc = (float) e.Quantidade / (float) e.QuantidadeMaxima;

                if (perc <= 0.1) {
                    lista.Add(e.Clone());
                }

            }

            return lista;

        }

        public bool MaterialExiste(string material) {
            return _materiais.ContainsKey(material.ToUpperInvariant());
        }

    }


}