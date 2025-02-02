using Microsoft.EntityFrameworkCore;
using RealEstateWebApp.Api.Data;
using RealEstateWebApp.Api.Dtos;
using RealEstateWebApp.Api.Entities;
using RealEstateWebApp.Api.Mapping;

namespace RealEstateWebApp.Api.Endpoints;

public static class ListingsEndpoints
{
    const string GetListingEndPointName = "GetListing";

    public static RouteGroupBuilder MapListingsEndpoints(this WebApplication app) {
        var group = app.MapGroup("listings").WithParameterValidation();

        // GET /listings
        group.MapGet("/", async (RealEstateContext dbContext) => await dbContext.Listings.Include(l => l.AdditionalProperties).Select(listing => listing.ToListingDto()).ToListAsync());

        // GET /listings/1
        group.MapGet("/{id}", async (int id, RealEstateContext dbContext) => { 
            Listing? listing = await dbContext.Listings.Include(l => l.AdditionalProperties).FirstOrDefaultAsync(l => l.Id == id);

            return listing is null ? Results.NotFound() : Results.Ok(listing.ToListingDto());
        }).WithName(GetListingEndPointName);

        // POST /listings
        group.MapPost("/", async (CreateListingDto newListing, RealEstateContext dbContext) => {
            
            Listing listing = newListing.ToEntity();

            if (newListing.AdditionalProperties is not null)
            {
                listing.AdditionalProperties = newListing.AdditionalProperties
                    .Select(ap => new AdditionalProperty
                    {
                        Key = ap.Key,
                        Value = ap.Value,
                        Listing = listing
                    }).ToList();
            }

            dbContext.Listings.Add(listing);
            await dbContext.SaveChangesAsync();

            return Results.CreatedAtRoute(GetListingEndPointName, new { id = listing.Id }, listing.ToListingDto());
        });

        // PUT /listings
        group.MapPut("/{id}", async (int id, UpdateListingDto updatedListing, RealEstateContext dbContext) => {
            var existingListing = await dbContext.Listings.Include(l => l.AdditionalProperties).FirstOrDefaultAsync(l => l.Id == id);

            if (existingListing is null)
                return Results.NotFound();

            dbContext.Entry(existingListing).CurrentValues.SetValues(updatedListing);

            existingListing.AdditionalProperties.Clear();
            if (updatedListing.AdditionalProperties is not null)
            {
                foreach (var ap in updatedListing.AdditionalProperties)
                {
                    existingListing.AdditionalProperties.Add(new AdditionalProperty
                    {
                        Key = ap.Key,
                        Value = ap.Value,
                        ListingId = existingListing.Id
                    });
                }
            }

            await dbContext.SaveChangesAsync();
            return Results.NoContent();
        });

        // DELETE /listings/1
        group.MapDelete("/{id}", async (int id, RealEstateContext dbContext) => {
            await dbContext.Listings.Where(listing => listing.Id == id).ExecuteDeleteAsync();

            return Results.NoContent();
        });

        return group;
    }
}
