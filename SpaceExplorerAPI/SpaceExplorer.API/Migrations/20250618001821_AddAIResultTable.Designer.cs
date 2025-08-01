﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SpaceExplorer.API.Data;

#nullable disable

namespace SpaceExplorer.API.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250618001821_AddAIResultTable")]
    partial class AddAIResultTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.6");

            modelBuilder.Entity("SpaceExplorer.API.Models.AIResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Atmoshpere")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double?>("BioScore")
                        .HasColumnType("REAL");

                    b.Property<int>("PlanetID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PlanetType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double?>("WaterLikelihood")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("PlanetID")
                        .IsUnique();

                    b.ToTable("AIResults");
                });

            modelBuilder.Entity("SpaceExplorer.API.Models.Planet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("DiscoveryMethod")
                        .HasColumnType("TEXT");

                    b.Property<int?>("DiscoveryYear")
                        .HasColumnType("INTEGER");

                    b.Property<double?>("DistanceFromEarth")
                        .HasColumnType("REAL");

                    b.Property<double?>("Mass")
                        .HasColumnType("REAL");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<double?>("OrbitalPeriod")
                        .HasColumnType("REAL");

                    b.Property<double?>("Radius")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("Planets");
                });

            modelBuilder.Entity("SpaceExplorer.API.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SpaceExplorer.API.Models.AIResult", b =>
                {
                    b.HasOne("SpaceExplorer.API.Models.Planet", "Planet")
                        .WithOne("AIResult")
                        .HasForeignKey("SpaceExplorer.API.Models.AIResult", "PlanetID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Planet");
                });

            modelBuilder.Entity("SpaceExplorer.API.Models.Planet", b =>
                {
                    b.Navigation("AIResult")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
