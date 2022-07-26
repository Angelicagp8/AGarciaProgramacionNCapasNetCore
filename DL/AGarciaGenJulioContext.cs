using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DL
{
    public partial class AGarciaGenJulioContext : DbContext
    {
        public AGarciaGenJulioContext()
        {
        }

        public AGarciaGenJulioContext(DbContextOptions<AGarciaGenJulioContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Alumno> Alumnos { get; set; } = null!;
        public virtual DbSet<Grupo> Grupos { get; set; } = null!;
        public virtual DbSet<Horario> Horarios { get; set; } = null!;
        public virtual DbSet<Plantel> Plantels { get; set; } = null!;
        public virtual DbSet<Semestre> Semestres { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=LAPTOP-CMCHQVFD; Database= AGarciaGenJulio; Trusted_Connection=True; User ID=sa; Password=pass@word1;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Alumno>(entity =>
            {
                entity.HasKey(e => e.IdAlumno)
                    .HasName("PK__Alumno__460B4740F3DCD2F3");

                entity.ToTable("Alumno");

                entity.HasIndex(e => e.Email, "UQ__Alumno__A9D10534E28DE04C")
                    .IsUnique();

                entity.Property(e => e.ApellidoMaterno)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ApellidoPaterno)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(264)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdSemestreNavigation)
                    .WithMany(p => p.Alumnos)
                    .HasForeignKey(d => d.IdSemestre)
                    .HasConstraintName("FK__Alumno__IdSemest__267ABA7A");
            });

            modelBuilder.Entity<Grupo>(entity =>
            {
                entity.HasKey(e => e.IdGrupo)
                    .HasName("PK__Grupo__303F6FD921349F5F");

                entity.ToTable("Grupo");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdPlantelNavigation)
                    .WithMany(p => p.Grupos)
                    .HasForeignKey(d => d.IdPlantel)
                    .HasConstraintName("FK__Grupo__IdPlantel__3A81B327");
            });

            modelBuilder.Entity<Horario>(entity =>
            {
                entity.HasKey(e => e.IdHorario)
                    .HasName("PK__Horario__1539229B2F5B19EF");

                entity.ToTable("Horario");

                entity.Property(e => e.Turno)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdAlumnoNavigation)
                    .WithMany(p => p.Horarios)
                    .HasForeignKey(d => d.IdAlumno)
                    .HasConstraintName("FK__Horario__IdAlumn__3E52440B");

                entity.HasOne(d => d.IdGrupoNavigation)
                    .WithMany(p => p.Horarios)
                    .HasForeignKey(d => d.IdGrupo)
                    .HasConstraintName("FK__Horario__IdGrupo__3D5E1FD2");
            });

            modelBuilder.Entity<Plantel>(entity =>
            {
                entity.HasKey(e => e.IdPlantel)
                    .HasName("PK__Plantel__485FDCFE651FE69C");

                entity.ToTable("Plantel");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Semestre>(entity =>
            {
                entity.HasKey(e => e.IdSemestre)
                    .HasName("PK__Semestre__BD1FD7F82E45570D");

                entity.ToTable("Semestre");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
