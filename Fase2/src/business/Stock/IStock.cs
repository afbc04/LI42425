using System.Collections.Generic;

namespace business {

    public interface IStock {

        public ISet<MaterialStock> GetStock();
        public int GetMaterialQuantidade(string material);
        public bool GetMaterialQuantidadeSuficiente(string material, int limite);
        public void AddMaterial(string material, int quantidade_maxima);
        public void RemoveMaterial(string material);
        public void AddMaterialQuantidade(string material, int q);
        public void RemoveMaterialQuantidade(string material, int q);
        public void ModifyMaterialQuantidade(string material, int q);
        public void ModifyMaterialQuantidadeMaxima(string material, int max);
        public bool ConsegueProduzir(Produto produto);
        public bool ProduzirProduto(Produto produto);
        public bool TemMaterialSuficiente(ISet<Material> materiais);
        public ISet<MaterialStock> GetMaterialBaixoStock();
        public bool MaterialExiste(string material);

    }

}