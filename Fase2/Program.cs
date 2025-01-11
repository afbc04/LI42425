using Fase2.Components;
using ui;
using business;

/*
var builder = WebApplication.CreateBuilder(args);

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

app.Run();*/


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

}