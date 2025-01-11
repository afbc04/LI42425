using System;
using System.Collections.Generic;

namespace ui {

    public class TextMenu {

        public interface IHandler {
            void Execute();
        }

        public interface IPreCondition {
            bool Validate();
        }

        private string Titulo;                  // Titulo do menu (opcional)
        private List<string> Opcoes;            // Lista de opções
        private List<Func<bool>> Disponivel;  // Lista de pré-condições
        private List<Action> Handlers;         // Lista de handlers

        public TextMenu() {
            this.Titulo = "Menu";
            this.Opcoes = new List<string>();
            this.Disponivel = new List<Func<bool>>();
            this.Handlers = new List<Action>();
        }

        public TextMenu(string titulo, List<string> opcoes) {
            this.Titulo = titulo;
            this.Opcoes = new List<string>(opcoes);
            this.Disponivel = new List<Func<bool>>();
            this.Handlers = new List<Action>();

            foreach (string s in opcoes) {
                Disponivel.Add(() => true);
                Handlers.Add(() => Console.WriteLine("\nATENÇÃO: Opção não implementada!"));
            }

        }

        public TextMenu(List<string> opcoes) : this("Menu", opcoes) {}
        public TextMenu(string titulo, string[] opcoes) : this(titulo, new List<string>(opcoes)) {}
        public TextMenu(string[] opcoes) : this(new List<string>(opcoes)) {}

        public void Option(string name, Func<bool> p, Action h) {
            this.Opcoes.Add(name);
            this.Disponivel.Add(p);
            this.Handlers.Add(h);
        }

        public void RunOnce() {
            int op;
            Show();
            op = ReadOption();
            // testar pré-condição
            if (op > 0 && !this.Disponivel[op-1]()) {
                Console.WriteLine("Opção indisponível!");
            } else if (op>0) {
                // executar handler
                this.Handlers[op-1]();
            }
        }

        public void Run() {
            int op;
            do {
                Show();
                op = ReadOption();
                // testar pré-condição
                if (op>0 && !this.Disponivel[op-1]()) {
                    Console.WriteLine("Opção indisponível! Tente novamente.");
                } else if (op>0) {
                    // executar handler
                    this.Handlers[op-1]();
                }
            } while (op != 0);
        }

        public void SetPreCondition(int i, Func<bool> b) {
            this.Disponivel[i-1] = b;
        }

        public void SetHandler(int i, Action h) {
            this.Handlers[i-1] = h;
        }

        private void Show() {
            Console.WriteLine($"\n *** {Titulo} *** ");
            for (int i=0; i<this.Opcoes.Count; i++) {
                Console.Write(i+1);
                Console.Write(" - ");
                Console.WriteLine(this.Disponivel[i]() ? Opcoes[i]:"---");
            }
            Console.WriteLine("0 - Sair");
        }

        private int ReadOption() {
            int op;

            Console.Write("Opção: ");
            try {
                string? line = Console.ReadLine();
                op = (line != null) ? int.Parse(line) : -1;
            }
            catch (FormatException) { // Não foi inscrito um int
                op = -1;
            }
            if (op<0 || op>this.Opcoes.Count) {
                Console.WriteLine("Opção Inválida!!!");
                op = -1;
            }
            return op;
        }
    }

}