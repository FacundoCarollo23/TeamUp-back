﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TeamUp.Model;

public partial class TeamUpContext : DbContext
{
    public TeamUpContext()
    {
    }

    public TeamUpContext(DbContextOptions<TeamUpContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Activity> Activities { get; set; }

    public virtual DbSet<Challenge> Challenges { get; set; }

    public virtual DbSet<ChallengesType> ChallengesTypes { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<DifficultyLevel> DifficultyLevels { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<EventsComment> EventsComments { get; set; }

    public virtual DbSet<Rate> Rates { get; set; }

    public virtual DbSet<RatesEvent> RatesEvents { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UsersActivity> UsersActivities { get; set; }

    public virtual DbSet<UsersChallenge> UsersChallenges { get; set; }

    public virtual DbSet<UsersEvent> UsersEvents { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Activity>(entity =>
        {
            entity.HasKey(e => e.ActivityId).HasName("PK__ACTIVITI__393F5A45ADE78311");

            entity.ToTable("ACTIVITIES");

            entity.Property(e => e.ActivityId).HasColumnName("Activity_Id");
            entity.Property(e => e.ActivityName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("Activity_Name");
        });

        modelBuilder.Entity<Challenge>(entity =>
        {
            entity.HasKey(e => e.ChallengeId).HasName("PK__CHALLENG__F4E71D655B7EBFB3");

            entity.ToTable("CHALLENGES");

            entity.Property(e => e.ChallengeId).HasColumnName("Challenge_Id");
            entity.Property(e => e.ActivityId).HasColumnName("Activity_Id");
            entity.Property(e => e.ChallengeDescription)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("Challenge_Description");
            entity.Property(e => e.ChallengeName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Challenge_Name");
            entity.Property(e => e.ChallengeTypeId)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("Challenge_Type_Id");

            entity.HasOne(d => d.Activity).WithMany(p => p.Challenges)
                .HasForeignKey(d => d.ActivityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CHALLENGE__Activ__59063A47");

            entity.HasOne(d => d.ChallengeType).WithMany(p => p.Challenges)
                .HasForeignKey(d => d.ChallengeTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CHALLENGE__Chall__5812160E");
        });

        modelBuilder.Entity<ChallengesType>(entity =>
        {
            entity.HasKey(e => e.ChallengeTypeId).HasName("PK__CHALLENG__B16202691094782D");

            entity.ToTable("CHALLENGES_TYPES");

            entity.Property(e => e.ChallengeTypeId)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("Challenge_Type_Id");
            entity.Property(e => e.ChallengeTypeDescription)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Challenge_Type_Description");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.CountryId).HasName("PK__COUNTRIE__8036CBAE2CAFF49B");

            entity.ToTable("COUNTRIES");

            entity.Property(e => e.CountryId).HasColumnName("Country_Id");
            entity.Property(e => e.CountryName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("Country_Name");
        });

        modelBuilder.Entity<DifficultyLevel>(entity =>
        {
            entity.HasKey(e => e.DifficultyLevelId).HasName("PK__DIFFICUL__CC44802DD4A44F0F");

            entity.ToTable("DIFFICULTY_LEVELS");

            entity.Property(e => e.DifficultyLevelId).HasColumnName("Difficulty_Level_Id");
            entity.Property(e => e.DifficultyName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Difficulty_Name");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.EventId).HasName("PK__EVENTS__FD6BEF84780DC207");

            entity.ToTable("EVENTS");

            entity.Property(e => e.EventId).HasColumnName("Event_Id");
            entity.Property(e => e.ActivityId).HasColumnName("Activity_Id");
            entity.Property(e => e.City)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.CountryId).HasColumnName("Country_Id");
            entity.Property(e => e.DifficultyLevelId).HasColumnName("Difficulty_Level_Id");
            entity.Property(e => e.EventCreateDateTime)
                .HasColumnType("datetime")
                .HasColumnName("Event_Create_Date_Time");
            entity.Property(e => e.EventDateTime)
                .HasColumnType("datetime")
                .HasColumnName("Event_Date_Time");
            entity.Property(e => e.EventDescription)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("Event_Description");
            entity.Property(e => e.EventName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Event_Name");
            entity.Property(e => e.UserCount).HasColumnName("User_Count");

            entity.HasOne(d => d.Activity).WithMany(p => p.Events)
                .HasForeignKey(d => d.ActivityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EVENTS__Activity__6477ECF3");

            entity.HasOne(d => d.Country).WithMany(p => p.Events)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("FK__EVENTS__Country___656C112C");

            entity.HasOne(d => d.DifficultyLevel).WithMany(p => p.Events)
                .HasForeignKey(d => d.DifficultyLevelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EVENTS__Difficul__6383C8BA");
        });

        modelBuilder.Entity<EventsComment>(entity =>
        {
            entity.HasKey(e => e.EventCommentId).HasName("PK__EVENTS_C__53A71BA5FECD3F41");

            entity.ToTable("EVENTS_COMMENTS");

            entity.Property(e => e.EventCommentId).HasColumnName("Event_Comment_Id");
            entity.Property(e => e.Comment)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.DateTime)
                .HasColumnType("datetime")
                .HasColumnName("Date_Time");
            entity.Property(e => e.EventId).HasColumnName("Event_Id");
            entity.Property(e => e.UserId).HasColumnName("User_Id");

            entity.HasOne(d => d.Event).WithMany(p => p.EventsComments)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("FK__EVENTS_CO__Event__71D1E811");

            entity.HasOne(d => d.User).WithMany(p => p.EventsComments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EVENTS_CO__User___72C60C4A");
        });

        modelBuilder.Entity<Rate>(entity =>
        {
            entity.HasKey(e => e.RateId).HasName("PK__RATES__30BADA32CE3015CD");

            entity.ToTable("RATES");

            entity.Property(e => e.RateId).HasColumnName("Rate_Id");
            entity.Property(e => e.RateDescription)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("Rate_Description");
            entity.Property(e => e.RateValue).HasColumnName("Rate_Value");
        });

        modelBuilder.Entity<RatesEvent>(entity =>
        {
            entity.HasKey(e => e.RateEventId).HasName("PK__RATES_EV__CDCBCE9FC9AE41DF");

            entity.ToTable("RATES_EVENTS");

            entity.Property(e => e.RateEventId).HasColumnName("Rate_Event_Id");
            entity.Property(e => e.EventId).HasColumnName("Event_Id");
            entity.Property(e => e.RateId).HasColumnName("Rate_Id");
            entity.Property(e => e.UserId).HasColumnName("User_Id");

            entity.HasOne(d => d.Event).WithMany(p => p.RatesEvents)
                .HasForeignKey(d => d.EventId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__RATES_EVE__Event__6D0D32F4");

            entity.HasOne(d => d.Rate).WithMany(p => p.RatesEvents)
                .HasForeignKey(d => d.RateId)
                .HasConstraintName("FK__RATES_EVE__Rate___6EF57B66");

            entity.HasOne(d => d.User).WithMany(p => p.RatesEvents)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__RATES_EVE__User___6E01572D");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RolId).HasName("PK__ROLES__795EBD49953897C8");

            entity.ToTable("ROLES");

            entity.Property(e => e.RolId).HasColumnName("Rol_Id");
            entity.Property(e => e.RolDescription)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Rol_Description");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__USERS__206D9170D9571E0B");

            entity.ToTable("USERS");

            entity.Property(e => e.UserId).HasColumnName("User_Id");
            entity.Property(e => e.Alias)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.DateOfBirth)
                .HasColumnType("date")
                .HasColumnName("Date_Of_Birth");
            entity.Property(e => e.Email)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TrainingLevel).HasColumnName("Training_Level");
            entity.Property(e => e.UserLastname)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("User_Lastname");
            entity.Property(e => e.UserName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("User_Name");

            entity.HasOne(d => d.TrainingLevelNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.TrainingLevel)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__USERS__Training___4D94879B");
        });

        modelBuilder.Entity<UsersActivity>(entity =>
        {
            entity.HasKey(e => e.UserActivityId).HasName("PK__USERS_AC__B54649CE4705F1F6");

            entity.ToTable("USERS_ACTIVITIES");

            entity.Property(e => e.UserActivityId).HasColumnName("User_Activity_Id");
            entity.Property(e => e.ActivityId).HasColumnName("Activity_Id");
            entity.Property(e => e.UserId).HasColumnName("User_Id");

            entity.HasOne(d => d.Activity).WithMany(p => p.UsersActivities)
                .HasForeignKey(d => d.ActivityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__USERS_ACT__Activ__52593CB8");

            entity.HasOne(d => d.User).WithMany(p => p.UsersActivities)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__USERS_ACT__User___534D60F1");
        });

        modelBuilder.Entity<UsersChallenge>(entity =>
        {
            entity.HasKey(e => e.UserChallengeId).HasName("PK__USERS_CH__748D3040718C784F");

            entity.ToTable("USERS_CHALLENGES");

            entity.Property(e => e.UserChallengeId).HasColumnName("User_Challenge_Id");
            entity.Property(e => e.ChallengeId).HasColumnName("Challenge_Id");
            entity.Property(e => e.UserId).HasColumnName("User_Id");

            entity.HasOne(d => d.Challenge).WithMany(p => p.UsersChallenges)
                .HasForeignKey(d => d.ChallengeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__USERS_CHA__Chall__5CD6CB2B");

            entity.HasOne(d => d.User).WithMany(p => p.UsersChallenges)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__USERS_CHA__User___5BE2A6F2");
        });

        modelBuilder.Entity<UsersEvent>(entity =>
        {
            entity.HasKey(e => e.UserEventId).HasName("PK__USERS_EV__4663C06304C0F7A7");

            entity.ToTable("USERS_EVENTS");

            entity.Property(e => e.UserEventId).HasColumnName("User_Event_Id");
            entity.Property(e => e.EventId).HasColumnName("Event_Id");
            entity.Property(e => e.RolId).HasColumnName("Rol_Id");
            entity.Property(e => e.UserId).HasColumnName("User_Id");

            entity.HasOne(d => d.Event).WithMany(p => p.UsersEvents)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("FK__USERS_EVE__Event__693CA210");

            entity.HasOne(d => d.Rol).WithMany(p => p.UsersEvents)
                .HasForeignKey(d => d.RolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__USERS_EVE__Rol_I__68487DD7");

            entity.HasOne(d => d.User).WithMany(p => p.UsersEvents)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__USERS_EVE__User___6A30C649");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
