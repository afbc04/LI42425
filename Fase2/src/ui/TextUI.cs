using System;
using System.Collections.Generic;
using System.Linq;
using business;

namespace ui {

    public class TextUI {

        private IFacade Model;

        public TextUI() {

            this.Model = new Facade();
        }

        public TextUI(IFacade model) {

            this.Model = model;
        }

        public void Run() {
            Console.WriteLine("Bem vindo ao Cantinho das Encomendas!");
            this.Home();
            Console.WriteLine("Até breve...");
        }

        private void Home() {
            TextMenu menu = new TextMenu(new string[]{
                    "Iniciar Sessão",
                    "Registar",
                    "Recuperar Palavra-Passe"
            });

            // Registar pré-condições das transições
            //menu.setPreCondition(3, () => this.model.haAlunos() && this.model.haTurmas());
            //menu.setPreCondition(4, () => this.model.haTurmasComAlunos());

            // Registar os handlers das transições
            menu.SetHandler(1, () => IniciarSessao());
            //menu.SetHandler(2, () => RegistarCliente());
            //menu.SetHandler(3, () => RecuperarPassword());
            //menu.setHandler(2, ()=>gestaoDeTurmas());
            //menu.setHandler(3, ()=>adicionarAlunoATurma());
            //menu.setHandler(4, ()=>removerAlunoDeTurma());
            //menu.setHandler(5, ()=>listarAlunosDaTurma());

            menu.RunOnce();
        }

        private void MenuFuncionario() {
            string[] opcoes = new string[]{
                    "Ver Stock",
                    "Ver Materiais escassos",
                    "Adicionar novo Material",
                    "Remover Material existente",
                    "Definir Quantidade de um Material",
                    "Definir Quantidade Máxima de um Material",
                    "---",
                    "Ver Produtos",
                    "Adicionar Produto",
                    "Remover Produtos",
                    "---",
                    "Interromper Produção",
                    "Ver uma Encomenda",
                    "Ver Encomendas em espera",
                    "Ver Encomendas a serem preparadas",
                    "Ver Encomendas feitas",
                    "---",
                    "Iniciar Produção de um Produto de uma Encomenda",
                    "Atualizar Estado de Encomendas",
                    "Atualizar Progresso de Encomendas",
                    "---",
                    "Ver FAQ",
                    "Adicionar FAQ",
                    "Remover FAQ",
                    "Terminar Sessão"
            };

            bool ProducaoInterrompida = Model.ProducaoInterrompida();
            if (ProducaoInterrompida == true) {
                opcoes[11] = "Retomar Produção";
            }

            int MaterialEscasso = Model.GetMaterialBaixoStock().Count();
            opcoes[1] = "Ver Materiais escassos (" + MaterialEscasso + ")";


            TextMenu menu = new TextMenu(opcoes);

            menu.SetPreCondition(2,() => MaterialEscasso > 0);
            menu.SetPreCondition(7, () => false);
            menu.SetPreCondition(11,() => false);
            menu.SetPreCondition(17,() => false);
            menu.SetPreCondition(21,() => false);

            // Registar os handlers das transições
            menu.SetHandler(1, () => VerStock());
            menu.SetHandler(2, () => VerMateriaisEscassos());
            menu.SetHandler(3, () => AdicionarMaterial());
            menu.SetHandler(4, () => RemoverMaterial());
            menu.SetHandler(5, () => DefinirQuantidadeMaterial());
            menu.SetHandler(6, () => DefinirQuantidadeMaximaMaterial());

            menu.SetHandler(8, () => VerProdutosFuncionario());
            menu.SetHandler(9, () => AdicionarProduto());
            menu.SetHandler(10, () => RemoverProdutos());

            if (ProducaoInterrompida == true)
                menu.SetHandler(12, () => RetomarProducao());
            else
                menu.SetHandler(12, () => InterromperProducao());

            menu.SetHandler(13, () => VerUmaEncomenda());
            menu.SetHandler(14, () => VerEncomendasQueue());
            menu.SetHandler(15, () => VerEncomendasDoing());
            menu.SetHandler(16, () => VerEncomendasDone());

            menu.SetHandler(18, () => IniciarEncomendaProduto());
            menu.SetHandler(19, () => AtualizarEstadoEncomenda());
            menu.SetHandler(20, () => AtualizarProgressoEncomenda());

            menu.SetHandler(22, () => VerFAQFuncionario());
            menu.SetHandler(23, () => AdicionarFAQ());
            menu.SetHandler(24, () => RemoverFAQ());
            menu.SetHandler(25, () => TerminarSessao());

            //TODO: Atualizar Estado / Progresso / Iniciar

            menu.RunOnce();


        }

        private void MenuCliente() {
            TextMenu menu = new TextMenu(new string[]{
                    "Iniciar Sessão",
                    "Registar",
                    "Recuperar Palavra-Passe"
            });

            // Registar pré-condições das transições
            //menu.setPreCondition(3, () => this.model.haAlunos() && this.model.haTurmas());
            //menu.setPreCondition(4, () => this.model.haTurmasComAlunos());

            // Registar os handlers das transições
            menu.SetHandler(1, () => IniciarSessao());
            //menu.SetHandler(2, () => RegistarCliente());
            //menu.SetHandler(3, () => RecuperarPassword());
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

        //Iniciar Sessão
        private void IniciarSessao() {
            
            Console.WriteLine("Indique o seu email: ");
            string? email = Console.ReadLine();
            Console.WriteLine("Indique a password:");
            string? password = Console.ReadLine();

            //Sessão Iniciada com Sucesso
            if (email is not null && password is not null && Model.IniciarSessao(email,password)) {

                Console.Write("Sessão Iniciada com sucesso - ");

                if (Model.isFuncionario()) {
                    Console.Write("Funcionário\n");
                    MenuFuncionario();
                }
                else {
                    Console.Write("Cliente\n");
                    MenuCliente();
                }

            }
            else {

                Console.WriteLine("Credênciais Inválidas");
                Home();

            }

        }

        //Terminar Sessão
        private void TerminarSessao() {
            
            Model.TerminarSessao();
            Home();

        }

        //Ver Stock
        private void VerStock() {

            ISet<MaterialStock> materiais = Model.VerStock();

            if (materiais.Count() > 0) {

                Console.WriteLine("Materiais:");

                foreach (MaterialStock m in materiais) {

                    Console.WriteLine(" -> " + m.Tipo + " (" + m.Quantidade + " / "+ m.QuantidadeMaxima + ")");

                }

                Console.WriteLine("\nPressione qualquer tecla para sair");
                Console.ReadLine();

            }
            else {

                Console.WriteLine("Não há materiais no Stock");

            }

            MenuFuncionario();

        }

        //Ver Materiais Escassos
        private void VerMateriaisEscassos() {

            ISet<MaterialStock> materiais = Model.GetMaterialBaixoStock();

            if (materiais.Count() > 0) {

                Console.WriteLine("Materiais Escassos:");

                foreach (MaterialStock m in materiais) {

                    Console.WriteLine(" -> " + m.Tipo + " (" + m.Quantidade + " / "+ m.QuantidadeMaxima + ")");

                }

                Console.WriteLine("\nPressione qualquer tecla para sair");
                Console.ReadLine();

            }
            else {

                Console.WriteLine("Não há materiais escassos no Stock");

            }

            MenuFuncionario();

        }

        //Adicionar Material
        private void AdicionarMaterial() {

            Console.WriteLine("Indique qual o Material a adicionar:");
            string? material = Console.ReadLine();

            if (material is not null) {

                if (Model.MaterialExiste(material) == false) {

                    Console.WriteLine("Indique qual a capacidade máxima para " + material);
                    string? quantidade_s = Console.ReadLine();

                    if (quantidade_s is not null) {

                    int quantidade = int.Parse(quantidade_s);

                    Model.AddMaterialStock(material,quantidade);
                    Console.WriteLine(material + " foi adicionado ao stock");

                    }
                    else {
                        Console.WriteLine("Indique um número válido");
                    }

                }
                else {
                    Console.WriteLine("Material já existe");
                }

            }
            else {
                Console.WriteLine("Indique um material válido");
            }

            MenuFuncionario();

        }


        //Remover um Material
        private void RemoverMaterial() {

            Console.WriteLine("Indique qual o Material que pretende remover:");
            string? material = Console.ReadLine();

            if (material is not null && Model.MaterialExiste(material)) {
                
                Model.RemoveMaterialStock(material);
                Console.WriteLine(material + " foi removido do stock");

            }
            else {
                Console.WriteLine("Indique um material válido");
            }

            MenuFuncionario();

        }


        //Definir a Quantidade de Material
        private void DefinirQuantidadeMaterial() {

            Console.WriteLine("Indique qual o Material a modificar a quantidade:");
            string? material = Console.ReadLine();

            if (material is not null && Model.MaterialExiste(material)) {
                Console.WriteLine("Indique qual a quantidade de " + material);
                string? quantidade_s = Console.ReadLine();

                if (quantidade_s is not null) {

                int quantidade = int.Parse(quantidade_s);

                Model.SetMaterialQuantidade(material,quantidade);
                Console.WriteLine("Quantidade de " + material + " foi modificada");

                }
                else {
                    Console.WriteLine("Indique um número válido");
                }

            }
            else {
                Console.WriteLine("Indique um material válido");
            }

            MenuFuncionario();

        }

        //Definir a quantidade máxima de material
        private void DefinirQuantidadeMaximaMaterial() {

            Console.WriteLine("Indique qual o Material a modificar a capacidade máxima:");
            string? material = Console.ReadLine();

            if (material is not null && Model.MaterialExiste(material)) {
                Console.WriteLine("Indique qual a capacidade máxima para " + material);
                string? quantidade_s = Console.ReadLine();

                if (quantidade_s is not null) {

                int quantidade = int.Parse(quantidade_s);

                Model.SetCapMaxMaterial(material,quantidade);
                Console.WriteLine("Capacidade máxima de " + material + " foi alterado");

                }
                else {
                    Console.WriteLine("Indique um número válido");
                }

            }
            else {
                Console.WriteLine("Indique um material válido");
            }

            MenuFuncionario();

        }

        //Ver Produtos
        private void VerProdutosFuncionario() {

            ISet<Produto> produtos = Model.ObterTartes();

            if (produtos.Count() > 0) {

                Console.WriteLine("Produtos:");
                int i = 1;

                foreach (Produto p in produtos) {

                    if (i>1)
                        Console.WriteLine();

                    Console.WriteLine("\n--- " + p.Id + "---\nNome: " + p.Nome + "\nPreço: " + string.Format("{0:F2}", p.Preco) + "€\nDescrição: " + p.Descricao);

                    i++;

                }

                Console.WriteLine("\nPressione qualquer tecla para sair");
                Console.ReadLine();

            }
            else {

                Console.WriteLine("Não há produtos disponíveis");

            }

            MenuFuncionario();

        }

        //Adicionar Produto
        private void AdicionarProduto() {

            Console.WriteLine("Indique qual o ID do novo Produto: ");
            string? id = Console.ReadLine();

            if (id is not null) {
                Console.WriteLine("Indique qual o nome do novo produto:");
                string? nome = Console.ReadLine();

                if (nome is not null) {
                    Console.WriteLine("Indique qual o novo preço:");
                    string? preco_s = Console.ReadLine();

                    if (preco_s is not null && float.Parse(preco_s) >= 0) {
                        float preco = float.Parse(preco_s);
                        Console.WriteLine("Indique qual a imagem do novo produto: (opcional)");
                        string? imagem = Console.ReadLine();

                        Console.WriteLine("Indique qual a descrição do novo produto: (opcional)");
                        string? descricao = Console.ReadLine();

                        Console.WriteLine("Indique quantos materiais o novo produto consome:");
                        string? materialN_s = Console.ReadLine();

                        if (materialN_s is not null) {

                            int materialN = int.Parse(materialN_s);
                            ISet<Material> materiais = new HashSet<Material>();

                            while (materialN > 0) {

                                Console.WriteLine("Indique qual o material:");
                                string? material_id = Console.ReadLine();
                                Console.WriteLine("Indique a quantidade desse material:");
                                string? material_quantidade_s = Console.ReadLine();

                                if (material_id is not null && material_quantidade_s is not null && int.Parse(material_quantidade_s) > 0) {

                                    materiais.Add(new Material(material_id,int.Parse(material_quantidade_s)));
                                    materialN--;

                                }
                                else {
                                    Console.WriteLine("Ocorreu um erro, tente novamente");
                                }

                            }

                            Console.WriteLine("Indique quantos procedimentos existem:");
                            string? procedimentosN_s = Console.ReadLine();
    
                            if (procedimentosN_s is not null) {
                            
                                int procedimentosN = int.Parse(procedimentosN_s);
                                IList<string> procedimentos = new List<string>();
                                int i = 1;

                                while (procedimentosN > 0) {
                                
                                    Console.WriteLine("Indique qual o " + i + "º procedimento:");
                                    string? procedimento = Console.ReadLine();
    
                                    if (procedimento is not null) {
                                    
                                        procedimentos.Add(procedimento);
                                        procedimentosN--;
                                        i++;
    
                                    }
                                    else {
                                        Console.WriteLine("Indique um procedimento válido");
                                    }
    
                                }

                                Model.AddTarte(id,nome,preco,imagem,descricao,materiais,procedimentos);
                                Console.WriteLine(nome + " foi adicionado");
    
                            }
                            else {
                                Console.WriteLine("Indique um número válido");
                            }

                        }
                        else {
                            Console.WriteLine("Indique um número válido");
                        }

                    }
                    else {
                        Console.WriteLine("Indique um preço válido");
                    }

                }
                else {
                    Console.WriteLine("Indique um nome válido");
                }

            }
            else {
                Console.WriteLine("Indique um ID válido");
            }

            MenuFuncionario();

        }

        //Remover Produto
        private void RemoverProdutos() {

            Console.WriteLine("Indique qual o produto a remover:");
            string? produto = Console.ReadLine();

            if (produto is not null) {

                Model.RemoveTarte(produto);
                Console.WriteLine(produto + " foi removido");

            }
            else {
                Console.WriteLine("Indique um produto válido");
            }

            MenuFuncionario();

        }

        //Retomar Produção
        private void RetomarProducao() {

            Model.RetomarEncomendas();
            Console.WriteLine("Produção foi retomada");
            MenuFuncionario();

        }

        //Interromper Produção
        private void InterromperProducao() {

            Model.InterromperEncomendas();
            Console.WriteLine("Produção foi interrompida");
            MenuFuncionario();

        }


        //Ver uma Encomenda
        private void VerUmaEncomenda() {

            Console.WriteLine("Indique qual a encomenda:");
            string? encomenda_s = Console.ReadLine();

            if (encomenda_s is not null) {

                Encomenda? e = Model.GetEncomenda(int.Parse(encomenda_s));
                if (e is not null) {

                    Console.WriteLine("Encomenda:");
                    Console.WriteLine("ID: " + e.ID);
                    Console.WriteLine("Cliente: " + e.Cliente);
                    Console.Write("Estado: ");
                    switch (e.Estado) {

                        case EncomendaEstado.DOING:
                            Console.Write("A FAZER\n");

                            Console.WriteLine("Produtos:");
                            foreach(EncomendaUnidade u in e.Produtos) {
                                
                                Console.Write(" - " + u.Produto.Nome);
                                if (u.Finalizado) {
                                    Console.Write(" (FEITO)\n");
                                }
                                else {

                                    if (u.Iniciado && u.ProcedimentoAtual is not null) {
                                        Console.Write(" (EM PRODUÇÃO) : " + u.ProcedimentoAtual + "\n");
                                    }
                                    else {
                                        Console.Write(" (EM ESPERA)");
                                    }

                                }

                            }

                        break;

                        case EncomendaEstado.QUEUE:
                            Console.Write("PENDENTE\n");
                        break;

                        case EncomendaEstado.DONE:
                            Console.Write("ENVIADO\n");
                        break;

                        case EncomendaEstado.INTERRUPTED:
                            Console.Write("INTERROMPIDO\n");
                        break;

                    }

                    Console.WriteLine("\nPressione qualquer tecla para sair");
                    Console.ReadLine();

                }
                else {
                    Console.WriteLine("Encomenda " + int.Parse(encomenda_s) + " nao existe");
                }

            }
            else {
                Console.WriteLine("Indique uma encomenda válida");
            }

            MenuFuncionario();

        }


        //Ver Encomendas em fila de espera
        private void VerEncomendasQueue() {

            ISet<Encomenda> encomendas = Model.GetEncomendasQueue(null);
            
            if (encomendas.Count() > 0) {

                Console.WriteLine("Encomendas em fila de espera:");
                foreach(Encomenda e in encomendas) {

                    Console.WriteLine(" - " + e.ID + " de " + e.Cliente);

                }

                Console.WriteLine("\nPressione qualquer tecla para sair");
                Console.ReadLine();

            }
            else {
                Console.WriteLine("Não existem encomendas em fila de espera");
            }

            MenuFuncionario();

        }


        //Ver Encomendas em produção
        private void VerEncomendasDoing() {

            ISet<Encomenda> encomendas = Model.GetEncomendasDoing(null);
            
            if (encomendas.Count() > 0) {

                Console.WriteLine("Encomendas em produção:");
                foreach(Encomenda e in encomendas) {

                    Console.WriteLine("\n\nEncomenda:");
                    Console.WriteLine("ID: " + e.ID);
                    Console.WriteLine("Cliente: " + e.Cliente);                    
                    Console.WriteLine("Produtos:");
                    foreach(EncomendaUnidade u in e.Produtos) {
                        
                        Console.Write(" - " + u.Produto.Nome);
                        if (u.Finalizado) {
                            Console.Write(" (FEITO)\n");
                        }
                        else {
                            if (u.Iniciado && u.ProcedimentoAtual is not null) {
                                Console.Write(" (EM PRODUÇÃO) : " + u.ProcedimentoAtual + "\n");
                            }
                            else {
                                Console.Write(" (EM ESPERA)");
                            }
                        }
                    }

                }

                Console.WriteLine("\nPressione qualquer tecla para sair");
                Console.ReadLine();

            }
            else {
                Console.WriteLine("Não existem encomendas em produção");
            }

            MenuFuncionario();

        }


        //Ver Encomendas feitas
        private void VerEncomendasDone() {

            ISet<Encomenda> encomendas = Model.GetEncomendasDone(null);
            
            if (encomendas.Count() > 0) {

                Console.WriteLine("Encomendas feitas:");
                foreach(Encomenda e in encomendas) {

                    Console.WriteLine(" - " + e.ID + " de " + e.Cliente);

                }

                Console.WriteLine("\nPressione qualquer tecla para sair");
                Console.ReadLine();

            }
            else {
                Console.WriteLine("Não existem encomendas feitas");
            }

            MenuFuncionario();

        }

        //Iniciar um produto de uma encomenda
        private void IniciarEncomendaProduto() {

            Console.WriteLine("Indique qual a encomenda:");
            string? encomenda_s = Console.ReadLine();

            if (encomenda_s is not null) {

                Encomenda? e = Model.GetEncomenda(int.Parse(encomenda_s));
                if (e is not null) {

                    if (e.Estado == EncomendaEstado.DOING) {

                        Console.WriteLine("Indique qual o produto a iniciar (índice)");
                        string? index_s = Console.ReadLine();

                        if (index_s is not null) {

                            int index = int.Parse(index_s);
                            if (Model.FazerEncomendaProduto(int.Parse(encomenda_s),index)) {
                                Console.WriteLine("Produção do produto foi iniciado");
                            }
                            else {
                                Console.WriteLine("Produto não conseguiu ser produzido");
                            }

                        }
                        else {
                            Console.WriteLine("Indique um índice válido");
                        }

                    }
                    else {
                        Console.WriteLine("Encomenda não está a ser feita de momento");
                    }

                }
                else {
                    Console.WriteLine("Encomenda não existe");
                }
            }
            else {
                Console.WriteLine("Indique uma encomenda válida");
            }

            MenuFuncionario();

        }

        //Atualizar Estado de uma Encomenda
        private void AtualizarEstadoEncomenda() {

            Console.WriteLine("Indique qual a encomenda:");
            string? encomenda_s = Console.ReadLine();

            if (encomenda_s is not null) {

                Encomenda? e = Model.GetEncomenda(int.Parse(encomenda_s));
                if (e is not null) {

                    Model.AtualizarEstadoEncomenda(int.Parse(encomenda_s));
                    Console.WriteLine("Estado da Encomenda foi atualizado");

                }
                else {
                    Console.WriteLine("Encomenda não existe");
                }
            }
            else {
                Console.WriteLine("Indique uma encomenda válida");
            }

            MenuFuncionario();

        }

        //Atualizar o Progresso de uma encomenda
        private void AtualizarProgressoEncomenda() {

            Console.WriteLine("Indique qual a encomenda:");
            string? encomenda_s = Console.ReadLine();

            if (encomenda_s is not null) {

                Encomenda? e = Model.GetEncomenda(int.Parse(encomenda_s));
                if (e is not null) {

                    if (e.Estado == EncomendaEstado.DOING) {

                        Console.WriteLine("Indique qual o produto a atualizar (índice)");
                        string? index_s = Console.ReadLine();

                        if (index_s is not null) {

                            int index = int.Parse(index_s);
                            Model.AtualizarProgressoEncomenda(int.Parse(encomenda_s),index);
                            Console.WriteLine("Progresso da Encomenda foi atualizado");

                        }
                        else {
                            Console.WriteLine("Indique um índice válido");
                        }

                    }
                    else {
                        Console.WriteLine("Encomenda não está a ser feita de momento");
                    }

                }
                else {
                    Console.WriteLine("Encomenda não existe");
                }
            }
            else {
                Console.WriteLine("Indique uma encomenda válida");
            }

            MenuFuncionario();

        }

        //Ver Materiais Escassos
        private void VerFAQFuncionario() {

            ISet<FAQ> FAQ = Model.GetFAQ();

            if (FAQ.Count() > 0) {
            
                Console.WriteLine("Perguntas e Respostas:");
                foreach(FAQ f in FAQ) {
                    Console.WriteLine("\n" + f.ID + ". Pergunta: " + f.Pergunta);
                    Console.WriteLine("Resposta: " + f.Resposta);
                }

                Console.WriteLine("\nPressione qualquer tecla para sair");
                Console.ReadLine();

            }
            else {
                Console.WriteLine("Não existem perguntas e respostas");
            }

            MenuFuncionario();

        }


        //Ver Materiais Escassos
        private void AdicionarFAQ() {

            Console.WriteLine("Indique qual a pergunta:");
            string? pergunta = Console.ReadLine();

            if (pergunta is not null) {

                Console.WriteLine("Indique qual a resposta da pergunta:");
                string? resposta = Console.ReadLine();

                if (resposta is not null) {

                    Model.AddFAQ(pergunta,resposta);
                    Console.WriteLine("Pergunta foi adicionada");

                }
                else {
                    Console.WriteLine("Indique uma resposta válida");
                }

            }
            else {
                Console.WriteLine("Indique uma pergunta válida");
            }

            MenuFuncionario();

        }


        //Ver Materiais Escassos
        private void RemoverFAQ() {

            Console.WriteLine("Indique qual a pergunta a remover:");
            string? pergunta_s = Console.ReadLine();

            if (pergunta_s is not null) {

                Model.RemoveFAQ(int.Parse(pergunta_s));
                Console.WriteLine("Pergunta " + int.Parse(pergunta_s) + " foi removida");

            }
            else {
                Console.WriteLine("Indique uma pergunta válida");
            }

            MenuFuncionario();

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