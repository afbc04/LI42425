namespace business {

    public class EncomendaUnidade {

        public Produto Produto {get; private set;}
        public bool Iniciado {get; private set;}
        public bool Finalizado {get; private set;}
        public string? ProcedimentoAtual {get; private set;}
        private int _procedimento_index;

        public EncomendaUnidade(Produto produto) {

            Produto = produto.Clone();
            Iniciado = false;
            Finalizado = false;
            ProcedimentoAtual = null;
            _procedimento_index = 0;

        }

        public void Iniciar() {

            Iniciado = true;
            AtualizarProgresso();

        }

        public void AtualizarProgresso() {

            ProcedimentoAtual = Produto.GetProcedimento(_procedimento_index);
            _procedimento_index++;

            if (ProcedimentoAtual is null) {
                Finalizado = true;
            }

        }

        public EncomendaUnidade Clone() {
            EncomendaUnidade e = new EncomendaUnidade(this.Produto);
            e.Iniciado = this.Iniciado;
            e.Finalizado = this.Finalizado;
            e.ProcedimentoAtual = this.ProcedimentoAtual;
            e._procedimento_index = this._procedimento_index;
            return e;
        }

    }


}