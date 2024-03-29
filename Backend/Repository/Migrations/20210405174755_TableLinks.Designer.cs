﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Repository;

namespace Repository.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20210405174755_TableLinks")]
    partial class TableLinks
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Models.Reservations.Models.Data.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("Day")
                        .HasColumnType("timestamp without time zone");

                    b.Property<TimeSpan>("End")
                        .HasColumnType("interval");

                    b.Property<int>("RestaurantId")
                        .HasColumnType("integer");

                    b.Property<TimeSpan>("Start")
                        .HasColumnType("interval");

                    b.Property<int>("TableId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantId");

                    b.HasIndex("TableId");

                    b.HasIndex("UserId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("Models.Restaurants.Models.Data.OpenHours", b =>
                {
                    b.Property<int>("WeekDay")
                        .HasColumnType("integer");

                    b.Property<int>("RestaurantId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Close")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("Open")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("WeekDay", "RestaurantId");

                    b.HasIndex("RestaurantId");

                    b.HasIndex("WeekDay", "RestaurantId");

                    b.ToTable("OpenHours");
                });

            modelBuilder.Entity("Models.Restaurants.Models.Data.PlanItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<double>("Height")
                        .HasColumnType("double precision");

                    b.Property<string>("Svg")
                        .HasColumnType("text");

                    b.Property<double>("Width")
                        .HasColumnType("double precision");

                    b.Property<double>("X")
                        .HasColumnType("double precision");

                    b.Property<double>("Y")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.ToTable("PlanItems");
                });

            modelBuilder.Entity("Models.Restaurants.Models.Data.Restaurant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<string>("City")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Restaurants");
                });

            modelBuilder.Entity("Models.Restaurants.Models.Data.RestaurantPlan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("RestaurantId")
                        .HasColumnType("integer");

                    b.Property<string>("WebSvg")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantId")
                        .IsUnique();

                    b.ToTable("RestaurantPlans");
                });

            modelBuilder.Entity("Models.Restaurants.Models.Data.TableLink", b =>
                {
                    b.Property<int>("FirstTableId")
                        .HasColumnType("integer");

                    b.Property<int>("SecondTableId")
                        .HasColumnType("integer");

                    b.HasKey("FirstTableId", "SecondTableId");

                    b.HasIndex("SecondTableId");

                    b.ToTable("TableLinks");
                });

            modelBuilder.Entity("Models.Users.Models.Dao.UserDao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.Property<string>("Salt")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "admin",
                            FirstName = "Administrator",
                            LastName = "Administrator",
                            Password = "GL2wAuCc9zf3Laur5fp+Q8dymRZIxrf3Vxa8DsyV8QAtLMFx",
                            Role = 2,
                            Salt = "GL2wAuCc9zf3Laur5fp+Qw=="
                        });
                });

            modelBuilder.Entity("Models.Restaurants.Models.Data.PlanTable", b =>
                {
                    b.HasBaseType("Models.Restaurants.Models.Data.PlanItem");

                    b.Property<int>("Number")
                        .HasColumnType("integer");

                    b.Property<int>("PlanId")
                        .HasColumnType("integer");

                    b.Property<int>("Seats")
                        .HasColumnType("integer");

                    b.HasIndex("PlanId");

                    b.ToTable("PlanTables");
                });

            modelBuilder.Entity("Models.Restaurants.Models.Data.PlanWall", b =>
                {
                    b.HasBaseType("Models.Restaurants.Models.Data.PlanItem");

                    b.Property<int>("PlanId")
                        .HasColumnType("integer");

                    b.HasIndex("PlanId");

                    b.ToTable("PlanWalls");
                });

            modelBuilder.Entity("Models.Reservations.Models.Data.Reservation", b =>
                {
                    b.HasOne("Models.Restaurants.Models.Data.Restaurant", "Restaurant")
                        .WithMany("Reservations")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Restaurants.Models.Data.PlanTable", "Table")
                        .WithMany("Reservations")
                        .HasForeignKey("TableId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Users.Models.Dao.UserDao", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Restaurant");

                    b.Navigation("Table");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Models.Restaurants.Models.Data.OpenHours", b =>
                {
                    b.HasOne("Models.Restaurants.Models.Data.Restaurant", "Restaurant")
                        .WithMany("Schedule")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("Models.Restaurants.Models.Data.Restaurant", b =>
                {
                    b.HasOne("Models.Users.Models.Dao.UserDao", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Models.Restaurants.Models.Data.RestaurantPlan", b =>
                {
                    b.HasOne("Models.Restaurants.Models.Data.Restaurant", "Restaurant")
                        .WithOne("RestaurantPlan")
                        .HasForeignKey("Models.Restaurants.Models.Data.RestaurantPlan", "RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("Models.Restaurants.Models.Data.TableLink", b =>
                {
                    b.HasOne("Models.Restaurants.Models.Data.PlanTable", "FirstTable")
                        .WithMany()
                        .HasForeignKey("FirstTableId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Restaurants.Models.Data.PlanTable", "SecondTable")
                        .WithMany()
                        .HasForeignKey("SecondTableId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FirstTable");

                    b.Navigation("SecondTable");
                });

            modelBuilder.Entity("Models.Restaurants.Models.Data.PlanTable", b =>
                {
                    b.HasOne("Models.Restaurants.Models.Data.PlanItem", null)
                        .WithOne()
                        .HasForeignKey("Models.Restaurants.Models.Data.PlanTable", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Restaurants.Models.Data.RestaurantPlan", "Plan")
                        .WithMany("Tables")
                        .HasForeignKey("PlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Plan");
                });

            modelBuilder.Entity("Models.Restaurants.Models.Data.PlanWall", b =>
                {
                    b.HasOne("Models.Restaurants.Models.Data.PlanItem", null)
                        .WithOne()
                        .HasForeignKey("Models.Restaurants.Models.Data.PlanWall", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Restaurants.Models.Data.RestaurantPlan", "Plan")
                        .WithMany("Walls")
                        .HasForeignKey("PlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Plan");
                });

            modelBuilder.Entity("Models.Restaurants.Models.Data.Restaurant", b =>
                {
                    b.Navigation("Reservations");

                    b.Navigation("RestaurantPlan");

                    b.Navigation("Schedule");
                });

            modelBuilder.Entity("Models.Restaurants.Models.Data.RestaurantPlan", b =>
                {
                    b.Navigation("Tables");

                    b.Navigation("Walls");
                });

            modelBuilder.Entity("Models.Restaurants.Models.Data.PlanTable", b =>
                {
                    b.Navigation("Reservations");
                });
#pragma warning restore 612, 618
        }
    }
}
