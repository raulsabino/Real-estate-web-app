using System;
using Microsoft.EntityFrameworkCore;
using RealEstateWebApp.Api.Entities;

namespace RealEstateWebApp.Api.Data;

public class RealEstateContext : DbContext
{
    public RealEstateContext(DbContextOptions<RealEstateContext> options) : base(options)
    {
    }

    public DbSet<Listing> Listings => Set<Listing>();
    public DbSet<AdditionalProperty> AdditionalProperties => Set<AdditionalProperty>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<AdditionalProperty>()
            .HasOne(ap => ap.Listing)
            .WithMany(l => l.AdditionalProperties)
            .HasForeignKey(ap => ap.ListingId)
            .IsRequired();

        modelBuilder.Entity<Listing>().HasData(
            new Listing
            {
                Id = 1,
                Title = "Modern Family Home",
                Description = "A beautiful family home with a garden and pool.",
                Address = "123 Maple Street",
                City = "Springfield",
                State = "Illinois",
                Neighborhood = "Downtown",
                Price = 450000.00M,
                HOA = 200.00M,
                PropertyTaxes = 3000.00M,
                AreaInSquareMeters = 200.5,
                Bedrooms = 4,
                Bathrooms = 3,
                LivingRoom = 1,
                ParkingSpaces = 2,
                PropertyType = "House",
                ListingDate = new DateTime(2023, 1, 1)
            },
            new Listing
            {
                Id = 2,
                Title = "Spacious Land Lot",
                Description = "A 5-acre land lot perfect for development.",
                Address = "789 Greenfield Rd",
                City = "Smallville",
                State = "Kansas",
                Price = 150000.00M,
                AreaInSquareMeters = 20000.0,
                PropertyType = "Land",
                ListingDate = new DateTime(2023, 1, 1)
            }
        );

        modelBuilder.Entity<AdditionalProperty>().HasData(
            new AdditionalProperty
            {
                Id = 1,
                ListingId = 1,
                Key = "Year Built",
                Value = "2020"
            },
            new AdditionalProperty
            {
                Id = 2,
                ListingId = 1,
                Key = "Pool Type",
                Value = "In-ground"
            },
            new AdditionalProperty
            {
                Id = 3,
                ListingId = 2,
                Key = "Zoning",
                Value = "Residential"
            }
        );
    }
}
