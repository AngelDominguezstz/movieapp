using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MoviesAPI.Models;

public partial class MoviesDbContext : DbContext
{
    public MoviesDbContext()
    {
    }

    public MoviesDbContext(DbContextOptions<MoviesDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Director> Directors { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost,1433;Database=MoviesDB;User Id=sa;Password=Dominguez.021;Encrypt=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Director>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Director__3214EC07F1F81F3E");

            entity.ToTable("Director");

            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Nationality)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Movies__3214EC07030552B7");

            entity.Property(e => e.Fkdirector).HasColumnName("FKDirector");
            entity.Property(e => e.Gender)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.FkdirectorNavigation).WithMany(p => p.Movies)
                .HasForeignKey(d => d.Fkdirector)
                .HasConstraintName("FK_Movies_Director");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
