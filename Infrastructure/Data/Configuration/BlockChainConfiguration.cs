using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration;

public class BlockChainConfiguration : IEntityTypeConfiguration<BlockChain>
{
    public void Configure(EntityTypeBuilder<BlockChain> builder)
    {
        builder.ToTable("blockchain");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id);

        builder.Property(p => p.FechaCreacion)
        .HasColumnType("datetime");
        
        builder.Property(p => p.FechaModificacion)
        .HasColumnType("datetime");

        builder.Property(p => p.HashGenerado)
        .IsRequired()
        .HasMaxLength(100);

        builder.HasOne(p => p.TipoNotificaciones)
        .WithMany(p => p.BlockChains)
        .HasForeignKey(p => p.IdNotificacion);

        builder.HasOne(p => p.HiloRespuestaNotificaciones)
        .WithMany(p => p.BlockChains)
        .HasForeignKey(p => p.IdHiloRespuesta);

        builder.HasOne(p => p.Auditorias)
        .WithMany(p => p.BlockChains)
        .HasForeignKey(p => p.IdAuditoria);
    }
}
