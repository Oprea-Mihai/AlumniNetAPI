using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AlumniNetAPI.Models;

public partial class AlumniNetAppContext : DbContext
{
    public AlumniNetAppContext()
    {
    }

    public AlumniNetAppContext(DbContextOptions<AlumniNetAppContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Experience> Experiences { get; set; }

    public virtual DbSet<Faculty> Faculties { get; set; }

    public virtual DbSet<FinishedStudy> FinishedStudies { get; set; }

    public virtual DbSet<LearningSchedule> LearningSchedules { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<Profile> Profiles { get; set; }

    public virtual DbSet<Specialization> Specializations { get; set; }

    public virtual DbSet<StudyProgram> StudyPrograms { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-1UAVMPK;Database=AlumniNetApp;Trusted_Connection=True;Trust Server Certificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Experience>(entity =>
        {
            entity.ToTable("Experience");

            entity.Property(e => e.CompanyName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.EndDate).HasColumnType("date");
            entity.Property(e => e.JobTitle)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.StartDate).HasColumnType("date");

            entity.HasOne(d => d.Profile).WithMany(p => p.Experiences)
                .HasForeignKey(d => d.ProfileId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Experience_Profile");
        });

        modelBuilder.Entity<Faculty>(entity =>
        {
            entity.ToTable("Faculty");

            entity.Property(e => e.FacultyName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<FinishedStudy>(entity =>
        {
            entity.ToTable("FinishedStudy");

            entity.HasOne(d => d.LearningSchedule).WithMany(p => p.FinishedStudies)
                .HasForeignKey(d => d.LearningScheduleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FinishedStudy_LearningSchedule");

            entity.HasOne(d => d.Profile).WithMany(p => p.FinishedStudies)
                .HasForeignKey(d => d.ProfileId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FinishedStudy_Profile");

            entity.HasOne(d => d.Specialization).WithMany(p => p.FinishedStudies)
                .HasForeignKey(d => d.SpecializationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FinishedStudy_Specialization");

            entity.HasOne(d => d.StudyProgram).WithMany(p => p.FinishedStudies)
                .HasForeignKey(d => d.StudyProgramId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FinishedStudy_StudyProgram");
        });

        modelBuilder.Entity<LearningSchedule>(entity =>
        {
            entity.ToTable("LearningSchedule");

            entity.Property(e => e.ScheduleName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.ToTable("Post");

            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.User).WithMany(p => p.Posts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Post_User");
        });

        modelBuilder.Entity<Profile>(entity =>
        {
            entity.ToTable("Profile");

            entity.Property(e => e.Description)
                .HasMaxLength(2000)
                .IsUnicode(false);

            entity.HasOne(d => d.User).WithMany(p => p.Profiles)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Profile_User");
        });

        modelBuilder.Entity<Specialization>(entity =>
        {
            entity.ToTable("Specialization");

            entity.Property(e => e.SpecializationName)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Faculty).WithMany(p => p.Specializations)
                .HasForeignKey(d => d.FacultyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Specialization_Faculty");
        });

        modelBuilder.Entity<StudyProgram>(entity =>
        {
            entity.ToTable("StudyProgram");

            entity.Property(e => e.ProgramName)
                .HasMaxLength(30)
                .IsFixedLength();
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FirebaseAuthToken)
                .HasMaxLength(255)
                .IsFixedLength();
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
