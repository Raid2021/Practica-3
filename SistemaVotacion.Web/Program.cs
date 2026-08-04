using SistemaVotacion.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Clientes tipados de la API vía IHttpClientFactory (evita fugas de sockets y permite configurar BaseAddress una sola vez)
var apiBaseUrl = builder.Configuration["ApiSettings:BaseUrl"]
    ?? throw new InvalidOperationException("No se configuró ApiSettings:BaseUrl.");

builder.Services.AddHttpClient<IVotanteApiService, VotanteApiService>(client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
});

builder.Services.AddHttpClient<IPartidoApiService, PartidoApiService>(client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
});

builder.Services.AddHttpClient<IVotacionApiService, VotacionApiService>(client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
