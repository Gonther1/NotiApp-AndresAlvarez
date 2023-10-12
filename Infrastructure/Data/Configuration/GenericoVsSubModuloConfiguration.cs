using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration;

public class GenericoVsSubModuloConfiguration : IEntityTypeConfiguration<GenericoVsSubmodulo>
{
    public void Configure(EntityTypeBuilder<GenericoVsSubmodulo> builder)
    {
        builder.ToTable("genericosvssubmodulos");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id);

        builder.Property(p => p.FechaCreacion)
        .HasColumnType("datetime");
        
        builder.Property(p => p.FechaModificacion)
        .HasColumnType("datetime");

        builder.HasOne(p => p.SubModulos)
        .WithMany(p => p.GenericosVsSubmodulos)
        .HasForeignKey(p => p.IdSubmodulos);

        builder.HasOne(p => p.Roles)
        .WithMany(p => p.GenericosVssSubModulos)
        .HasForeignKey(p => p.IdRoles);

        builder.HasOne(p => p.PermisosGenericos)
        .WithMany(p => p.GenericosVsSubmodulos)
        .HasForeignKey(p => p.IdGenericos);
    }
}
