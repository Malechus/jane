using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace jane.Models;

public partial class HouseholdContext : DbContext
{
    private string ConnectionStr;

    public HouseholdContext(string connectionStr)
    {
        ConnectionStr = connectionStr;
    }

    public HouseholdContext(DbContextOptions<HouseholdContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DailyChore> DailyChores { get; set; }

    public virtual DbSet<TransactionalChore> TransactionalChores { get; set; }

    public virtual DbSet<WeeklyChore> WeeklyChores { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql(ConnectionStr, Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.32-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("user");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FirstName).HasMaxLength(25).HasColumnName("first_name");
            entity.Property(e => e.DiscordID).HasColumnName("discord_id");
            entity.Property(e => e.Group).HasMaxLength(25).HasColumnName("group");
            entity.Property(e => e.DayOff).HasColumnName("day_off");
        });

        modelBuilder.Entity<DailyChore>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("daily_chore");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active)
                .HasColumnType("bit(1)")
                .HasColumnName("active");
            entity.Property(e => e.ChoreName)
                .HasMaxLength(50)
                .HasColumnName("chore_name");
            entity.Property(e => e.Friday)
                .HasMaxLength(25)
                .HasColumnName("friday");
            entity.Property(e => e.Monday)
                .HasMaxLength(25)
                .HasColumnName("monday");
            entity.Property(e => e.Notes)
                .HasMaxLength(255)
                .HasColumnName("notes");
            entity.Property(e => e.Saturday)
                .HasMaxLength(25)
                .HasColumnName("saturday");
            entity.Property(e => e.Sunday)
                .HasMaxLength(25)
                .HasColumnName("sunday");
            entity.Property(e => e.Thursday)
                .HasMaxLength(25)
                .HasColumnName("thursday");
            entity.Property(e => e.Tuesday)
                .HasMaxLength(25)
                .HasColumnName("tuesday");
            entity.Property(e => e.Wednesday)
                .HasMaxLength(25)
                .HasColumnName("wednesday");
        });

        modelBuilder.Entity<TransactionalChore>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("transactional_chore");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ChoreId).HasColumnName("chore_id");
            entity.Property(e => e.ChoreName).HasMaxLength(50).HasColumnName("chore_name");
            entity.Property(e => e.Completed)
                .HasColumnType("bit(1)")
                .HasColumnName("completed");
            entity.Property(e => e.CompletedDatetime)
                .HasColumnType("datetime")
                .HasColumnName("completed_datetime");
            entity.Property(e => e.WeekOf).HasColumnName("week_of");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Owner).HasMaxLength(25).HasColumnName("owner");
        });

        modelBuilder.Entity<WeeklyChore>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("weekly_chore");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active)
                .HasColumnType("bit(1)")
                .HasColumnName("active");
            entity.Property(e => e.ChoreName)
                .HasMaxLength(50)
                .HasColumnName("chore_name");
            entity.Property(e => e.Notes)
                .HasMaxLength(255)
                .HasColumnName("notes");
            entity.Property(e => e.WeekFour)
                .HasMaxLength(25)
                .HasColumnName("week_four");
            entity.Property(e => e.WeekOne)
                .HasMaxLength(25)
                .HasColumnName("week_one");
            entity.Property(e => e.WeekThree)
                .HasMaxLength(25)
                .HasColumnName("week_three");
            entity.Property(e => e.WeekTwo)
                .HasMaxLength(25)
                .HasColumnName("week_two");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
