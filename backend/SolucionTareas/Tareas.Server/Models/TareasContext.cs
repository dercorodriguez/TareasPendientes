using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Tareas.Server.Models;

public partial class TareasContext : DbContext
{
    public TareasContext()
    {
    }

    public TareasContext(DbContextOptions<TareasContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TareasPendiente> TareasPendientes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TareasPendiente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tarea");

            entity.ToTable("tareasPendientes");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Completada)
              .HasMaxLength(1)
              .IsUnicode(false)
              .HasDefaultValue(true);
            entity.Property(e => e.Estado)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValue("A");
            entity.Property(e => e.FechaCreacion)
                .HasColumnType("datetime")
                .HasColumnName("Fecha_Creacion");
            entity.Property(e => e.FechaVencimiento)
                .HasColumnType("datetime")
                .HasColumnName("Fecha_Vencimiento");
            entity.Property(e => e.Titulo)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
