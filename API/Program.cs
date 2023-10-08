using System.Reflection;
using API.Extensions;
using AspNetCoreRateLimit;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

/*
    Program.cs = este archivo es conocido como el contendor de dependencias el cual va a contener todas las referencias a los metodos de extensión y acada uno de los middlework para tener acceso a cada  de esos metodos que pertenecen al entityframework
*/

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.ConfigurationRatelimiting();
builder.Services.ConfigureCors();
builder.Services.AddApplicationServices();
builder.Services.AddAutoMapper(Assembly.GetEntryAssembly());//Es propiamente de C#, el Assembly.GetEntreAssembly se utiliza para poder obtener la ruta de donde se está ejecutan el ensamblado el DLL en este caso el proyecto.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AnimalsContext>(optionsBuilder =>
{//AddDbContext<AnimalsContext>: Este método se utiliza para registrar el contexto de Entity Framework Core en la colección de servicios. AnimalsContext es el tipo de contexto de base de datos que estás registrando.
    string connectionString = builder.Configuration.GetConnectionString("MySqlConex");
    //Esta línea obtiene la cadena de conexión a la base de datos desde la configuración de la aplicación. La configuración de la cadena de conexión se suele definir en el archivo appsettings.json o en otro archivo de configuración. GetConnectionString("MySqlConex") recupera la cadena de conexión con el nombre "MySqlConex" de la configuración.
    optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    //Esta línea configura el proveedor de base de datos que Entity Framework Core utilizará para interactuar con la base de datos.
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
