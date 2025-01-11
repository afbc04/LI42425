using System;
using System.Collections.Generic;
using System.Linq;
using business;

namespace ui {

    public class TextUI {

        private IFacade Model;

        public TextUI() {

            //this.Model = new TurmasFacade();
            //scin = new Scanner(System.in);
        }

        public void Run() {
            Console.WriteLine("Bem vindo ao Cantinho das Encomendas!");
            this.MenuPrincipal();
            Console.WriteLine("Até breve...");
        }

        private void MenuPrincipal() {
            TextMenu menu = new TextMenu(new string[]{
                    "Iniciar Sessão",
                    "Recuperar Palavra-Passe"
            });

            // Registar pré-condições das transições
            //menu.setPreCondition(3, () => this.model.haAlunos() && this.model.haTurmas());
            //menu.setPreCondition(4, () => this.model.haTurmasComAlunos());

            // Registar os handlers das transições
            menu.SetHandler(1, () => gestaoDeAlunos());
            //menu.setHandler(2, ()=>gestaoDeTurmas());
            //menu.setHandler(3, ()=>adicionarAlunoATurma());
            //menu.setHandler(4, ()=>removerAlunoDeTurma());
            //menu.setHandler(5, ()=>listarAlunosDaTurma());

            menu.Run();
        }

        /**
         *  Estado - Gestão de Alunos
         */
        private void gestaoDeAlunos() {
            TextMenu menu = new TextMenu("Gestão de Alunos", new string[]{
                    "Adicionar Aluno",
                    "Consultar Aluno",
                    "Listar Alunos"
            });

            // Registar os handlers
            //menu.setHandler(1, ()=>adicionarAluno());
            //menu.setHandler(2, ()=>consultarAluno());
            //menu.setHandler(3, ()=>listarAlunos());

            menu.Run();
        }

        /**
         * Estado - Gestão de Turmas
         
        private void gestaoDeTurmas() {
            Menu menu = new Menu("Gestão de Turmas", new string[]{
                    "Adicionar Turma",
                    "Mudar Sala à Turma",
                    "Listar Turmas"
            });

            // Registar os handlers - utilizando referências para métodos em vez de expressões lamdas
            menu.setHandler(1, adicionarTurma);
            menu.setHandler(2, mudarSalaDeTurma);
            menu.setHandler(3, listarTurmas);

            menu.run();
        }

        /**
         *  Estado - Adicionar Aluno
         
        private void adicionarAluno() {
            try {
                Console.WriteLine("Número da novo aluno: ");
                string? num = Console.ReadLine();
                if (num != null && !this.model.existeAluno(num)) {
                    Console.WriteLine("Nome da novo aluno: ");
                    string? nome = Console.ReadLine();
                    Console.WriteLine("Email da novo aluno: ");
                    string? email = Console.ReadLine();
                    if (nome != null && email != null && num != null) {
                        this.model.adicionaAluno(new Aluno(num, nome, email));
                        Console.WriteLine("Aluno adicionado");
                    }
                } else {
                    Console.WriteLine("Esse número de aluno já existe!");
                }
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }

        /**
         *  Estado - Consultar Aluno
         
        private void consultarAluno() {
            try {
                Console.WriteLine("Número a consultar: ");
                string? num = Console.ReadLine();
                if (num != null && this.model.existeAluno(num)) {
                    Console.WriteLine(this.model.procuraAluno(num).ToString());
                } else {
                    Console.WriteLine("Esse número de aluno não existe!");
                }
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }

        /**
         *  Estado - Listar Alunos
         
        private void listarAlunos() {
            try {

                Console.WriteLine("\nAlunos:");
                foreach (Aluno a in model.getAlunos())
                    Console.WriteLine(a.ToString());
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }

        /**
         *  Estado - Adicionar Turma
         
        private void adicionarTurma() {
            try {
                Console.WriteLine("Número da turma: ");
                string? tid = Console.ReadLine();
                if (tid!=null && !this.model.existeTurma(tid)) {
                    Console.WriteLine("Sala: ");
                    string? sala = Console.ReadLine();
                    Console.WriteLine("Edifício: ");
                    string? edif = Console.ReadLine();
                    Console.WriteLine("Capacidade: ");
                    string? capS = Console.ReadLine();
                    if (sala is not null && edif is not null && capS is not null) {
                        int cap = int.Parse(capS);
                        this.model.adicionaTurma(new Turma(tid, new Sala(sala, edif, cap)));
                        Console.WriteLine("Turma adicionada");
                    }
                } else {
                    Console.WriteLine("Esse número de turma já existe!");
                }
            } catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }

        /**
         *  Estado - Mudar Sala de Turma
         
        private void mudarSalaDeTurma() {
            try {
                Console.WriteLine("Número da turma: ");
                string ?tid = Console.ReadLine();
                if (tid!= null && this.model.existeTurma(tid)) {
                    Console.WriteLine("Sala: ");
                    string? sala = Console.ReadLine();
                    Console.WriteLine("Edifício: ");
                    string? edif = Console.ReadLine();
                    Console.WriteLine("Capacidade: ");
                    string? capS = Console.ReadLine();
                    if (sala is not null && edif is not null && capS is not null) {
                        int cap = int.Parse(capS); // Limpar o buffer depois de ler o inteiro
                        this.model.alteraSalaDeTurma(tid, new Sala(sala, edif, cap));
                        Console.WriteLine("Sala da turma alterada");
                    }
                } else {
                    Console.WriteLine("Esse número de turma não existe!");
                }
            } catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }

        /**
         *  Estado - Listar Turmas
         
        private void listarTurmas() {
            try {

                Console.WriteLine("\nTurmas:");
                foreach (Turma t in model.getTurmas())
                    Console.WriteLine(t.ToString());

            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }

        /**
         *  Estado - Adicionar Aluno a Turma
         
        private void adicionarAlunoATurma() {
            try {
                Console.WriteLine("Número da turma: ");
                string? tid = Console.ReadLine();
                if (tid != null && this.model.existeTurma(tid)) {
                    Console.WriteLine("Número do aluno: ");
                    string? num = Console.ReadLine();
                    if (num is not null && this.model.existeAluno(num)) {
                        this.model.adicionaAlunoTurma(tid, num);
                        Console.WriteLine("Aluno adicionado à turma");
                    } else {
                        Console.WriteLine("Esse número de aluno não existe!");
                    }
                } else {
                    Console.WriteLine("Esse número de turma não existe!");
                }
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }

        /**
         *  Estado - Remover Aluno de Turma
         *
         *  Exemplo de menu dinâmico
         
        private void removerAlunoDeTurma() {
            try {
                Console.WriteLine("Número da turma: ");
                string? tid = Console.ReadLine();
                if (tid is not null && this.model.existeTurma(tid)) {
                    // Obter os alunos
                    List<Aluno> ca = this.model.getAlunos(tid).ToList();
                    List<string> lalunos = ca.Select(a => $"{a.Numero}-{a.Nome}").ToList();
                    // Construit o menu de alunos
                    Menu menu = new Menu("Aluno a remover...", lalunos);

                    // As opções do menu começam em 1, ms as lisats em 0!!1
                    for(int i=1; i<= lalunos.Count; i++) {
                        int idx = i;
                        menu.setHandler(i, ()=>{
                            this.model.removeAlunoTurma(tid, ca[idx-1].Numero);
                            menu.setPreCondition(idx, ()=>false);   // 'remover' aluno do menu
                        });
                    }
                    menu.run();
                } else {
                    Console.WriteLine("Esse número de turma não existe!");
                }
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }

        /**
         *  Estado - Listar Alunos da Turma
         
        private void listarAlunosDaTurma() {
            try {
                Console.WriteLine("Número da turma: ");
                string? tid = Console.ReadLine();

                if (tid is not null) {
                    foreach (Aluno a in model.getAlunos(tid)) {
                        Console.WriteLine(a.ToString());
                    }
                }

                //Console.WriteLine(this.model.getAlunos(tid).ToString());
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }*/
    }

}