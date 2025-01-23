namespace business {

    public class FAQ {

        public int ID {get; set;}
        public string Pergunta {get; set;}
        public string Resposta {get; set;}

        public FAQ(int id, string pergunta, string resposta) {
            this.ID = id;
            this.Pergunta = pergunta;
            this.Resposta = resposta;
        }

        public FAQ Clone() {
            return new FAQ(this.ID,this.Pergunta,this.Resposta);
        }

    }

}