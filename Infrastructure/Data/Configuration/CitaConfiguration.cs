using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class CitaConfiguration : IEntityTypeConfiguration<Cita>
    {
        public void Configure(EntityTypeBuilder<Cita> builder)
        {
            builder.ToTable("cita");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id);
            
            builder.Property(x=>x.Fecha).HasColumnType("date");
            builder.Property(x=>x.Hora).HasColumnType("time");

            builder.HasOne(x => x.Clientes).WithMany(x => x.Citas).HasForeignKey(x => x.IdClienteFk);

            builder.HasOne(x => x.Servicios).WithMany(x => x.Citas).HasForeignKey(x => x.IdServicioFk);

            builder.HasOne(x => x.Mascotas).WithMany(x => x.Citas).HasForeignKey(x => x.IdMascotaFk);
        }
    }
}