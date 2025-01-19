using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;
using TestMySql.Entities;

namespace TestMySql;

public partial class StudentCourseDbContext : DbContext
{
    public StudentCourseDbContext()
    {
    }

    public StudentCourseDbContext(DbContextOptions<StudentCourseDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Enrollment> Enrollments { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=127.0.0.1;uid=root;pwd=12345;database=StudentCourseDB", Microsoft.EntityFrameworkCore.ServerVersion.Parse("9.1.0-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("PRIMARY");

            entity.ToTable("courses");

            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.CourseName).HasMaxLength(100);
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("curdate()");
            entity.Property(e => e.Description).HasColumnType("text");
        });

        modelBuilder.Entity<Enrollment>(entity =>
        {
            entity.HasKey(e => e.EnrollmentId).HasName("PRIMARY");

            entity.ToTable("enrollments");

            entity.HasIndex(e => e.CourseId, "CourseID");

            entity.HasIndex(e => e.StudentId, "StudentID");

            entity.Property(e => e.EnrollmentId).HasColumnName("EnrollmentID");
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.EnrollmentDate).HasDefaultValueSql("curdate()");
            entity.Property(e => e.Grade)
                .HasDefaultValueSql("'Incomplete'")
                .HasColumnType("enum('A','B','C','D','F','Incomplete')");
            entity.Property(e => e.StudentId).HasColumnName("StudentID");

            entity.HasOne(d => d.Course).WithMany(p => p.Enrollments)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("enrollments_ibfk_2");

            entity.HasOne(d => d.Student).WithMany(p => p.Enrollments)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("enrollments_ibfk_1");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PRIMARY");

            entity.ToTable("students");

            entity.HasIndex(e => e.Email, "Email").IsUnique();

            entity.Property(e => e.StudentId).HasColumnName("StudentID");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.EnrollmentDate).HasDefaultValueSql("curdate()");
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Gender).HasColumnType("enum('Male','Female','Other')");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
