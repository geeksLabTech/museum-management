﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using museum_management.Data;

#nullable disable

namespace museum_management.Migrations
{
    [DbContext(typeof(MuseumManagementContext))]
    [Migration("20220613202153_MuseumRoom")]
    partial class MuseumRoom
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.5");

            modelBuilder.Entity("museum_management.Models.Artwork", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Author")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("EconomicValue")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("EntryDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("MuseumRoom")
                        .HasColumnType("TEXT");

                    b.Property<string>("Period")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Artwork", (string)null);
                });

            modelBuilder.Entity("museum_management.Models.LendingToMuseum", b =>
                {
                    b.Property<int>("ArtworkId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MuseumId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Amount")
                        .HasColumnType("INTEGER");

                    b.Property<int>("LendingState")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PeriodInDays")
                        .HasColumnType("INTEGER");

                    b.HasKey("ArtworkId", "MuseumId");

                    b.HasIndex("MuseumId");

                    b.ToTable("LendingToMuseum", (string)null);
                });

            modelBuilder.Entity("museum_management.Models.Museum", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Museum", (string)null);
                });

            modelBuilder.Entity("museum_management.Models.Restauration", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ArtworkId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("RestaurationType")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id", "ArtworkId");

                    b.HasIndex("ArtworkId");

                    b.ToTable("Restauration", (string)null);
                });

            modelBuilder.Entity("museum_management.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Role")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("museum_management.Models.Picture", b =>
                {
                    b.HasBaseType("museum_management.Models.Artwork");

                    b.Property<string>("Style")
                        .HasColumnType("TEXT");

                    b.Property<string>("Technique")
                        .HasColumnType("TEXT");

                    b.ToTable("Picture", (string)null);
                });

            modelBuilder.Entity("museum_management.Models.Sculpture", b =>
                {
                    b.HasBaseType("museum_management.Models.Artwork");

                    b.Property<string>("Material")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Style")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.ToTable("Sculpture", (string)null);
                });

            modelBuilder.Entity("museum_management.Models.LendingToMuseum", b =>
                {
                    b.HasOne("museum_management.Models.Artwork", "Artwork")
                        .WithMany()
                        .HasForeignKey("ArtworkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("museum_management.Models.Museum", "Museum")
                        .WithMany()
                        .HasForeignKey("MuseumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Artwork");

                    b.Navigation("Museum");
                });

            modelBuilder.Entity("museum_management.Models.Restauration", b =>
                {
                    b.HasOne("museum_management.Models.Artwork", "Artwork")
                        .WithMany()
                        .HasForeignKey("ArtworkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Artwork");
                });

            modelBuilder.Entity("museum_management.Models.Picture", b =>
                {
                    b.HasOne("museum_management.Models.Artwork", null)
                        .WithOne()
                        .HasForeignKey("museum_management.Models.Picture", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("museum_management.Models.Sculpture", b =>
                {
                    b.HasOne("museum_management.Models.Artwork", null)
                        .WithOne()
                        .HasForeignKey("museum_management.Models.Sculpture", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
