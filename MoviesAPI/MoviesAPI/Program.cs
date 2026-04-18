using Microsoft.EntityFrameworkCore;
using MoviesAPI.Models;
using MoviesAPI.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<MovieService>();
builder.Services.AddScoped<DirectorService>();
// Registrar DbContext con la cadena de conexión del appsettings.json
builder.Services.AddDbContext<MoviesDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MoviesDB")));
// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy.WithOrigins("http://localhost:5173") // origen de tu frontend
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});



builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();
// Activar CORS antes de autenticación/autorización
app.UseCors("AllowFrontend");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
