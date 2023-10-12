using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration;

public class RolVsMaestroConfiguration : IEntityTypeConfiguration<RolVsMaestro>
{
    public void Configure(EntityTypeBuilder<RolVsMaestro> builder)
    {
        builder.ToTable("rolvsmaestro");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id);

        builder.Property(p => p.FechaCreacion)
        .HasColumnType("datetime");

        builder.Property(p => p.FechaModificacion)
        .HasColumnType("datetime");

        builder.HasOne(p => p.Roles)
        .WithMany(p => p.RolesVsMaestros)
        .HasForeignKey(p => p.IdRol);

        builder.HasOne(p => p.ModulosMaestros)
        .WithMany(p => p.RolesVsMaestros)
        .HasForeignKey(p => p.IdMaestro);
    }
}
