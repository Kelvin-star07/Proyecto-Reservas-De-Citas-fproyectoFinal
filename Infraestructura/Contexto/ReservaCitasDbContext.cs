using System;
using System.Collections.Generic;
using Infraestructura.Modelos;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura.Contexto;

public partial class ReservaCitasDbContext : DbContext
{
    public ReservaCitasDbContext()
    {
    }

    public ReservaCitasDbContext(DbContextOptions<ReservaCitasDbContext> options)
        : base(options)
    {
    }


    public virtual DbSet<ConfiguracionReserva> ConfiguracionReservas { get; set; }

    public virtual DbSet<Estacione> Estaciones { get; set; }

    public virtual DbSet<RegistroUsuario> RegistroUsuarios { get; set; }

    public virtual DbSet<ReservaCita> ReservaCitas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ConfiguracionReserva>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Configur__3214EC07BE47F4E5");

            entity.ToTable("ConfiguracionReserva");

            entity.HasIndex(e => e.CantidadEstaciones, "idx_Estaciones");

            entity.HasIndex(e => new { e.Turno, e.Fecha }, "idx_nurnoFecha").IsUnique();

            entity.Property(e => e.HoraFin).HasColumnType("time").HasPrecision(0);
            entity.Property(e => e.HoraInicio).HasColumnType("time").HasPrecision(0);
            entity.Property(e => e.Turno)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Estacione>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Estacion__3214EC077E7B365B");

            entity.HasIndex(e => e.Id, "idx_estacion");

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<RegistroUsuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Registro__3214EC076AA3A1C2");

            entity.ToTable("RegistroUsuario");

            entity.HasIndex(e => e.Correo, "UQ__Registro__60695A19309A357D").IsUnique();

            entity.HasIndex(e => e.Contraseña, "UQ__Registro__A961D9D23225BB50").IsUnique();

            entity.HasIndex(e => e.Cedula, "UQ__Registro__B4ADFE38ADE8AF5C").IsUnique();

            entity.HasIndex(e => new { e.Id, e.Correo }, "idx_Nombre");

            entity.HasIndex(e => new { e.Nombre, e.Apellido, e.Cedula, e.Correo, e.Contraseña, e.Sexo }, "idx_noRepetirRegistro").IsUnique();

            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Cedula)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Contraseña)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Sexo)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
        });

        modelBuilder.Entity<ReservaCita>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ReservaC__3214EC07BCC59AFC");

            entity.HasIndex(e => new { e.IdUsuario, e.Fecha }, "UQ_Usuario_Fecha").IsUnique();

            entity.HasIndex(e => new { e.IdUsuario, e.IdEstacion }, "idx_idUsuario_idEstacion");

            entity.HasIndex(e => new { e.IdEstacion, e.Fecha, e.Hora }, "idx_noRepetirEstacion").IsUnique();

            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("Pendiente");
            entity.Property(e => e.Hora).HasPrecision(0);
            entity.Property(e => e.Turno)
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.HasOne(d => d.IdEstacionNavigation).WithMany(p => p.ReservaCita)
                .HasForeignKey(d => d.IdEstacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ReservaCi__IdEst__4222D4EF");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.ReservaCita)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ReservaCi__IdUsu__412EB0B6");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
