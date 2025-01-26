using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

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

            migrationBuilder.InsertData(
                table: "Listings",
                columns: new[] { "Id", "Address", "AreaInSquareMeters", "Bathrooms", "Bedrooms", "City", "Description", "HOA", "Images", "ListingDate", "LivingRoom", "Neighborhood", "ParkingSpaces", "Price", "PropertyTaxes", "PropertyType", "State", "Title" },
                values: new object[,]
                {
                    { 1, "123 Maple Street", 200.5, 3, 4, "Springfield", "A beautiful family home with a garden and pool.", 200.00m, "[]", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Downtown", 2, 450000.00m, 3000.00m, "House", "Illinois", "Modern Family Home" },
                    { 2, "789 Greenfield Rd", 20000.0, null, null, "Smallville", "A 5-acre land lot perfect for development.", null, "[]", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 150000.00m, null, "Land", "Kansas", "Spacious Land Lot" }
                });

            migrationBuilder.InsertData(
                table: "AdditionalProperties",
                columns: new[] { "Id", "Key", "ListingId", "Value" },
                values: new object[,]
                {
                    { 1, "Year Built", 1, "2020" },
                    { 2, "Pool Type", 1, "In-ground" },
                    { 3, "Zoning", 2, "Residential" }
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
