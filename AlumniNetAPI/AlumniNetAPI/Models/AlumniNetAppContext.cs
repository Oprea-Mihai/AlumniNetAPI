using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace AlumniNetAPI.Models;

public partial class AlumniNetAppContext : DbContext
{
    private readonly IConfiguration? _configuration;
    private IDbConnection DbConnection { get; } = new SqlConnection();



    public AlumniNetAppContext()
    {
    }



    public AlumniNetAppContext(DbContextOptions<AlumniNetAppContext> options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
        DbConnection = new SqlConnection(this._configuration.GetConnectionString("MyContext"));
    }



    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        try
        { optionsBuilder.UseSqlServer(DbConnection.ConnectionString); }
        catch (Exception e) { throw new Exception(e.Message); }
    }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<Experience> Experiences { get; set; }

    public virtual DbSet<Faculty> Faculties { get; set; }

    public virtual DbSet<FinishedStudy> FinishedStudies { get; set; }

    public virtual DbSet<InvitedUser> InvitedUsers { get; set; }

    public virtual DbSet<LearningSchedule> LearningSchedules { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<Profile> Profiles { get; set; }

    public virtual DbSet<Specialization> Specializations { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<StudyProgram> StudyPrograms { get; set; }

    public virtual DbSet<User> Users { get; set; }

  
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Event>(entity =>
        {
            entity.ToTable("Event");

            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.EventName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Image)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Initiator)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.StartDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Experience>(entity =>
        {
            entity.ToTable("Experience");

            entity.Property(e => e.CompanyName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.JobTitle)
                .HasMaxLength(255)
                .IsUnicode(false);

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
                .HasConstraintName("FK_FinishedStudy_Specialization");

            entity.HasOne(d => d.StudyProgram).WithMany(p => p.FinishedStudies)
                .HasForeignKey(d => d.StudyProgramId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FinishedStudy_StudyProgram");
        });

        modelBuilder.Entity<InvitedUser>(entity =>
        {
            entity.ToTable("InvitedUser");

            entity.Property(e => e.Status).HasDefaultValueSql("((1))");
            entity.Property(e => e.UserId)
                .HasMaxLength(250)
                .IsUnicode(false);

            entity.HasOne(d => d.Event).WithMany(p => p.InvitedUsers)
                .HasForeignKey(d => d.EventId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InvitedUser_Event");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.InvitedUsers)
                .HasForeignKey(d => d.Status)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InvitedUser_Status");

            entity.HasOne(d => d.User).WithMany(p => p.InvitedUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InvitedUser_User");
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

            entity.Property(e => e.PostingDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserId)
                .HasMaxLength(250)
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
            entity.Property(e => e.Facebook)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Instagram)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.LinkedIn)
                .HasMaxLength(250)
                .IsUnicode(false);
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

        modelBuilder.Entity<Status>(entity =>
        {
            entity.ToTable("Status");

            entity.Property(e => e.StatusName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<StudyProgram>(entity =>
        {
            entity.ToTable("StudyProgram");

            entity.Property(e => e.ProgramName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.UserId)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Language)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasDefaultValueSql("('EN')");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Profile).WithMany(p => p.Users)
                .HasForeignKey(d => d.ProfileId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Profile");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
