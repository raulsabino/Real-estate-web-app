using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateWebApp.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Listings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    City = table.Column<string>(type: "TEXT", nullable: false),
                    State = table.Column<string>(type: "TEXT", nullable: false),
                    Neighborhood = table.Column<string>(type: "TEXT", nullable: true),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    HOA = table.Column<decimal>(type: "TEXT", nullable: true),
                    PropertyTaxes = table.Column<decimal>(type: "TEXT", nullable: true),
                    AreaInSquareMeters = table.Column<double>(type: "REAL", nullable: false),
                    Bedrooms = table.Column<int>(type: "INTEGER", nullable: true),
                    Bathrooms = table.Column<int>(type: "INTEGER", nullable: true),
                    LivingRoom = table.Column<int>(type: "INTEGER", nullable: true),
                    ParkingSpaces = table.Column<int>(type: "INTEGER", nullable: true),
                    PropertyType = table.Column<string>(type: "TEXT", nullable: false),
                    ListingDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Images = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Listings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AdditionalProperties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ListingId = table.Column<int>(type: "INTEGER", nullable: false),
                    Key = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalProperties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdditionalProperties_Listings_ListingId",
                        column: x => x.ListingId,
                        principalTable: "Listings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalProperties_ListingId",
                table: "AdditionalProperties",
                column: "ListingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdditionalProperties");

            migrationBuilder.DropTable(
                name: "Listings");
        }
    }
}
