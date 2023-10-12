using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration;

public class HilorespuestaNotificacionConfiguration : IEntityTypeConfiguration<HiloRespuestaNotificacion>
{
    public void Configure(EntityTypeBuilder<HiloRespuestaNotificacion> builder)
    {
        builder.ToTable("hilorespuestanotificacion");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id);

        builder.Property(p => p.NombreTipo)
        .IsRequired()
        .HasMaxLength(80);
    }
}
