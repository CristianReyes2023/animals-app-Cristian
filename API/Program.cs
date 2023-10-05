using System.Reflection;
using API.Extensions;
using AspNetCoreRateLimit;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.ConfigurationRatelimiting();//
builder.Services.ConfigureCors();
builder.Services.AddAutoMapper(Assembly.GetEntryAssembly());//Es propiamente de C#, el Assembly.GetEntreAssembly se utiliza para poder obtener la ruta de donde se está ejecutan el ensamblado el DLL en este caso el proyecto.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AnimalsContext>(optionsBuilder =>
{//
    string connectionString = builder.Configuration.GetConnectionString("MySqlConex");
    optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{ 
    app.UseSwagger();
    app.UseSwaggerUI();
}
//Se necesita para completar nuestra configuración de dependencia, llamamos nuestra configuración de Cors y Ip en este caso. Y eso lo hacemos atraves niderwork

app.UseCors("CorsPolicy");
app.UseHttpsRedirection();
app.UseIpRateLimiting();

app.UseAuthorization();

app.MapControllers();

app.Run();
