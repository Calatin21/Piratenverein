using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Piratenverein.Models;

public partial class PiratenVereinContext : DbContext
{
    public PiratenVereinContext()
    {
    }

    public PiratenVereinContext(DbContextOptions<PiratenVereinContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Pirat> Pirats { get; set; }

    public virtual DbSet<PiratFormer> PiratFormers { get; set; }

    public virtual DbSet<PiratJunior> PiratJuniors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=PiratenVerein;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pirat>(entity =>
        {
            entity.HasKey(e => e.Spitzname).HasName("pk_pirat");

            entity.ToTable("Pirat");

            entity.Property(e => e.Spitzname)
                .HasMaxLength(55)
                .IsUnicode(false)
                .HasColumnName("spitzname");
            entity.Property(e => e.Jahresalter).HasColumnName("jahresalter");
            entity.Property(e => e.Nachname)
                .HasMaxLength(55)
                .IsUnicode(false)
                .HasColumnName("nachname");
            entity.Property(e => e.Vorname)
                .HasMaxLength(55)
                .IsUnicode(false)
                .HasColumnName("vorname");
        });

        modelBuilder.Entity<PiratFormer>(entity =>
        {
            entity.HasKey(e => e.Spitzname).HasName("pk_piratformer");

            entity.ToTable("PiratFormer");

            entity.Property(e => e.Spitzname)
                .HasMaxLength(55)
                .IsUnicode(false)
                .HasColumnName("spitzname");
            entity.Property(e => e.Jahresalter).HasColumnName("jahresalter");
            entity.Property(e => e.Nachname)
                .HasMaxLength(55)
                .IsUnicode(false)
                .HasColumnName("nachname");
            entity.Property(e => e.Vorname)
                .HasMaxLength(55)
                .IsUnicode(false)
                .HasColumnName("vorname");
        });

        modelBuilder.Entity<PiratJunior>(entity =>
        {
            entity.HasKey(e => e.Spitzname).HasName("pk_piratjunior");

            entity.ToTable("PiratJunior");

            entity.Property(e => e.Spitzname)
                .HasMaxLength(55)
                .IsUnicode(false)
                .HasColumnName("spitzname");
            entity.Property(e => e.Jahresalter).HasColumnName("jahresalter");
            entity.Property(e => e.Nachname)
                .HasMaxLength(55)
                .IsUnicode(false)
                .HasColumnName("nachname");
            entity.Property(e => e.Vorname)
                .HasMaxLength(55)
                .IsUnicode(false)
                .HasColumnName("vorname");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
