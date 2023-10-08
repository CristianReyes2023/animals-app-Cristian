
using System.Reflection;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;
public class AnimalsContext : DbContext
{
    public AnimalsContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Pais> Paises { get; set; }
    public DbSet<Departamento> Departamentos { get; set; }
    public DbSet<Ciudad> Ciudades { get; set; }
    public DbSet<Mascota> Mascotas { get; set; }
    public DbSet<Raza> Razas { get; set; }
    public DbSet<Servicio> Servicios { get; set; }
    public DbSet<ClienteDireccion> ClienteDirecciones { get; set; }
    public DbSet<ClienteTelefono> ClienteTelefonos { get; set; }
    public DbSet<Cita> Citas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    /*Este método se llama cuando se está creando el modelo de base de datos utilizando Entity Framework Core. El objeto modelBuilder se utiliza para definir la estructura de las tablas y sus relaciones.
    */
    {
        base.OnModelCreating(modelBuilder);
        //base.OnModelCreating(modelBuilder);: La primera línea de este método llama al método OnModelCreating de la clase base (DbContext). Esto es importante porque permite que Entity Framework Core ejecute las configuraciones básicas de las convenciones de nombres de tablas y claves primarias. Si no se llamara a este método, se perderían las convenciones predeterminadas de Entity Framework Core.
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        //Busca y aplica clases de configuración de entidades que definan cómo se deben mapear las clases de entidades a las tablas de la base de datos.
    }
}
