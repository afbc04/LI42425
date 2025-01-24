using Fase2.Features;
using ui;
using business;


var builder = WebApplication.CreateBuilder(args);

//� singleton porque deve ser unico para toda a aplica��o de modo a evitar inconsist�ncia de dados
// Registra o GestorProdutos como um servi�o Singleton
//Substituir pela bd se poss�vel
builder.Services.AddSingleton<IGestorProdutos, GestorProdutos>(sp =>
{
    var gestor = new GestorProdutos();
    gestor.AddProduto("Tarte de Ma��", 8.00f, "maca", "tarte_maca.png", "Uma deliciosa tarte de ma�� com um toque de canela e croc�ncia.");
    gestor.AddProduto("Tarte Rosas", 7.00f, "rosas", "tarte_rosa.png", "Imagine fatias finas de fruta moldadas em rosas.");
    gestor.AddProduto("Tarte de Mirtilo", 8.50f, "mirtilo", "tarte_mirtilo.png", "Cada fatia cont�m a combina��o perfeita de do�ura e acidez. O ideal para completar qualquer refei��o.");
    gestor.AddProduto("Tarte de Framboesa", 8.00f, "framboesa", "tarte_framboesa.png", "Esta � mais uma tarte onde cada fatia traz uma explos�o deliciosa de sabores. � ainda melhor quando acompanhada com uma bola de gelado.");
    gestor.AddProduto("Tarte de Chocolate", 9.00f, "chocolate", "tarte_chocolate.png", "A tarte de chocolate � sempre um prazer irresist�vel. O suave e cremoso creme de chocolate enriquecido com uma pitada de flor de sal � a combina��o perfeita para ficar rendido a esta tarte. Os frutos secos completam estes sabores, ao trazerem toques de croc�ncia.");
    gestor.AddProduto("Tarte Mandala", 8.00f, "mandala", "tarte_mandala.png", "Uma simples e saborosa tarte que lembra a primavera. A perfeita combina��o de sabores no recheio e o bonito desenho no topo fazem desta tarte �nica e especial");

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