using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;
using UESAN.StoreDB.DOMAIN.Core.Entities;

namespace UESAN.StoreDB.DOMAIN.Infrastructure.Data;

public partial class PromcoserContext : DbContext
{
    public PromcoserContext()
    {
    }

    public PromcoserContext(DbContextOptions<PromcoserContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Cliente { get; set; }

    public virtual DbSet<DetalleParteDiario> DetalleParteDiario { get; set; }

    public virtual DbSet<LugarTrabajo> LugarTrabajo { get; set; }

    public virtual DbSet<Maquinaria> Maquinaria { get; set; }

    public virtual DbSet<ParteDiario> ParteDiario { get; set; }

    public virtual DbSet<Personal> Personal { get; set; }

    public virtual DbSet<Rol> Rol { get; set; }

    public virtual DbSet<User> User { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;database=Promcoser;user=root;password=123456789", ServerVersion.Parse("8.0.37-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PRIMARY");

            entity.ToTable("cliente");

            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.Apellido)
                .HasColumnType("text")
                .HasColumnName("apellido");
            entity.Property(e => e.CorreoElectronico)
                .HasColumnType("text")
                .HasColumnName("correo_electronico");
            entity.Property(e => e.Direccion)
                .HasColumnType("text")
                .HasColumnName("direccion");
            entity.Property(e => e.Nombre)
                .HasColumnType("text")
                .HasColumnName("nombre");
            entity.Property(e => e.RazonSocial)
                .HasColumnType("text")
                .HasColumnName("razon_social");
            entity.Property(e => e.Ruc)
                .HasColumnType("text")
                .HasColumnName("ruc");
            entity.Property(e => e.Telefono)
                .HasColumnType("text")
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<DetalleParteDiario>(entity =>
        {
            entity.HasKey(e => e.IdDetalleParte).HasName("PRIMARY");

            entity.ToTable("detalle_parte_diario");

            entity.HasIndex(e => e.IdParteDiario, "id_parte_diario");

            entity.Property(e => e.IdDetalleParte).HasColumnName("id_detalle_parte");
            entity.Property(e => e.CantidadAceite)
                .HasPrecision(10, 2)
                .HasColumnName("cantidad_aceite");
            entity.Property(e => e.CantidadPetroleo)
                .HasPrecision(10, 2)
                .HasColumnName("cantidad_petroleo");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.Horas)
                .HasPrecision(10, 2)
                .HasColumnName("horas");
            entity.Property(e => e.IdParteDiario).HasColumnName("id_parte_diario");
            entity.Property(e => e.TrabajoEfectuado)
                .HasColumnType("text")
                .HasColumnName("trabajo_efectuado");

            entity.HasOne(d => d.IdParteDiarioNavigation).WithMany(p => p.DetalleParteDiario)
                .HasForeignKey(d => d.IdParteDiario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("detalle_parte_diario_ibfk_1");
        });

        modelBuilder.Entity<LugarTrabajo>(entity =>
        {
            entity.HasKey(e => e.IdLugarTrabajo).HasName("PRIMARY");

            entity.ToTable("lugar_trabajo");

            entity.HasIndex(e => e.IdLugarTrabajo, "unique_id_lugar_trabajo").IsUnique();

            entity.Property(e => e.IdLugarTrabajo).HasColumnName("id_lugar_trabajo");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<Maquinaria>(entity =>
        {
            entity.HasKey(e => e.IdMaquinaria).HasName("PRIMARY");

            entity.ToTable("maquinaria");

            entity.Property(e => e.IdMaquinaria).HasColumnName("id_maquinaria");
            entity.Property(e => e.AnoFabricacion).HasColumnName("ano_fabricacion");
            entity.Property(e => e.Estado)
                .HasColumnType("text")
                .HasColumnName("estado");
            entity.Property(e => e.HorasFin)
                .HasPrecision(10, 2)
                .HasColumnName("horas_fin");
            entity.Property(e => e.HorasInicio)
                .HasPrecision(10, 2)
                .HasColumnName("horas_inicio");
            entity.Property(e => e.HorometroFin)
                .HasPrecision(10, 2)
                .HasColumnName("horometro_fin");
            entity.Property(e => e.HorometroInicio)
                .HasPrecision(10, 2)
                .HasColumnName("horometro_inicio");
            entity.Property(e => e.Marca)
                .HasColumnType("text")
                .HasColumnName("marca");
            entity.Property(e => e.Modelo)
                .HasColumnType("text")
                .HasColumnName("modelo");
            entity.Property(e => e.Placa)
                .HasColumnType("text")
                .HasColumnName("placa");
            entity.Property(e => e.TipoMaquinaria)
                .HasColumnType("text")
                .HasColumnName("tipo_maquinaria");
        });

        modelBuilder.Entity<ParteDiario>(entity =>
        {
            entity.HasKey(e => e.IdParte).HasName("PRIMARY");

            entity.ToTable("parte_diario");

            entity.HasIndex(e => e.IdCliente, "id_cliente");

            entity.HasIndex(e => e.IdLugarTrabajo, "id_lugar_trabajo");

            entity.HasIndex(e => e.IdMaquinaria, "id_maquinaria");

            entity.HasIndex(e => e.IdPersonal, "id_personal");

            entity.Property(e => e.IdParte).HasColumnName("id_parte");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.Fin)
                .HasPrecision(10, 2)
                .HasColumnName("fin");
            entity.Property(e => e.Firmas)
                .HasColumnType("text")
                .HasColumnName("firmas");
            entity.Property(e => e.HorometroFinal)
                .HasPrecision(10, 2)
                .HasColumnName("horometro_final");
            entity.Property(e => e.HorometroInicio)
                .HasPrecision(10, 2)
                .HasColumnName("horometro_inicio");
            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.IdLugarTrabajo).HasColumnName("id_lugar_trabajo");
            entity.Property(e => e.IdMaquinaria).HasColumnName("id_maquinaria");
            entity.Property(e => e.IdPersonal).HasColumnName("id_personal");
            entity.Property(e => e.Inicio)
                .HasPrecision(10, 2)
                .HasColumnName("inicio");
            entity.Property(e => e.Serie)
                .HasColumnType("text")
                .HasColumnName("serie");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.ParteDiario)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("parte_diario_ibfk_1");

            entity.HasOne(d => d.IdLugarTrabajoNavigation).WithMany(p => p.ParteDiario)
                .HasForeignKey(d => d.IdLugarTrabajo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("parte_diario_ibfk_3");

            entity.HasOne(d => d.IdMaquinariaNavigation).WithMany(p => p.ParteDiario)
                .HasForeignKey(d => d.IdMaquinaria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("parte_diario_ibfk_4");

            entity.HasOne(d => d.IdPersonalNavigation).WithMany(p => p.ParteDiario)
                .HasForeignKey(d => d.IdPersonal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("parte_diario_ibfk_2");
        });

        modelBuilder.Entity<Personal>(entity =>
        {
            entity.HasKey(e => e.IdPersonal).HasName("PRIMARY");

            entity.ToTable("personal");

            entity.HasIndex(e => e.IdRol, "id_rol");

            entity.Property(e => e.IdPersonal).HasColumnName("id_personal");
            entity.Property(e => e.Apellido)
                .HasColumnType("text")
                .HasColumnName("apellido");
            entity.Property(e => e.CorreoElectronico)
                .HasColumnType("text")
                .HasColumnName("correo_electronico");
            entity.Property(e => e.Direccion)
                .HasColumnType("text")
                .HasColumnName("direccion");
            entity.Property(e => e.Dni)
                .HasColumnType("text")
                .HasColumnName("dni");
            entity.Property(e => e.FechaIngreso).HasColumnName("fecha_ingreso");
            entity.Property(e => e.IdRol).HasColumnName("id_rol");
            entity.Property(e => e.Nombre)
                .HasColumnType("text")
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasColumnType("text")
                .HasColumnName("telefono");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Personal)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("personal_ibfk_1");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PRIMARY");

            entity.ToTable("rol");

            entity.Property(e => e.IdRol).HasColumnName("id_rol");
            entity.Property(e => e.DescripcionRol)
                .HasColumnType("text")
                .HasColumnName("descripcion_rol");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("user");

            entity.HasIndex(e => e.IdPersonal, "id_personal");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasColumnType("text")
                .HasColumnName("address");
            entity.Property(e => e.Country)
                .HasColumnType("text")
                .HasColumnName("country");
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.Email)
                .HasColumnType("text")
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasColumnType("text")
                .HasColumnName("first_name");
            entity.Property(e => e.IdPersonal).HasColumnName("id_personal");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.LastName)
                .HasColumnType("text")
                .HasColumnName("last_name");
            entity.Property(e => e.Password)
                .HasColumnType("text")
                .HasColumnName("password");
            entity.Property(e => e.Type)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("type");

            entity.HasOne(d => d.IdPersonalNavigation).WithMany(p => p.User)
                .HasForeignKey(d => d.IdPersonal)
                .HasConstraintName("user_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
