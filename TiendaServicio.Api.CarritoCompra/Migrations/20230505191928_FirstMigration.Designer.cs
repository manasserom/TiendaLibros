﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TiendaServicio.Api.CarritoCompra.Persistencia;

namespace TiendaServicio.Api.CarritoCompra.Migrations
{
    [DbContext(typeof(CarritoContexto))]
    [Migration("20230505191928_FirstMigration")]
    partial class FirstMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TiendaServicio.Api.CarritoCompra.Modelo.CarritoSesion", b =>
                {
                    b.Property<int>("CarritoSesionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.HasKey("CarritoSesionId");

                    b.ToTable("CarritoCompras");
                });

            modelBuilder.Entity("TiendaServicio.Api.CarritoCompra.Modelo.CarritoSesionDetalle", b =>
                {
                    b.Property<int>("CarritoSesionDetalleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CarritoSesionId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("ProductoSeleccionado")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CarritoSesionDetalleId");

                    b.HasIndex("CarritoSesionId");

                    b.ToTable("CarritoSesionDetalles");
                });

            modelBuilder.Entity("TiendaServicio.Api.CarritoCompra.Modelo.CarritoSesionDetalle", b =>
                {
                    b.HasOne("TiendaServicio.Api.CarritoCompra.Modelo.CarritoSesion", "CarritoSesion")
                        .WithMany("ListaDetalle")
                        .HasForeignKey("CarritoSesionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CarritoSesion");
                });

            modelBuilder.Entity("TiendaServicio.Api.CarritoCompra.Modelo.CarritoSesion", b =>
                {
                    b.Navigation("ListaDetalle");
                });
#pragma warning restore 612, 618
        }
    }
}
