using System.Collections.Generic;

namespace Materiais {

    public interface IStock {

        public bool Contains(string material);
        public int Quantidade(string material);
        public bool QuantidadeSuficiente(string material, int limite);
        public void Add(Material material);
        public void Remove(string material);
        public void AddQuantidade(string material, int q);
        public void RemoveQuantidade(string material, int q);

    }

}