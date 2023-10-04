using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration;

public class MascotaConfiguration : IEntityTypeConfiguration<Mascota>
{
    public void Configure(EntityTypeBuilder<Mascota> builder)
    {
        builder.ToTable("mascotas");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id);

        builder.Property(x => x.Nombre).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Especie).IsRequired().HasMaxLength(50);

        builder.HasOne(x => x.Razas).WithMany(x => x.Mascotas).HasForeignKey(x => x.IdRazaFk);

        builder.Property(x => x.FechaNacimiento).HasColumnType("datetime");
        builder.HasOne(x => x.Clientes).WithMany(x => x.Mascotas).HasForeignKey(x => x.IdClienteFk);
    }
}