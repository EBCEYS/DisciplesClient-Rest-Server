﻿// <auto-generated />
using System;
using Disciples2ClientDataBaseLibrary.DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DisciplesClient_Update_Service.Migrations
{
    [DbContext(typeof(Disciples2ClientDBConnext))]
    [Migration("20230114181745_AddSoftware")]
    partial class AddSoftware
    {
        /// <inheritdoc/>
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Disciples2ClientDataBaseModels.DBModels.Mod", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("AuthorUserId")
                        .HasColumnType("integer");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("FirstUpdateDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset>("LastUpdateDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Name");

                    b.HasIndex("AuthorUserId");

                    b.ToTable("Mods");
                });

            modelBuilder.Entity("Disciples2ClientDataBaseModels.DBModels.Software", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("AuthorUserId")
                        .HasColumnType("integer");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("FirstUpdateDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset>("LastUpdateDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Name");

                    b.HasIndex("AuthorUserId");

                    b.ToTable("Software");
                });

            modelBuilder.Entity("Disciples2ClientDataBaseModels.DBModels.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string[]>("Roles")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<string>("UserName")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Disciples2ClientDataBaseModels.DBModels.Mod", b =>
                {
                    b.HasOne("Disciples2ClientDataBaseModels.DBModels.User", "Author")
                        .WithMany("Mods")
                        .HasForeignKey("AuthorUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("Disciples2ClientDataBaseModels.DBModels.Software", b =>
                {
                    b.HasOne("Disciples2ClientDataBaseModels.DBModels.User", "Author")
                        .WithMany("Softwares")
                        .HasForeignKey("AuthorUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("Disciples2ClientDataBaseModels.DBModels.User", b =>
                {
                    b.Navigation("Mods");

                    b.Navigation("Softwares");
                });
#pragma warning restore 612, 618
        }
    }
}