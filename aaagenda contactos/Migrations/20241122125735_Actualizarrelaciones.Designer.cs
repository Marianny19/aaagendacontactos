﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace aaagenda_contactos.Migrations
{
    [DbContext(typeof(MiDbContext))]
    [Migration("20241122125735_Actualizarrelaciones")]
    partial class Actualizarrelaciones
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MiDbContext+contacto", b =>
                {
                    b.Property<int>("ID_contacto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID_contacto"));

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Tipo_Contacto")
                        .HasColumnType("integer");

                    b.Property<int>("Tipo_red_social")
                        .HasColumnType("integer");

                    b.HasKey("ID_contacto");

                    b.HasIndex("Tipo_Contacto");

                    b.HasIndex("Tipo_red_social");

                    b.ToTable("contactos");
                });

            modelBuilder.Entity("agenda", b =>
                {
                    b.Property<int>("ID_agenda")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID_agenda"));

                    b.Property<string>("Descripcion_agenda")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Fecha_agendada")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("ID_contacto")
                        .HasColumnType("integer");

                    b.Property<string>("Nombre_agenda")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ID_agenda");

                    b.ToTable("agendas");
                });

            modelBuilder.Entity("red_social", b =>
                {
                    b.Property<int>("Id_red_social")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id_red_social"));

                    b.Property<int>("ID_contacto")
                        .HasColumnType("integer");

                    b.Property<string>("Nombre_de_usuario")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id_red_social");

                    b.ToTable("red_sociall");
                });

            modelBuilder.Entity("teléfono", b =>
                {
                    b.Property<int>("Id_telefono")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id_telefono"));

                    b.Property<int>("Id_contacto")
                        .HasColumnType("integer");

                    b.Property<string>("Número_de_teléfono")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Tipo_teléfono")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id_telefono");

                    b.ToTable("telefono");
                });

            modelBuilder.Entity("tipo_contacto", b =>
                {
                    b.Property<int>("ID_tipo_contacto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID_tipo_contacto"));

                    b.Property<string>("Nombre_tipo_contacto")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ID_tipo_contacto");

                    b.ToTable("tipos_contacto");
                });

            modelBuilder.Entity("tipo_red_social", b =>
                {
                    b.Property<int>("Id_tipo_red_social")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id_tipo_red_social"));

                    b.Property<string>("Nombre_red_social")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id_tipo_red_social");

                    b.ToTable("tipos_red_social");
                });

            modelBuilder.Entity("MiDbContext+contacto", b =>
                {
                    b.HasOne("tipo_contacto", "TipoContacto")
                        .WithMany()
                        .HasForeignKey("Tipo_Contacto")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("tipo_red_social", "TipoRedSocial")
                        .WithMany()
                        .HasForeignKey("Tipo_red_social")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TipoContacto");

                    b.Navigation("TipoRedSocial");
                });
#pragma warning restore 612, 618
        }
    }
}