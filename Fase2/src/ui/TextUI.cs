using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using business;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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

            // Registar os handlers das transições
            menu.SetHandler(1, () => IniciarSessao());
            menu.SetHandler(2, () => RegistarCliente());
            menu.SetHandler(3, () => RecuperarSenha());

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

            menu.SetHandler(8, () => VerProdutos());
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
            menu.SetPreCondition(18, () => ProducaoInterrompida == false);
            menu.SetHandler(19, () => AtualizarEstadoEncomenda());
            menu.SetPreCondition(19, () => ProducaoInterrompida == false);
            menu.SetHandler(20, () => AtualizarProgressoEncomenda());
            menu.SetPreCondition(20, () => ProducaoInterrompida == false);

            menu.SetHandler(22, () => VerFAQ());
            menu.SetHandler(23, () => AdicionarFAQ());
            menu.SetHandler(24, () => RemoverFAQ());
            menu.SetHandler(25, () => TerminarSessao());

            //TODO: Atualizar Estado / Progresso / Iniciar

            menu.RunOnce();


        }

        private void MenuCliente() {
            string[] opcoes = new string[]{
                    "Ver Produtos",
                    "Ver Carrinho de Compras",
                    "Adicionar Produto ao Carrinho",
                    "Remover Produto do Carrinho",
                    "Pagar Carrinho",
                    "Pedir Notificação de stock",
                    "Ver Favoritos",
                    "Adicionar Produto aos Favoritos",
                    "Remover produto dos Favoritos",
                    "Ver as minhas encomendas",
                    "Cancelar uma encomenda",
                    "Ver Preparação da Encomenda",
                    "Ver Dados Pessoais",
                    "Modificar Dados Pessoais",
                    "Ver Avaliações",
                    "Fazer Avaliação",
                    "Ver FAQ",
                    "Terminar Sessão"
            };

            TextMenu menu = new TextMenu(opcoes);

            menu.SetPreCondition(6, () => false);

            // Registar os handlers das transições
            menu.SetHandler(1, () => VerProdutos());
            menu.SetHandler(2, () => VerCarrinho());
            menu.SetHandler(3, () => AdicionarAoCarrinho());
            menu.SetHandler(4, () => RemoverDoCarrinho());
            menu.SetHandler(5, () => PagarCarrinho());
            menu.SetHandler(6, () => PedirNotificacaoDeStock());
            menu.SetHandler(7, () => VerFavoritos());
            menu.SetHandler(8, () => AdicionarAosFavoritos());
            menu.SetHandler(9, () => RemoverDosFavoritos());
            menu.SetHandler(10, () => VerMinhasEncomendas());
            menu.SetHandler(11, () => CancelarEncomenda());
            menu.SetHandler(12, () => VerPreparacaoEncomenda());
            menu.SetHandler(13, () => VerDadosPessoais());
            menu.SetHandler(14, () => ModificarDadosPessoais());
            menu.SetHandler(15, () => VerAvaliacoes());
            menu.SetHandler(16, () => FazerAvaliacao());
            menu.SetHandler(17, () => VerFAQ());
            menu.SetHandler(18, () => TerminarSessao());

            menu.RunOnce();
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

        //Regista um cliente no sistema
        private void RegistarCliente() {

            Console.WriteLine("Indique qual o seu nome:");
            string? nome = Console.ReadLine();
            Console.WriteLine("Indique qual o seu email:");
            string? email = Console.ReadLine();
            Console.WriteLine("Indique qual a sua palavra-passe");
            string? senha = Console.ReadLine();
            Console.WriteLine("Indique o seu número de telemovel: (opcional)");
            string? telemovel = Console.ReadLine();
            Console.WriteLine("Indique a sua morada: (opcional)");
            string? morada = Console.ReadLine();

            if (nome is not null && email is not null && senha is not null) {

                if (Model.RegistarCliente(email,nome,senha,telemovel,morada)) {

                    Console.WriteLine("Cliente registado com sucesso!");

                    Model.IniciarSessao(email,senha);
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
                    Console.WriteLine("Cliente já existe com esse email");
                    Home();
                }

            }
            else {
                Console.WriteLine("Indique dados válido");
                Home();
            }

        }

        //Altera a senha de um email
        private void RecuperarSenha() {

            Console.WriteLine("[AVISO] Modo de demonstração!");
            Console.WriteLine("Indique qual o email que pretende alterar a senha:");
            string? email = Console.ReadLine();

            if (email is not null) {

                Console.WriteLine("Indique qual a nova palavra-passe:");
                string? password = Console.ReadLine();

                if (password is not null) {

                    if (Model.AlterarSenha(email,password))
                        Console.WriteLine("Palavra-passe do email " + email + " alterada com sucesso");
                    else
                        Console.WriteLine("Ocorreu um erro durante a mudança de palavra-passe");

                }
                else {
                    Console.WriteLine("Indique uma palavra-passe válida");
                }

            }
            else {
                Console.WriteLine("Indique um email válido");
            }

            Home();

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
        private void VerProdutos() {

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

            if(Model.isFuncionario()) {
                MenuFuncionario();
            } 
            else {
                MenuCliente();
            }

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
                        if (u.Finalizado == true) {
                            Console.Write(" (FEITO)\n");
                        }
                        else {
                            if (u.Iniciado == true) {
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
        private void VerFAQ() {

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

            if(Model.isFuncionario()) {
                MenuFuncionario();
            } else {
                MenuCliente();
            }
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

        private void VerCarrinho() {

            string? email = Model.GetEmailLogin();

            if (email != null) {
            CarrinhoCompras carrinho = Model.ObterCarrinhoCompras(email);
            ISet<(string Id, int Quantidade)> produtos = carrinho.GetProdutos();

                if (produtos.Count() > 0) {
                    
                    Console.WriteLine("Carrinho de compras:");
                    foreach (var (id, quantidade) in produtos) {
                        Produto? p = Model.ObterTarte(id);
                        if(p != null) {
                        Console.WriteLine(" - "+ p.Nome + " (preço: " + string.Format("{0:F2}", p.Preco) + "€) " + quantidade + "x");
                        } 
                        else {
                            Console.WriteLine("Produto não encontrado.");
                        }
                    }

                        // Aqui é onde você adiciona a mensagem e espera pela tecla
                    Console.WriteLine("\nPressione qualquer tecla para sair");
                    Console.ReadLine();

                }
                else  {
                    Console.WriteLine("O carrinho está vazio.");
                }

            }
            else {
                Console.WriteLine("Não foi possível obter o email do utilizador.");
            }

        MenuCliente();
        }

        private void AdicionarAoCarrinho() {

            string? email = Model.GetEmailLogin();

            if (email != null) {
                ISet<Produto> produtos = Model.ObterTartes();

                if (produtos.Count() > 0) {
                    
                    Console.WriteLine("Produtos:");
                    foreach (Produto p in produtos) {
                        //Mostrar os produtos numerados
                        Console.WriteLine(" - " + p.Id + " (" + p.Nome + " - " + string.Format("{0:F2}", p.Preco) + "€)");
                    }

                    Console.WriteLine("Insira o id do produto: ");
                    string? id = Console.ReadLine();
                    if (id != null && produtos.Any(p => p.Id == id)) { // verifica se o Id introduzido corresponde a algum Id em produtos
                        Model.AddProdutoCarrinhoCompras(email, id);
                    }
                    else {
                        Console.WriteLine("Id introduzido não é válido.");
                    }
                } else {
                    Console.WriteLine("Não há produtos disponíveis para adicionar.");
                }
            } else {
                Console.WriteLine("Email não encontrado.");
            }
            MenuCliente();
        }
        
    
        private void RemoverDoCarrinho() {

            string? email = Model.GetEmailLogin();

            if (email != null) {

                CarrinhoCompras carrinho = Model.ObterCarrinhoCompras(email);
                ISet<(string Id, int Quantidade)> produtos = carrinho.GetProdutos();

                if (produtos.Count() > 0) {
                    Console.WriteLine("Produtos no seu carrinho:");

                    foreach (var (id, quantidade) in produtos) {
                        Produto? p = Model.ObterTarte(id);
                        if (p != null) {
                            Console.WriteLine(" - " + id + " - " + p.Nome + " (Quantidade: " + quantidade + "x)");
                        }
                    }

                    Console.WriteLine("\nInsira o ID do produto que deseja remover:");
                    string? idRemocao = Console.ReadLine();

                    if (idRemocao != null && produtos.Any(p => p.Item1 == idRemocao)) {
                        Model.RemoveProdutoCarrinhoCompras(email, idRemocao);
                        Console.WriteLine("Produto removido com sucesso.");
                    } else {
                        Console.WriteLine("O Id introduzido não é válido.");
                    }

                } else {
                    Console.WriteLine("O carrinho está vazio.");
                }

            } else {
                Console.WriteLine("Email não encontrado.");
            }

            MenuCliente();
        }

        private void PagarCarrinho() {
            string? email = Model.GetEmailLogin();

            if (email != null) {
                if (Model.ConseguePagarEncomenda(email)) {
                    if (Model.PagarEncomenda(email))
                        Console.WriteLine("Encomenda foi paga com sucesso");
                    else
                        Console.WriteLine("Não é possivel concluir o pagamento");
                } else {
                    Console.WriteLine("Não é possível efetuar a sua encomenda.");
                }

            } else {
                Console.WriteLine("Email não encontrado.");
            }

            MenuCliente();
        }

        private void PedirNotificacaoDeStock() {
            Console.WriteLine("Opção não implementada.");
            MenuCliente();
        }

        private void VerFavoritos() {
            string? email = Model.GetEmailLogin();

            if (email != null) {
            ListaDeFavoritos lista = Model.GetFavoritos(email);
            ISet<string> produtos = lista.GetProdutos();

                if (produtos.Count() > 0) {
                    
                    Console.WriteLine("Favoritos:");
                    foreach (var id in produtos) {
                        Produto? p = Model.ObterTarte(id);
                        if(p != null) {
                            Console.WriteLine(" - "+ p.Nome + "  - preço: " + string.Format("{0:F2}", p.Preco) + "€");
                        } 
                        else {
                            Console.WriteLine("Produto não encontrado.");
                        }
                    }
                      // Aqui é onde você adiciona a mensagem e espera pela tecla
                    Console.WriteLine("\nPressione qualquer tecla para sair");
                    Console.ReadLine();

                } else  {
                    Console.WriteLine("Não há nenhum produto nos seus favoritos.");
                }

            }
            else {
                Console.WriteLine("Não foi possível obter o email do utilizador.");
            }

            MenuCliente();
        }

        private void AdicionarAosFavoritos() {
            string? email = Model.GetEmailLogin();

            if (email != null) {
                ISet<Produto> produtos = Model.ObterTartes();

                if (produtos.Count() > 0) {
                    
                    foreach (Produto p in produtos) {
                        //Mostrar os produtos numerados
                        Console.WriteLine(" - " + p.Id + " (" + p.Nome + " - " + string.Format("{0:F2}", p.Preco) + "€)");
                    }

                    Console.WriteLine("Insira o id do produto: ");
                    string? id = Console.ReadLine();
                    if (id != null && produtos.Any(p => p.Id == id)) { // verifica se o Id introduzido corresponde a algum Id em produtos
                        Model.AddProdutoFavoritos(email, id);
                    }
                    else {
                        Console.WriteLine("Id introduzido não é válido.");
                    }
                } else {
                    Console.WriteLine("Não há produtos disponíveis para adicionar.");
                }
            } else {
                Console.WriteLine("Email não encontrado.");
            }
            
            MenuCliente();
        }

        private void RemoverDosFavoritos() {
            string? email = Model.GetEmailLogin();

            if (email != null) {

            ListaDeFavoritos lista = Model.GetFavoritos(email);
            ISet<string> produtos = lista.GetProdutos();


                if (produtos.Count() > 0) {
                    Console.WriteLine("Produtos nos Favoritos:");

                    foreach (var id in produtos) {
                        Produto? p = Model.ObterTarte(id);
                        if (p != null) {
                            Console.WriteLine(" - " + p.Id + " (" + p.Nome + " - " + string.Format("{0:F2}", p.Preco) + "€)");
                        }
                    }

                    Console.WriteLine("\nInsira o ID do produto que deseja remover:");
                    string? idRemocao = Console.ReadLine();

                    if (idRemocao != null && produtos.Any(p => p == idRemocao)) {
                        Model.RemoveProdutoFavoritos(email, idRemocao);
                        Console.WriteLine("Produto removido dos favoritos com sucesso.");
                    } else {
                        Console.WriteLine("O Id introduzido não é válido.");
                    }

                } else {
                    Console.WriteLine("A lista de favoritos está vazia.");
                }

            } else {
                Console.WriteLine("Email não encontrado.");
            }

            MenuCliente();
        }

        private void VerMinhasEncomendas() {
            string? email = Model.GetEmailLogin();

            if (email != null) {
                ISet<Encomenda> encomendas = Model.GetEncomendasCliente(email, null);

                 if (encomendas.Count() > 0) {
                    Console.WriteLine("As suas encomendas:");

                    foreach(Encomenda e in encomendas) {
                        Console.WriteLine("\nID: " + e.ID);
                        Console.WriteLine("Cliente: " + e.Cliente);

                        switch (e.Estado) {

                            case EncomendaEstado.DOING:
                                Console.WriteLine("Estado: Em Preparação");
                            break;

                            case EncomendaEstado.DONE:
                                Console.WriteLine("Estado: Feito");
                            break;

                            case EncomendaEstado.QUEUE:
                                Console.WriteLine("Estado: Em espera");
                            break;

                            case EncomendaEstado.INTERRUPTED:
                                Console.WriteLine("Estado: Interrompido");
                            break;

                        }

                        Console.WriteLine("Produtos:");
                        foreach (EncomendaUnidade p in e.VerProdutos()) {
                            Console.WriteLine(" - Produto: " + p.Produto.Nome);
                        }
                    }

                    Console.WriteLine("\nPressione qualquer tecla para sair");
                    Console.ReadLine();
                } else {
                    Console.WriteLine("Não existem encomendas para ver.");
                }
            } else {
                Console.WriteLine("Email não encontrado.");
            }
            MenuCliente();
        }

        private void CancelarEncomenda() {
            string? email = Model.GetEmailLogin();

            if (email != null) {
                 ISet<Encomenda> encomendas = Model.GetEncomendasCliente(email, null);

                 if(encomendas.Count() > 0) {
                    Console.WriteLine("As suas encomendas:");

                    foreach (Encomenda e in encomendas) {
                        Console.WriteLine("\nID: " + e.ID);
                    }

                    Console.WriteLine("Indique o ID da encomenda que pretende cancelar:");
                    string? idEncomenda = Console.ReadLine();
                    
                    if (idEncomenda != null && int.TryParse(idEncomenda, out int idEncomendaInt)) { // converter o id para int
                        if (encomendas.Any(e => e.ID == idEncomendaInt)) {
                            Model.CancelarEncomenda(idEncomendaInt);
                            Console.WriteLine("Encomenda cancelada com sucesso.");
                        } else {
                            Console.WriteLine("ID da encomenda não encontrado.");
                        }

                    } else {
                        Console.WriteLine("ID de encomenda inválido.");
                    }

                 } else {
                    Console.WriteLine("Não existem encomendas para cancelar.");
                 }
            } else {
                Console.WriteLine("Email não encontrado.");
            }
            
            MenuCliente();
        }

        private void VerPreparacaoEncomenda() {
            string? email = Model.GetEmailLogin();
            
            if (email != null) {
                ISet<Encomenda> encomendas = Model.GetEncomendasCliente(email, null);

                if (encomendas.Count() > 0) {
                Console.WriteLine("As suas encomendas:");
                }

                foreach (Encomenda e in encomendas) {
                    Console.WriteLine("ID: " + e.ID + "- Estado: " + e.Estado);
                }
                Console.WriteLine("\nIndique o ID da encomenda que deseja ver a preparação:");
                string? encomendaStr = Console.ReadLine();
                if(encomendaStr != null) {
            
                    Encomenda? e = Model.GetEncomenda(int.Parse(encomendaStr));
                    if (e != null) {
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

                                    if (u.Iniciado) {
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

                    } else {
                        Console.WriteLine("Encomenda não existe.");
                    }
                } else {
                    Console.WriteLine("Indique uma encomenda válida");
                }
            } else {
                Console.WriteLine("Email não encontrado.");
            }

            MenuCliente();
        }

        private void VerDadosPessoais() {
            string? email = Model.GetEmailLogin();
            
            if (email != null) {
                
                Utilizador? cliente = Model.GetUtilizador(email);
                if (cliente is not null) {
                    Console.WriteLine("\nDados Pessoais:");
                    Console.WriteLine("Nome: " + cliente.Nome);
                    Console.WriteLine("Email: " + cliente.Email);
                    if (cliente.Telemovel is not null)
                        Console.WriteLine("Telefone: " + cliente.Telemovel);
                    if (cliente.Morada is not null)
                        Console.WriteLine("Morada: " + cliente.Morada);
                } else {
                    Console.WriteLine("Cliente não encontrado.");
                }
            } else {
                Console.WriteLine("Email não encontrado.");
            }

            Console.WriteLine("\nPressione qualquer tecla para voltar ao menu");
            Console.ReadKey();
            MenuCliente();
        }

        private void ModificarDadosPessoais() {
            string? email = Model.GetEmailLogin();

            if(email != null) {
            // Perguntar ao cliente o que deseja modificar
                Console.WriteLine("\nO que você gostaria de modificar?");
                Console.WriteLine("1 - Nome");
                Console.WriteLine("2 - Email");
                Console.WriteLine("3 - Telemóvel");
                Console.WriteLine("4 - Morada");
                Console.WriteLine("Digite o número da opção que deseja modificar:");
                string? opcao = Console.ReadLine();

                if (opcao != null) {
                    switch (opcao) {
                        case "1": // Modificar o nome
                            Console.WriteLine("Digite o novo nome:");
                            string? novoNome = Console.ReadLine();
                            if (novoNome != null) {
                                Model.ModifyClienteNome(email, novoNome);
                                Console.WriteLine("Nome foi alterado");
                            } else {
                                Console.WriteLine("Nome inválido.");
                            }
                        break;
                        
                        case "2":
                            Console.WriteLine("Digite o novo email:");
                            string? novoEmail = Console.ReadLine();
                            if(novoEmail != null) {
                                if(Model.ModifyClienteEmail(email, novoEmail))
                                    Console.WriteLine("Email foi alterado");
                                else
                                    Console.WriteLine("Email não foi alterado");
                            } else {
                                Console.WriteLine("Email inválido.");
                            }
                        break;

                        case "3":
                            Console.WriteLine("Digite o novo telemóvel:");
                            string? novoTelemovel = Console.ReadLine();
                            if (novoTelemovel != null) {
                                Model.ModifyClienteTelefone(email, novoTelemovel);
                                Console.WriteLine("Telemóvel foi alterado");
                            } else {
                                Console.WriteLine("Telemóvel inválido.");
                            }
                        break;

                        case "4":
                            Console.WriteLine("Digite a nova morada:");
                            string? novaMorada = Console.ReadLine();
                            if(novaMorada != null) {
                                Model.ModifyClienteMorada(email, novaMorada);
                                Console.WriteLine("Morada foi alterada");
                            } else {
                                Console.WriteLine("Morada inválida.");
                            }
                        break;

                        default:
                            Console.WriteLine("Opção inválida.");
                            break;
                    }
                }
            } else {
                Console.WriteLine("Email não encontrado.");
            }

            MenuCliente();   
        }

        private void VerAvaliacoes() {
            ISet<Avaliacao> avaliacoes = Model.GetAvaliacoes();

            if (avaliacoes.Count() > 0) {

                Console.WriteLine("Avaliações:");
                foreach (Avaliacao a in avaliacoes) {
                    Console.WriteLine("\n--------------------------");
                    Console.WriteLine("Autor: " + a.Autor);
                    Console.WriteLine("Rating: " + a.Rating);
                    if (a.Comentario is not null)
                        Console.WriteLine("Comentário: " + a.Comentario);
                }

                Console.WriteLine("\nPressione qualquer tecla para sair");
                Console.ReadLine();

            } else {
                Console.WriteLine("Não existem avaliações disponíveis.");
            }

            MenuCliente();
        }

        private void FazerAvaliacao() {
            string? email = Model.GetEmailLogin();

            if (email != null) {

                Console.WriteLine("Qual a sua avaliação de 1 a 5?");
                string? ratingStr = Console.ReadLine();

                if (int.TryParse(ratingStr, out int rating) && rating >= 1 && rating <= 5) {
                    Console.WriteLine("Escreva um comentário:");
                    string? comentario = Console.ReadLine(); 

                    Model.AddAvaliacao(email, rating, comentario); 
                    Console.WriteLine("Avaliação adicionada com sucesso!");      
                } else {
                    Console.WriteLine("Número introduzido é inválido.");
                } 
            } else {
                Console.WriteLine("Email não encontrado.");
            }

            MenuCliente();
        }

    }

}