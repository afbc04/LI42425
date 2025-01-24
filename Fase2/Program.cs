using Fase2.Features;
using ui;
using business;


var builder = WebApplication.CreateBuilder(args);

//É singleton porque deve ser unico para toda a aplicação de modo a evitar inconsistência de dados
// Registra o GestorProdutos como um serviço Singleton
//Substituir pela bd se possível
builder.Services.AddSingleton<IGestorProdutos, GestorProdutos>(sp =>
{
    var gestor = new GestorProdutos();
    gestor.AddProduto("Tarte de Maçã", 8.00f, "maca", "tarte_maca.png", "Uma deliciosa tarte de maçã com um toque de canela e crocância.");
    gestor.AddProduto("Tarte Rosas", 7.00f, "rosas", "tarte_rosa.png", "Imagine fatias finas de fruta moldadas em rosas.");
    gestor.AddProduto("Tarte de Mirtilo", 8.50f, "mirtilo", "tarte_mirtilo.png", "Cada fatia contém a combinação perfeita de doçura e acidez. O ideal para completar qualquer refeição.");
    gestor.AddProduto("Tarte de Framboesa", 8.00f, "framboesa", "tarte_framboesa.png", "Esta é mais uma tarte onde cada fatia traz uma explosão deliciosa de sabores. É ainda melhor quando acompanhada com uma bola de gelado.");
    gestor.AddProduto("Tarte de Chocolate", 9.00f, "chocolate", "tarte_chocolate.png", "A tarte de chocolate é sempre um prazer irresistível. O suave e cremoso creme de chocolate enriquecido com uma pitada de flor de sal é a combinação perfeita para ficar rendido a esta tarte. Os frutos secos completam estes sabores, ao trazerem toques de crocância.");
    gestor.AddProduto("Tarte Mandala", 8.00f, "mandala", "tarte_mandala.png", "Uma simples e saborosa tarte que lembra a primavera. A perfeita combinação de sabores no recheio e o bonito desenho no topo fazem desta tarte única e especial");

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