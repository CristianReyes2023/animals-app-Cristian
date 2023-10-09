using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreRateLimit;
using Core.Interfaces;
using Infrastructure.UnitOfWork;

namespace API.Extensions;
public static class ApplicationServiceExtension //Agregar el STATIC   IMPORTANTE
{
    public static void ConfigureCors(this IServiceCollection services) => services.AddCors(options =>
    {
        options.AddPolicy("CorsPolicy", builder =>
        {
            builder.AllowAnyOrigin() // WithOrigins("https://domain.com")
            .AllowAnyMethod() // WithMethods("GET", "POST")
            .AllowAnyHeader(); // WithHeaders("accept", "content-type")
        });
    }); // Remember to put 'static' on the class and to add builder.Services.AddApplicationServices(); to Program.cs and builder.Services.ConfigureCors(); and app.UseCors("CorsPolicy");
    
    // public static void AddApplicationServices(this IServiceCollection services)
    // {
    //     services.AddScoped<IUnitOfWork, UnitOfWork>();
    // }


    //Agregamos le metodo de extensión que nos va a permitir la UnitOfWork al scope del EntityFramework
    //Si no tuviera una UnitOfWork tendría que hacer por cada repositorio, tendría que hacer este proceso. Aca es donde se va a crear una instancia por cada una de ellas la usemos o no la usemos. Acá estamos haciendo una sola instancia que es la UnitOfWork por medio de esa unidad de trabajo hacemos instancias unicas de las otras instancias.
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
    public static void ConfigurationRatelimiting (this IServiceCollection services)
    {//Vamos a poder determinar en que momento vamos a establecer ese rate limit atraves de los encabezados en un expresion y poder determinar dentro de header de la petición cuantas nos quedan disponibles.
        services.AddMemoryCache(); //Aministrar primero memoria cache
        services.AddSingleton<IRateLimitConfiguration,RateLimitConfiguration>();//es un método que se utiliza para registrar una implementación específica de una interfaz o una clase con el contenedor de inyección de dependencias. La implementación registrada se crea una vez y se reutiliza en toda la aplicación cada vez que se necesita.
        services.AddInMemoryRateLimiting();//Argumentos y metodos que permiten almacer información en memoria de manera temporal
        services.Configure<IpRateLimitOptions>(options =>//aca determino cuales son las diferentes opciones que va a tener ese limite
        {
            options.EnableEndpointRateLimiting =true;
            options.StackBlockedRequests = false;
            options.HttpStatusCode = 429;
            options.RealIpHeader = "X-Real-IP";//Permite establer encabezados y poder determinadar dentro de header de la petición cuantas peticiones nos queda disponibles
            options.GeneralRules = new List<RateLimitRule>
            {
                new RateLimitRule
                {//Algo importante que tenemos en el rate limit es está configuración que tenemos acontinuación
                Endpoint = "*",
                Period = "10s",
                Limit = 2
                }
            };
        });
        
    }
}
