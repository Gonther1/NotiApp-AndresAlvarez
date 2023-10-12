using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration;

public class EstadoNotificacionConfiguration : IEntityTypeConfiguration<EstadoNotificacion>
{
    public void Configure(EntityTypeBuilder<EstadoNotificacion> builder)
    {
        builder.ToTable("estadonotificacion");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id);

        builder.Property(p => p.FechaCreacion)
        .HasColumnType("datetime");
        
        builder.Property(p => p.FechaModificacion)
        .HasColumnType("datetime");

        builder.Property(p => p.NombreEstado)
        .IsRequired()
        .HasMaxLength(50);
    }
}
