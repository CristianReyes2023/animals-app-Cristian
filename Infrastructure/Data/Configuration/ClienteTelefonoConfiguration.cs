using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration;

public class ClienteTelefonoConfiguration : IEntityTypeConfiguration<ClienteTelefono>
{
    public void Configure(EntityTypeBuilder<ClienteTelefono> builder)
    {
        builder.ToTable("clientetelefono");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id);
        
        builder.Property(x => x.Numero).IsRequired().HasMaxLength(20);

        builder.HasOne(x => x.Clientes).WithMany(x => x.ClienteTelefonos).HasForeignKey(x => x.IdClienteFk);
        //Here you can configure the properties using the object 'Builder'.
    }
}