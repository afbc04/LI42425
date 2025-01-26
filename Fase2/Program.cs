using Fase2;
using ui;
using business;


namespace CantinhoDasEncomendas
{
    
    public class CantinhoDasEncomendas {

        public static void Main(string[] args) {

            /*
                Cantinho Das Encomendas -- versão texto
            */
            if (args.Count() > 0 && args[0] == "txt") {
                CantinhoDasEncomendasTxt();
                return;
            }

            var builder = WebApplication.CreateBuilder(args);

            //� singleton porque deve ser unico para toda a aplica��o de modo a evitar inconsist�ncia de dados
            // Registra o GestorProdutos como um servi�o Singleton
            builder.Services.AddSingleton<IFacade, Facade>(sp => PovoarFacade());

            //builder.Services.AddSingleton(sp => new EmailService(
            //    smtpServer: "smtp.gmail.com",
            //    port: 587,
            //    senderEmail: "seuemail@gmail.com",
            //    senderPassword: "suasenha"
            //));

            Console.WriteLine("TESTE");

            //builder.Services.AddSingleton<RecoveryCodeService>();

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error", createScopeForErrors: true);
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseHttpsRedirection();

            app.UseRouting(); // Adicionar routing aqui se estiver faltando
            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();

        }
        
        //Método teste que povoa o Facade
        private static Facade PovoarFacade() {

                Facade model = new Facade();

                //Adicionar Material
                model.AddMaterialStock("Ovo",60);
                model.SetMaterialQuantidade("Ovo",47);

                model.AddMaterialStock("Farinha",1000);
                model.SetMaterialQuantidade("Farinha",760);

                model.AddMaterialStock("Maçã",30);
                model.SetMaterialQuantidade("Maçã",17);

                model.AddMaterialStock("Leite",1000);
                model.SetMaterialQuantidade("Leite",789);

                model.AddMaterialStock("Açucar",600);
                model.SetMaterialQuantidade("Açucar",567);

                model.AddMaterialStock("Rosas",30);
                model.SetMaterialQuantidade("Rosas",4);

                model.AddMaterialStock("Mirtilo",30);
                model.SetMaterialQuantidade("Mirtilo",23);

                model.AddMaterialStock("Framboesa",30);
                model.SetMaterialQuantidade("Framboesa",18);

                model.AddMaterialStock("Chocolate",40);
                model.SetMaterialQuantidade("Chocolate",29);

                model.AddMaterialStock("Mandala",30);
                model.SetMaterialQuantidade("Mandala",9);

                //Adicionar Produtos
                IList<string> procedimentos = new List<string>();
                ISet<Material> materiais = new HashSet<Material>();

                    //Maça
                    procedimentos.Clear();
                    procedimentos.Add("Adicionar ovos");
                    procedimentos.Add("Misturar Farinha");
                    procedimentos.Add("Adicionar maçã");

                    materiais.Clear();
                    materiais.Add(new Material("Ovo",3));
                    materiais.Add(new Material("Farinha",300));
                    materiais.Add(new Material("Maçã",1));
                    model.AddTarte("maca", "Tarte de Maçã", 8.00f, "images/tarte_maca.png", "Uma deliciosa tarte de maçã com um toque de canela e crocância que faz recordar o Outono, a estação favorita da criadora destas receitas maravilhosas. Cada fatia é um abraço de conforto. É o ideal para acompanhar uma chávena de chá num dia frio.", materiais, procedimentos);

                    //Rosas
                    procedimentos.Clear();
                    procedimentos.Add("Adicionar ovos");
                    procedimentos.Add("Misturar Farinha");
                    procedimentos.Add("Adicionar Rosa");

                    materiais.Clear();
                    materiais.Add(new Material("Ovo",4));
                    materiais.Add(new Material("Farinha",400));
                    materiais.Add(new Material("Rosas",3));
                    model.AddTarte("rosas", "Tarte Rosas", 7.00f, "images/tarte_rosas.png", "Uma refrescante tarte de limão. Em baixo do topo macio em formato de rosas, encontra-se um recheio suave e cremoso. É a sobremesa ideal para destacar numa mesa.", materiais, procedimentos);

                    //Mirtilo
                    procedimentos.Clear();
                    procedimentos.Add("Adicionar ovos");
                    procedimentos.Add("Misturar Farinha");
                    procedimentos.Add("Adicionar maçã");

                    materiais.Clear();
                    materiais.Add(new Material("Ovo",2));
                    materiais.Add(new Material("Farinha",300));
                    materiais.Add(new Material("Mirtilo",5));
                    model.AddTarte("mirtilo", "Tarte de Mirtilo", 8.50f, "images/tarte_mirtilo.png", "Cada fatia contêm a combinação perfeita de doçura e acidez. O ideal para completar qualquer refeição.", materiais, procedimentos);


                    //Framboesa
                    procedimentos.Clear();
                    procedimentos.Add("Adicionar ovos");
                    procedimentos.Add("Misturar Farinha");
                    procedimentos.Add("Adicionar framboesas");

                    materiais.Clear();
                    materiais.Add(new Material("Ovo",3));
                    materiais.Add(new Material("Farinha",250));
                    materiais.Add(new Material("Framboesa",7));
                    model.AddTarte("framboesa", "Tarte de Framboesa", 8.00f, "images/tarte_framboesa.png", "Essencialmente feita de framboesa, é uma tarte onde cada fatia traz uma explosão deliciosa de sabores. É ainda melhor quando acompanhada com uma bola de gelado.", materiais, procedimentos);

                    //Chocolate
                    procedimentos.Clear();
                    procedimentos.Add("Adicionar ovos");
                    procedimentos.Add("Misturar Farinha");
                    procedimentos.Add("Adicionar Leite");
                    procedimentos.Add("Adicionar chocolate");

                    materiais.Clear();
                    materiais.Add(new Material("Ovo",6));
                    materiais.Add(new Material("Farinha",500));
                    materiais.Add(new Material("Leite",500));
                    materiais.Add(new Material("Chocolate",50));
                    model.AddTarte("chocolate", "Tarte de Chocolate", 9.00f, "images/tarte_chocolate.png", "A tarte de chocolate é sempre um prazer irresistível. O suave e cremoso creme de chocolate enriquecido com uma pitada de flor de sal é a combinação perfeita para ficar rendido a esta tarte. Os frutos secos completam estes sabores, ao trazerem toques de crocância.", materiais, procedimentos);

                    //Mandala
                    procedimentos.Clear();
                    procedimentos.Add("Adicionar leite");
                    procedimentos.Add("Adicionar mandala");

                    materiais.Clear();
                    materiais.Add(new Material("Leite",300));
                    materiais.Add(new Material("Mandala",1));
                    model.AddTarte("mandala", "Tarte Folhas", 8.00f, "images/tarte_mandala.png", "Uma bonita e saborosa tarte com uns bordados em forma de folha. A perfeita combinação de sabores no recheio e o bonito desenho no topo fazem desta tarte única e especial.", materiais, procedimentos);
                
                //FAQ
                model.AddFAQ("Como faço para criar uma conta?","Para criar uma conta, clique em \"Área Pessoal\" no menu e depois selecione \"Criar Conta\". Preencha os dados solicitados e finalize o seu registo.");
                model.AddFAQ("Como posso ver os meus favoritos?","No menu horizontal, no topo da página, clique em \"Favoritos\"");
                model.AddFAQ("Como posso modificar meus dados pessoais?","Após fazer login na sua conta, acesse a opção \"Dados Pessoais\" na sua área pessoal. Lá, poderá editar os seus dados.");
                model.AddFAQ("O que devo fazer se uma tarte estiver sem stock?","Pode pedir para receber um email, avisando que a tarte está novamente disponível.");

                //Utilizadores
                model.RegistarFuncionario("mario@gmail.com","Mário","1234");
                model.RegistarCliente("p1@gmail.com","Pessoa 1","1234",null,null);
                model.RegistarCliente("p2@gmail.com","Pessoa 2","1234","911837273","Rua do Mário - Castelo de Viana");
                model.RegistarCliente("p3@gmail.com","Pessoa 3","1234",null,null);

                    //Pessoa 1
                    model.AddAvaliacao("p1@gmail.com",3,"Boa Pastelaria");
                    model.AddAvaliacao("p1@gmail.com",2,"Mau Serviço");

                    model.AddProdutoFavoritos("p1@gmail.com","chocolate");
                    model.AddProdutoCarrinhoCompras("p1@gmail.com","mandala");

                    //Pessoa 2
                    model.AddAvaliacao("p2@gmail.com",4,null);
                    model.AddProdutoCarrinhoCompras("p1@gmail.com","mandala");
                    model.AddProdutoCarrinhoCompras("p1@gmail.com","chocolate");
                    model.AddProdutoCarrinhoCompras("p1@gmail.com","mandala");
                    model.AddProdutoCarrinhoCompras("p1@gmail.com","maca");


                return model;

        }

        public static void CantinhoDasEncomendasTxt() {
            try {
                new TextUI(PovoarFacade()).Run();
            }
            catch (Exception e) {
                Console.WriteLine($"Erro fatal: {e.Message} [{e.ToString}]");
            }
        }

    }


}