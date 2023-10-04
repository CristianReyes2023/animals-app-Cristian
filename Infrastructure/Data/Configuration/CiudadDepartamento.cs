using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class CiudadDepartamento : IEntityTypeConfiguration<Ciudad>
    {
        public void Configure(EntityTypeBuilder<Ciudad> builder)
        {
            builder.ToTable("ciudad");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id);

            builder.Property(x => x.NombreCiudad).IsRequired().HasMaxLength(50);
            builder.HasOne(x => x.Departamentos).WithMany(x => x.Ciudades).HasForeignKey(x => x.IdDepFk);
        }
    }
}