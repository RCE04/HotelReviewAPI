using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProyectoFinalCursoAPI.Models;

public partial class HotelReviewContext : DbContext
{
    public HotelReviewContext()
    {
    }

    public HotelReviewContext(DbContextOptions<HotelReviewContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Comentario> Comentarios { get; set; }

    public virtual DbSet<Lugare> Lugares { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=ep-super-sky-a4y3fvc0-pooler.us-east-1.aws.neon.tech;Database=HotelReview;Username=HotelReview_owner;Password=npg_7IVejNowvmG8;SSL Mode=Require");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comentario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("comentarios_pkey");

            entity.ToTable("comentarios");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.ComentarioLugar).HasColumnName("comentarioLugar");
            entity.Property(e => e.LugarId).HasColumnName("lugarId");
            entity.Property(e => e.Puntuacion)
                .HasColumnType("character varying")
                .HasColumnName("puntuacion");
            entity.Property(e => e.UsuarioId).HasColumnName("usuarioId");
        });

        modelBuilder.Entity<Lugare>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("lugares_pkey");

            entity.ToTable("lugares");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.Direccion).HasColumnName("direccion");
            entity.Property(e => e.Imagen)
                .HasColumnType("character varying")
                .HasColumnName("imagen");
            entity.Property(e => e.NombreLugar).HasColumnName("nombreLugar");
            entity.Property(e => e.Precio).HasColumnName("precio");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("usuarios_pkey");

            entity.ToTable("usuarios");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Contraseña)
                .HasColumnType("character varying")
                .HasColumnName("contraseña");
            entity.Property(e => e.Favoritos).HasColumnName("favoritos");
            entity.Property(e => e.NombreUsuario)
                .HasColumnType("character varying")
                .HasColumnName("nombreUsuario");
            entity.Property(e => e.Rol)
                .HasColumnType("character varying")
                .HasColumnName("rol");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
