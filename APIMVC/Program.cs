using APIMVC.Interfaces;
using APIMVC.Repositories;
using APIMVC.Data; // Importa el namespace de tu DbContext
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Registra la configuración para que esté disponible para DbContext y otros servicios
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

// Registra el DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("APIMVC"))); // Usa "APIMVC" o el nombre de tu cadena de conexión

// Registra tus servicios y repositorios
builder.Services.AddScoped<IChatbotService, GeminiRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();