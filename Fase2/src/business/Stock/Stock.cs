using System.Collections.Generic;

namespace business {

    public class Stock : IStock{

        private IDictionary<string,StockEntry> _materiais;
        public ISet<Material> Materiais {
            get {
                HashSet<Material> materiais = new HashSet<Material>();
                
                IEnumerable<KeyValuePair<string, StockEntry>> entrySet = _materiais;

                foreach (var entry in entrySet)
                    materiais.Add(new Material(entry.Key,entry.Value.Quantidade));

                return materiais;

            }
        }

        public Stock() {

            _materiais = new Dictionary<string,StockEntry>();

        }

        public ISet<Material> GetStock() {
            return this.Materiais;
        }

        public bool Contains(string material) {
            return _materiais.ContainsKey(material);
        }

        public int GetMaterialQuantidade(string material) {
            return _materiais[material].Quantidade;
        }

        public bool GetMaterialQuantidadeSuficiente(string material, int limite) {
            return _materiais[material].Quantidade >= limite;
        }

        public void AddMaterial(string material, int quantidade_maxima) {
            this._materiais.Add(material,new StockEntry(0,quantidade_maxima));
        }

        public void RemoveMaterial(string material) {
            this._materiais.Remove(material);
        }

        public void AddQuantidadeMaterial(string material, int q) {

            int quantidade = _materiais[material].Quantidade += q;
            int quantidade_max = _materiais[material].QuantidadeMaxima;

            _materiais[material].Quantidade = (quantidade > quantidade_max) ? quantidade_max : quantidade;

        }

        public void RemoveQuantidadeMaterial(string material, int q) {

            int quantidade = _materiais[material].Quantidade -= q;

            _materiais[material].Quantidade = (quantidade < 0) ? 0 : quantidade;

        }

        public void ModifyMaterialQuantidadeMaxima(string material, int max) {
            _materiais[material].QuantidadeMaxima = max;
        }

        public bool ConsegueProduzir(Produto produto) {

            ISet<Material> materiais = produto.Materiais;

            foreach(Material m in materiais) {

                if (GetMaterialQuantidadeSuficiente(m.Tipo,m.Quantidade) == false)
                    return false;

            }

            return true;

        }

        public bool ProduzirProduto(Produto produto) {

            if (ConsegueProduzir(produto) == false)
                return false;

            ISet<Material> materiais = produto.Materiais;

            foreach(Material m in materiais) {
                RemoveQuantidadeMaterial(m.Tipo,m.Quantidade);
            }

            return true;

        }

        private class StockEntry {

            public int Quantidade {get; set;}
            public int QuantidadeMaxima {get; set;}

            public StockEntry(int quantidade, int max) {

                Quantidade = quantidade;
                QuantidadeMaxima = max;

            }

        }


    }


}