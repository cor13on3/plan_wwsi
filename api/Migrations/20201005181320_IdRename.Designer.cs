﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Test2.Data;

namespace Test2.Migrations
{
    [DbContext(typeof(PlanContext))]
    [Migration("20201005181320_IdRename")]
    partial class IdRename
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Test2.Entities.Grupa", b =>
                {
                    b.Property<string>("NrGrupy")
                        .HasColumnType("text");

                    b.Property<int>("Semestr")
                        .HasColumnType("integer");

                    b.Property<int>("StopienStudiow")
                        .HasColumnType("integer");

                    b.Property<int>("TrybStudiow")
                        .HasColumnType("integer");

                    b.HasKey("NrGrupy");

                    b.ToTable("Grupa");
                });

            modelBuilder.Entity("Test2.Entities.GrupaZjazd", b =>
                {
                    b.Property<int>("NrZjazdu")
                        .HasColumnType("integer");

                    b.Property<int>("IdZjazdu")
                        .HasColumnType("integer");

                    b.Property<string>("NrGrupy")
                        .HasColumnType("text");

                    b.Property<bool>("CzyZdalnie")
                        .HasColumnType("boolean");

                    b.HasKey("NrZjazdu", "IdZjazdu", "NrGrupy");

                    b.HasIndex("IdZjazdu");

                    b.HasIndex("NrGrupy");

                    b.ToTable("GrupaZjazd");
                });

            modelBuilder.Entity("Test2.Entities.Lekcja", b =>
                {
                    b.Property<int>("IdLekcji")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("GodzinaDo")
                        .HasColumnType("text");

                    b.Property<string>("GodzinaOd")
                        .HasColumnType("text");

                    b.Property<int>("IdPrzedmiotu")
                        .HasColumnType("integer");

                    b.Property<int>("IdSali")
                        .HasColumnType("integer");

                    b.Property<int>("IdWykladowcy")
                        .HasColumnType("integer");

                    b.HasKey("IdLekcji");

                    b.HasIndex("IdPrzedmiotu");

                    b.HasIndex("IdSali");

                    b.HasIndex("IdWykladowcy");

                    b.ToTable("Lekcja");
                });

            modelBuilder.Entity("Test2.Entities.LekcjaGrupa", b =>
                {
                    b.Property<int>("IdLekcji")
                        .HasColumnType("integer");

                    b.Property<string>("NrGrupy")
                        .HasColumnType("text");

                    b.Property<bool>("CzyOdpracowanie")
                        .HasColumnType("boolean");

                    b.Property<int>("DzienTygodnia")
                        .HasColumnType("integer");

                    b.Property<int>("NrZjazdu")
                        .HasColumnType("integer");

                    b.HasKey("IdLekcji", "NrGrupy");

                    b.HasIndex("NrGrupy");

                    b.ToTable("LekcjaGrupa");
                });

            modelBuilder.Entity("Test2.Entities.Przedmiot", b =>
                {
                    b.Property<int>("IdPrzedmiotu")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Forma")
                        .HasColumnType("integer");

                    b.Property<string>("Nazwa")
                        .HasColumnType("text");

                    b.HasKey("IdPrzedmiotu");

                    b.ToTable("Przedmiot");
                });

            modelBuilder.Entity("Test2.Entities.Sala", b =>
                {
                    b.Property<int>("IdSali")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Nazwa")
                        .HasColumnType("text");

                    b.Property<int>("Rodzaj")
                        .HasColumnType("integer");

                    b.HasKey("IdSali");

                    b.ToTable("Sala");
                });

            modelBuilder.Entity("Test2.Entities.Specjalnosc", b =>
                {
                    b.Property<int>("IdSpecjalnosci")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Nazwa")
                        .HasColumnType("text");

                    b.HasKey("IdSpecjalnosci");

                    b.ToTable("Specjalnosc");
                });

            modelBuilder.Entity("Test2.Entities.WyklSpec", b =>
                {
                    b.Property<int>("IdWykladowcy")
                        .HasColumnType("integer");

                    b.Property<int>("IdSpecjalnosci")
                        .HasColumnType("integer");

                    b.HasKey("IdWykladowcy", "IdSpecjalnosci");

                    b.HasIndex("IdSpecjalnosci");

                    b.ToTable("WyklSpec");
                });

            modelBuilder.Entity("Test2.Entities.Wykladowca", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Imie")
                        .HasColumnType("text");

                    b.Property<string>("Nazwisko")
                        .HasColumnType("text");

                    b.Property<string>("Tytul")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Wykladowca");
                });

            modelBuilder.Entity("Test2.Entities.Zjazd", b =>
                {
                    b.Property<int>("IdZjazdu")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("DataDo")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("DataOd")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("RodzajSemestru")
                        .HasColumnType("integer");

                    b.HasKey("IdZjazdu");

                    b.ToTable("Zjazd");
                });

            modelBuilder.Entity("Test2.Entities.GrupaZjazd", b =>
                {
                    b.HasOne("Test2.Entities.Zjazd", "Zjazd")
                        .WithMany("GrupaZjazdList")
                        .HasForeignKey("IdZjazdu")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Test2.Entities.Grupa", "Grupa")
                        .WithMany("GrupaZjazdList")
                        .HasForeignKey("NrGrupy")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Test2.Entities.Lekcja", b =>
                {
                    b.HasOne("Test2.Entities.Przedmiot", "Przedmiot")
                        .WithMany("LekcjaList")
                        .HasForeignKey("IdPrzedmiotu")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Test2.Entities.Sala", "Sala")
                        .WithMany("LekcjaList")
                        .HasForeignKey("IdSali")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Test2.Entities.Wykladowca", "Wykladowca")
                        .WithMany("LekcjaList")
                        .HasForeignKey("IdWykladowcy")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Test2.Entities.LekcjaGrupa", b =>
                {
                    b.HasOne("Test2.Entities.Lekcja", "Lekcja")
                        .WithMany("LekcjaGrupaList")
                        .HasForeignKey("IdLekcji")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Test2.Entities.Grupa", "Grupa")
                        .WithMany("LekcjaGrupaList")
                        .HasForeignKey("NrGrupy")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Test2.Entities.WyklSpec", b =>
                {
                    b.HasOne("Test2.Entities.Specjalnosc", "Specjalnosc")
                        .WithMany("WyklSpecList")
                        .HasForeignKey("IdSpecjalnosci")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Test2.Entities.Wykladowca", "Wykladowca")
                        .WithMany("WyklSpecList")
                        .HasForeignKey("IdWykladowcy")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
