﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Vehicles.Persistence.Context;

#nullable disable

namespace Vehicles.Persistence.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.32")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Vehicles.Core.Domain.Mark", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("marks", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Name = "Audi"
                        },
                        new
                        {
                            Id = 2L,
                            Name = "Mercedes"
                        },
                        new
                        {
                            Id = 3L,
                            Name = "BMW"
                        },
                        new
                        {
                            Id = 4L,
                            Name = "Peugeot"
                        },
                        new
                        {
                            Id = 5L,
                            Name = "Volkswagen"
                        },
                        new
                        {
                            Id = 6L,
                            Name = "Citroën"
                        },
                        new
                        {
                            Id = 7L,
                            Name = "Renault"
                        },
                        new
                        {
                            Id = 8L,
                            Name = "Volvo"
                        },
                        new
                        {
                            Id = 9L,
                            Name = "Fiat"
                        });
                });

            modelBuilder.Entity("Vehicles.Core.Domain.Model", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("MarkId")
                        .HasColumnType("bigint")
                        .HasColumnName("mark_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.HasIndex("MarkId");

                    b.ToTable("models", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            MarkId = 1L,
                            Name = "A3"
                        },
                        new
                        {
                            Id = 2L,
                            MarkId = 2L,
                            Name = "Classe A"
                        },
                        new
                        {
                            Id = 3L,
                            MarkId = 3L,
                            Name = "Serie 1"
                        },
                        new
                        {
                            Id = 4L,
                            MarkId = 4L,
                            Name = "308"
                        },
                        new
                        {
                            Id = 5L,
                            MarkId = 5L,
                            Name = "Golf"
                        },
                        new
                        {
                            Id = 6L,
                            MarkId = 6L,
                            Name = "C4"
                        },
                        new
                        {
                            Id = 7L,
                            MarkId = 7L,
                            Name = "Megane"
                        },
                        new
                        {
                            Id = 8L,
                            MarkId = 8L,
                            Name = "V40"
                        },
                        new
                        {
                            Id = 9L,
                            MarkId = 9L,
                            Name = "Punto"
                        });
                });

            modelBuilder.Entity("Vehicles.Core.Domain.Vehicle", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Color")
                        .HasColumnType("text")
                        .HasColumnName("color");

                    b.Property<int>("Doors")
                        .HasColumnType("integer")
                        .HasColumnName("doors");

                    b.Property<int>("EngineSize")
                        .HasColumnType("integer")
                        .HasColumnName("engine_size");

                    b.Property<int>("FuelType")
                        .HasColumnType("integer")
                        .HasColumnName("fuel_type");

                    b.Property<int>("Mileage")
                        .HasColumnType("integer")
                        .HasColumnName("mileage");

                    b.Property<long>("ModelId")
                        .HasColumnType("bigint")
                        .HasColumnName("model_id");

                    b.Property<string>("Observations")
                        .HasColumnType("text")
                        .HasColumnName("observations");

                    b.Property<bool>("Opportunity")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false)
                        .HasColumnName("opportunity");

                    b.Property<int>("Power")
                        .HasColumnType("integer")
                        .HasColumnName("power");

                    b.Property<double>("Price")
                        .HasColumnType("double precision")
                        .HasColumnName("price");

                    b.Property<bool>("Sold")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false)
                        .HasColumnName("sold");

                    b.Property<DateTime?>("SoldDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("sold_date");

                    b.Property<int>("Transmission")
                        .HasColumnType("integer")
                        .HasColumnName("transmission");

                    b.Property<string>("Version")
                        .HasColumnType("text")
                        .HasColumnName("version");

                    b.Property<int>("Year")
                        .HasColumnType("integer")
                        .HasColumnName("year");

                    b.HasKey("Id");

                    b.HasIndex("ModelId");

                    b.ToTable("vehicles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Color = "Azul",
                            Doors = 5,
                            EngineSize = 1999,
                            FuelType = 2,
                            Mileage = 2000,
                            ModelId = 1L,
                            Observations = "Garantia de 2 anos",
                            Opportunity = true,
                            Power = 140,
                            Price = 40000.0,
                            Sold = false,
                            Transmission = 1,
                            Version = "Sportline",
                            Year = 2022
                        },
                        new
                        {
                            Id = 2L,
                            Color = "Cinza",
                            Doors = 5,
                            EngineSize = 1999,
                            FuelType = 3,
                            Mileage = 7000,
                            ModelId = 2L,
                            Observations = "Garantia de 2 anos",
                            Opportunity = true,
                            Power = 140,
                            Price = 27000.0,
                            Sold = false,
                            Transmission = 2,
                            Version = "AMG",
                            Year = 2021
                        },
                        new
                        {
                            Id = 3L,
                            Color = "Vermelho",
                            Doors = 5,
                            EngineSize = 1999,
                            FuelType = 1,
                            Mileage = 0,
                            ModelId = 3L,
                            Observations = "Garantia de 2 anos",
                            Opportunity = true,
                            Power = 140,
                            Price = 29000.0,
                            Sold = false,
                            Transmission = 2,
                            Version = "Sport",
                            Year = 2023
                        },
                        new
                        {
                            Id = 4L,
                            Color = "Verde",
                            Doors = 5,
                            EngineSize = 1999,
                            FuelType = 1,
                            Mileage = 10000,
                            ModelId = 4L,
                            Observations = "Garantia de 2 anos",
                            Opportunity = false,
                            Power = 140,
                            Price = 18000.0,
                            Sold = false,
                            Transmission = 1,
                            Version = "GTI",
                            Year = 2022
                        });
                });

            modelBuilder.Entity("Vehicles.Core.Domain.VehicleImage", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<bool>("IsMain")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false)
                        .HasColumnName("is_main");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("url");

                    b.Property<long>("VehicleId")
                        .HasColumnType("bigint")
                        .HasColumnName("vehicleId");

                    b.HasKey("Id");

                    b.HasIndex("VehicleId");

                    b.ToTable("vehicle_images", (string)null);
                });

            modelBuilder.Entity("Vehicles.Core.Domain.Model", b =>
                {
                    b.HasOne("Vehicles.Core.Domain.Mark", "Mark")
                        .WithMany("Models")
                        .HasForeignKey("MarkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Mark");
                });

            modelBuilder.Entity("Vehicles.Core.Domain.Vehicle", b =>
                {
                    b.HasOne("Vehicles.Core.Domain.Model", "Model")
                        .WithMany("Vehicles")
                        .HasForeignKey("ModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Model");
                });

            modelBuilder.Entity("Vehicles.Core.Domain.VehicleImage", b =>
                {
                    b.HasOne("Vehicles.Core.Domain.Vehicle", "Vehicle")
                        .WithMany("VehicleImages")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("Vehicles.Core.Domain.Mark", b =>
                {
                    b.Navigation("Models");
                });

            modelBuilder.Entity("Vehicles.Core.Domain.Model", b =>
                {
                    b.Navigation("Vehicles");
                });

            modelBuilder.Entity("Vehicles.Core.Domain.Vehicle", b =>
                {
                    b.Navigation("VehicleImages");
                });
#pragma warning restore 612, 618
        }
    }
}
