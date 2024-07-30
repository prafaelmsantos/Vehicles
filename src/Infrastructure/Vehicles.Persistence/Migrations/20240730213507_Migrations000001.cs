using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Vehicles.Persistence.Migrations
{
    public partial class Migrations000001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "marks",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_marks", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "models",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    mark_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_models", x => x.id);
                    table.ForeignKey(
                        name: "FK_models_marks_mark_id",
                        column: x => x.mark_id,
                        principalTable: "marks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "vehicles",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    model_id = table.Column<long>(type: "bigint", nullable: false),
                    version = table.Column<string>(type: "text", nullable: true),
                    fuel_type = table.Column<int>(type: "integer", nullable: false),
                    price = table.Column<double>(type: "double precision", nullable: false),
                    mileage = table.Column<int>(type: "integer", nullable: false),
                    year = table.Column<int>(type: "integer", nullable: false),
                    color = table.Column<string>(type: "text", nullable: true),
                    doors = table.Column<int>(type: "integer", nullable: false),
                    transmission = table.Column<int>(type: "integer", nullable: false),
                    engine_size = table.Column<int>(type: "integer", nullable: false),
                    power = table.Column<int>(type: "integer", nullable: false),
                    observations = table.Column<string>(type: "text", nullable: true),
                    opportunity = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    sold = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    sold_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vehicles", x => x.id);
                    table.ForeignKey(
                        name: "FK_vehicles_models_model_id",
                        column: x => x.model_id,
                        principalTable: "models",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "vehicle_images",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    url = table.Column<string>(type: "text", nullable: false),
                    vehicleId = table.Column<long>(type: "bigint", nullable: false),
                    is_main = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vehicle_images", x => x.id);
                    table.ForeignKey(
                        name: "FK_vehicle_images_vehicles_vehicleId",
                        column: x => x.vehicleId,
                        principalTable: "vehicles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "marks",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1L, "Audi" },
                    { 2L, "Mercedes" },
                    { 3L, "BMW" },
                    { 4L, "Peugeot" },
                    { 5L, "Volkswagen" },
                    { 6L, "Citroën" },
                    { 7L, "Renault" },
                    { 8L, "Volvo" },
                    { 9L, "Fiat" }
                });

            migrationBuilder.InsertData(
                table: "models",
                columns: new[] { "id", "mark_id", "name" },
                values: new object[,]
                {
                    { 1L, 1L, "A3" },
                    { 2L, 2L, "Classe A" },
                    { 3L, 3L, "Serie 1" },
                    { 4L, 4L, "308" },
                    { 5L, 5L, "Golf" },
                    { 6L, 6L, "C4" },
                    { 7L, 7L, "Megane" },
                    { 8L, 8L, "V40" },
                    { 9L, 9L, "Punto" }
                });

            migrationBuilder.InsertData(
                table: "vehicles",
                columns: new[] { "id", "color", "doors", "engine_size", "fuel_type", "mileage", "model_id", "observations", "opportunity", "power", "price", "sold_date", "transmission", "version", "year" },
                values: new object[,]
                {
                    { 1L, "Azul", 5, 1999, 2, 2000, 1L, "Garantia de 2 anos", true, 140, 40000.0, null, 1, "Sportline", 2022 },
                    { 2L, "Cinza", 5, 1999, 3, 7000, 2L, "Garantia de 2 anos", true, 140, 27000.0, null, 2, "AMG", 2021 },
                    { 3L, "Vermelho", 5, 1999, 1, 0, 3L, "Garantia de 2 anos", true, 140, 29000.0, null, 2, "Sport", 2023 }
                });

            migrationBuilder.InsertData(
                table: "vehicles",
                columns: new[] { "id", "color", "doors", "engine_size", "fuel_type", "mileage", "model_id", "observations", "power", "price", "sold_date", "transmission", "version", "year" },
                values: new object[] { 4L, "Verde", 5, 1999, 1, 10000, 4L, "Garantia de 2 anos", 140, 18000.0, null, 1, "GTI", 2022 });

            migrationBuilder.CreateIndex(
                name: "IX_models_mark_id",
                table: "models",
                column: "mark_id");

            migrationBuilder.CreateIndex(
                name: "IX_vehicle_images_vehicleId",
                table: "vehicle_images",
                column: "vehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_vehicles_model_id",
                table: "vehicles",
                column: "model_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "vehicle_images");

            migrationBuilder.DropTable(
                name: "vehicles");

            migrationBuilder.DropTable(
                name: "models");

            migrationBuilder.DropTable(
                name: "marks");
        }
    }
}
