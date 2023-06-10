﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using jane.Models;

#nullable disable

namespace jane.Migrations
{
    [DbContext(typeof(HouseholdContext))]
    [Migration("20230610033540_CreateDevDb")]
    partial class CreateDevDb
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("utf8mb4_0900_ai_ci")
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.HasCharSet(modelBuilder, "utf8mb4");

            modelBuilder.Entity("jane.Models.DailyChore", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<ulong>("Active")
                        .HasColumnType("bit(1)")
                        .HasColumnName("active");

                    b.Property<string>("ChoreName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("chore_name");

                    b.Property<string>("Friday")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("varchar(25)")
                        .HasColumnName("friday");

                    b.Property<string>("Monday")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("varchar(25)")
                        .HasColumnName("monday");

                    b.Property<string>("Notes")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("notes");

                    b.Property<string>("Saturday")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("varchar(25)")
                        .HasColumnName("saturday");

                    b.Property<string>("Sunday")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("varchar(25)")
                        .HasColumnName("sunday");

                    b.Property<string>("Thursday")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("varchar(25)")
                        .HasColumnName("thursday");

                    b.Property<string>("Tuesday")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("varchar(25)")
                        .HasColumnName("tuesday");

                    b.Property<string>("Wednesday")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("varchar(25)")
                        .HasColumnName("wednesday");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.ToTable("daily_chore", (string)null);
                });

            modelBuilder.Entity("jane.Models.TransactionalChore", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<int>("ChoreId")
                        .HasColumnType("int")
                        .HasColumnName("chore_id");

                    b.Property<ulong>("Completed")
                        .HasColumnType("bit(1)")
                        .HasColumnName("completed");

                    b.Property<DateTime?>("CompletedDatetime")
                        .HasColumnType("datetime")
                        .HasColumnName("completed_datetime");

                    b.Property<DateOnly>("WeekOf")
                        .HasColumnType("date")
                        .HasColumnName("week_of");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.ToTable("transactional_chore", (string)null);
                });

            modelBuilder.Entity("jane.Models.WeeklyChore", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<ulong>("Active")
                        .HasColumnType("bit(1)")
                        .HasColumnName("active");

                    b.Property<string>("ChoreName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("chore_name");

                    b.Property<string>("Notes")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("notes");

                    b.Property<string>("WeekFour")
                        .HasMaxLength(25)
                        .HasColumnType("varchar(25)")
                        .HasColumnName("week_four");

                    b.Property<string>("WeekOne")
                        .HasMaxLength(25)
                        .HasColumnType("varchar(25)")
                        .HasColumnName("week_one");

                    b.Property<string>("WeekThree")
                        .HasMaxLength(25)
                        .HasColumnType("varchar(25)")
                        .HasColumnName("week_three");

                    b.Property<string>("WeekTwo")
                        .HasMaxLength(25)
                        .HasColumnType("varchar(25)")
                        .HasColumnName("week_two");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.ToTable("weekly_chore", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
