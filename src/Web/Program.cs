using Microsoft.AspNetCore.Authentication.Cookies;
using SuporteSolidarioBusiness.Application.Repositories;
using SuporteSolidarioBusiness.Application.Services;
using SuporteSolidarioBusiness.Application.UseCases;
using SuporteSolidarioBusiness.Infrastructure.MySQL.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
    options => {
        options.Cookie.Name = "SuporteSolidarioCookie";
        options.LoginPath = "/Home/Index";
        });

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Add services to the container.
builder.Services.AddControllersWithViews();

//  Repositorios MySQL
builder.Services.AddDbContext<SuporteSolidarioDbContext>();
builder.Services.AddScoped<IDatabase, MySqlDatabase>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioMySqlRepository>();
builder.Services.AddScoped<IAuthRepository, AuthMySqlRepository>();
builder.Services.AddScoped<ICategoriaRepository, CategoriaMySqlRepository>();
builder.Services.AddScoped<IServicoRepository, ServicoMySqlRepository>();
builder.Services.AddScoped<IClienteRepository, ClienteMySqlRepository>();
builder.Services.AddScoped<IColaboradorRepository, ColaboradorMySqlRepository>();
builder.Services.AddScoped<IColaboradorServicoRepository, ColaboradorServicoMySqlRepository>();
builder.Services.AddScoped<IAtendimentoRepository, AtendimentoMySqlRepository>();
builder.Services.AddScoped<IAtendimentoMensagemRepository, AtendimentoMensagemMySqlRepository>();
builder.Services.AddScoped<ISolicitacaoRepository, SolicitacaoRepository>();

//  Repositorios MySQL

builder.Services.AddSingleton<ICryptoService, CryptoService>();
builder.Services.AddSingleton<ITokenService, TokenService>();
builder.Services.AddSingleton<IGeoLocalizacaoService, GeoLocalizacaoGoogleService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Realizará a inicialização do banco de dados, caso já não tenha sido inicializado
InicializarDatabaseUseCase inicializarDatabase = new InicializarDatabaseUseCase(app.Services);
inicializarDatabase.Execute();

app.Run();
