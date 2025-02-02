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

        modelBuilder.Entity<Listing>()
            .HasMany(l => l.AdditionalProperties)
            .WithOne(ap => ap.Listing)
            .HasForeignKey(ap => ap.ListingId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
