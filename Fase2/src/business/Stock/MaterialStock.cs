namespace business {

    public class MaterialStock {

            public int Quantidade {get; set;}
            public int QuantidadeMaxima {get; set;}
            public string Tipo {get; set;}

            public MaterialStock(int quantidade, int max, string tipo) {

                Quantidade = quantidade;
                QuantidadeMaxima = max;
                Tipo = tipo;

            }

            public MaterialStock Clone() {
                return new MaterialStock(Quantidade,QuantidadeMaxima,Tipo);
            }

        }

}