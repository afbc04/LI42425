using Fase2.Features;
using ui;
using business;


var builder = WebApplication.CreateBuilder(args);

//É singleton porque deve ser unico para toda a aplicação de modo a evitar inconsistência de dados
// Registra o GestorProdutos como um serviço Singleton
builder.Services.AddSingleton<IFacade, Facade>(sp =>
{
    var gestor = new Facade();
    IList<string> t1 = new List<string>();
    ISet<Material> t2 = new HashSet<Material>();
    gestor.AddTarte("maca", "Tarte de Maçã", 8.00f, "tarte_maca.png", "Uma deliciosa tarte de maçã com um toque de canela e crocância.", t2, t1);
    gestor.AddTarte("rosas", "Tarte Rosas", 7.00f, "tarte_rosa.png", "Imagine fatias finas de fruta moldadas em rosas.", t2, t1);
    gestor.AddTarte("mirtilo", "Tarte de Mirtilo", 8.50f, "tarte_mirtilo.png", "Cada fatia contém a combinação perfeita de doçura e acidez. O ideal para completar qualquer refeição.", t2, t1);
    gestor.AddTarte("framboesa", "Tarte de Framboesa", 8.00f, "tarte_framboesa.png", "Esta é mais uma tarte onde cada fatia traz uma explosão deliciosa de sabores. É ainda melhor quando acompanhada com uma bola de gelado.", t2, t1);
    gestor.AddTarte("chocolate", "Tarte de Chocolate", 9.00f, "tarte_chocolate.png", "A tarte de chocolate é sempre um prazer irresistível. O suave e cremoso creme de chocolate enriquecido com uma pitada de flor de sal é a combinação perfeita para ficar rendido a esta tarte. Os frutos secos completam estes sabores, ao trazerem toques de crocância.", t2, t1);
    gestor.AddTarte("mandala", "Tarte Mandala", 8.00f, "tarte_mandala.png", "Uma simples e saborosa tarte que lembra a primavera. A perfeita combinação de sabores no recheio e o bonito desenho no topo fazem desta tarte única e especial", t2, t1);

    return gestor;
});

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

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

builder.Services.AddSingleton(sp => new EmailService(
    smtpServer: "smtp.gmail.com",
    port: 587,
    senderEmail: "seuemail@gmail.com",
    senderPassword: "suasenha"
));


builder.Services.AddSingleton<RecoveryCodeService>();

app.Run();

/*
namespace CantinhoDasEncomendas {

    public class CantinhoDasEncomendas {

        public static void Main(string[] args) {
            try {
                Console.WriteLine("Iniciar");
                new TextUI().Run();
                Console.WriteLine("Fim");
            }
            catch (Exception e) {
                Console.WriteLine($"Erro fatal: {e.Message} [{e.ToString}]");
            }
        }

    }

} */