using Microsoft.EntityFrameworkCore;

namespace Parcial2Turró.Models;

public partial class PwadbContext : DbContext
{
    public PwadbContext()
    {
    }

    public PwadbContext(DbContextOptions<PwadbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Alumno> Alumnos { get; set; }

    public virtual DbSet<Inscripcion> Inscripcions { get; set; }

    public virtual DbSet<Materium> Materia { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\POO;Initial Catalog=PWADB;Integrated Security=True;Persist Security Info=False;Pooling=False;Multiple Active Result Sets=False;Encrypt=False;Trust Server Certificate=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Alumno>(entity =>
        {
            entity.HasKey(e => e.Dni);

            entity.ToTable("Alumno");

            entity.Property(e => e.Dni)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("DNI");
            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Domicilio)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FechaNacimiento).HasColumnType("date");
            entity.Property(e => e.Foto).IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SituacionBeca)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Inscripcion>(entity =>
        {
            entity.HasKey(e => new { e.Dnialumno, e.Idmateria });

            entity.ToTable("Inscripcion");

            entity.Property(e => e.Dnialumno)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("DNIAlumno");
            entity.Property(e => e.Idmateria)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("IDMateria");
            entity.Property(e => e.FechaInscripcion).HasColumnType("datetime");

            entity.HasOne(d => d.DnialumnoNavigation).WithMany(p => p.Inscripcions)
                .HasForeignKey(d => d.Dnialumno)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Inscripcion_Alumno");

            entity.HasOne(d => d.IdmateriaNavigation).WithMany(p => p.Inscripcions)
                .HasForeignKey(d => d.Idmateria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Inscripcion_Materia");
        });

        modelBuilder.Entity<Materium>(entity =>
        {
            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ID");
            entity.Property(e => e.FechaInicio).HasColumnType("date");
            entity.Property(e => e.ImporteInscripcion).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Sede)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Turno)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
