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
        group.MapGet("/", (RealEstateContext dbContext) => dbContext.Listings.Select(listing => listing.ToListingDto()).ToList());

        // GET /listings/1
        group.MapGet("/{id}", (int id, RealEstateContext dbContext) => { 
            Listing? listing = dbContext.Listings.Find(id);

            return listing is null ? Results.NotFound() : Results.Ok(listing);
        }).WithName(GetListingEndPointName);

        // POST /listings
        group.MapPost("/", (CreateListingDto newListing, RealEstateContext dbContext) => {
            
            Listing listing = newListing.ToEntity();

            dbContext.Listings.Add(listing);
            dbContext.SaveChanges();

            return Results.CreatedAtRoute(GetListingEndPointName, new { id = listing.Id }, listing);
        });

        // PUT /listings
        group.MapPut("/{id}", (int id, UpdateListingDto updatedListing, RealEstateContext dbContext) => {
            var existingListing = dbContext.Listings.Find(id);

            if(existingListing is null) {
                return Results.NotFound();
            }

            dbContext.Entry(existingListing).CurrentValues.SetValues(updatedListing.ToEntity(id));

            dbContext.SaveChanges();

            return Results.NoContent();
        });

        // DELETE /listings/1
        group.MapDelete("/{id}", (int id, RealEstateContext dbContext) => {
            dbContext.Listings.Where(listing => listing.Id == id).ExecuteDelete();

            return Results.NoContent();
        });

        return group;
    }
}
