using AppWebExemplo.Configs;
using HealthFlow.Components;
using HealthFlow.DAO;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddAntiforgery();

builder.Services.AddScoped<Conexao>();

builder.Services.AddScoped<UsuarioDAO>();

builder.Services.AddScoped<EspecialidadeDAO>();

builder.Services.AddScoped<PacienteDAO>();

builder.Services.AddScoped<ProfissionalDAO>();

builder.Services.AddScoped<AgendamentoDAO>();

builder.Services.AddScoped<FinanceiroDAO>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();

app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();