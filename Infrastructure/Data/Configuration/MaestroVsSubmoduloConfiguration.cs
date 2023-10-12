using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration;

public class MaestroVsSubmoduloConfiguration : IEntityTypeConfiguration<MaestroVsSubmodulos>
{
    public void Configure(EntityTypeBuilder<MaestroVsSubmodulos> builder)
    {
        builder.ToTable("maestrosvssubmodulos");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id);

        builder.Property(p => p.FechaCreacion)
        .HasColumnType("datetime");
        
        builder.Property(p => p.FechaModificacion)
        .HasColumnType("datetime");

        builder.HasOne(p => p.ModulosMaestros)
        .WithMany(p => p.MaestrosVsSubmodulos)
        .HasForeignKey(p => p.IdMaestro);   

        builder.HasOne(p => p.SubModulos)
        .WithMany(p => p.MaestroVsSubmodulos)
        .HasForeignKey(p => p.IdSubmodulo);
    }
}
