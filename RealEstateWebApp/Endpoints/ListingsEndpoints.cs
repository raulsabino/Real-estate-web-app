using RealEstateWebApp.Api.Data;
using RealEstateWebApp.Api.Dtos;
using RealEstateWebApp.Api.Entities;

namespace RealEstateWebApp.Api.Endpoints;

public static class ListingsEndpoints
{
    const string GetListingEndPointName = "GetListing";

    private static readonly List<ListingDto> listings = [
        new ListingDto(
                    Id: 1,
                    Title: "Modern Family Home",
                    Description: "A beautiful family home with a garden and pool.",
                    Address: "123 Maple Street",
                    City: "Springfield",
                    State: "Illinois",
                    Neighborhood: "Downtown",
                    Price: 450000.00M,
                    HOA: 200.00M,
                    PropertyTaxes: 3000.00M,
                    AreaInSquareMeters: 200.5,
                    Bedrooms: 4,
                    Bathrooms: 3,
                    LivingRoom: 1,
                    ParkingSpaces: 2,
                    PropertyType: "House",
                    Images: new List<string>
                    {
                        "https://example.com/image1.jpg",
                        "https://example.com/image2.jpg"
                    },
                    ListingDate: DateTime.Now
                ),
                new ListingDto(
                    Id: 2,
                    Title: "Spacious Land Lot",
                    Description: "A 5-acre land lot perfect for development.",
                    Address: "789 Greenfield Rd",
                    City: "Smallville",
                    State: "Kansas",
                    Neighborhood: null,
                    Price: 150000.00M,
                    HOA: null,
                    PropertyTaxes: null,
                    AreaInSquareMeters: 20000.0,
                    Bedrooms: null,  // Not applicable for land
                    Bathrooms: null, // Not applicable for land
                    LivingRoom: null,
                    ParkingSpaces: null,
                    PropertyType: "Land",
                    Images: new List<string>
                    {
                        "https://example.com/image1.jpg",
                        "https://example.com/image2.jpg"
                    },
                    ListingDate: DateTime.Now
                )
    ];

    public static RouteGroupBuilder MapListingsEndpoints(this WebApplication app) {
        var group = app.MapGroup("listings").WithParameterValidation();

        // GET /listings
        group.MapGet("/", () => listings);

        // GET /listings/1
        group.MapGet("/{id}", (int id) => { 
            var listing = listings.Find(listing => listing.Id == id);
            return listing is null ? Results.NotFound() : Results.Ok(listing);
        }).WithName(GetListingEndPointName);

        // POST /listings
        group.MapPost("/", (CreateListingDto newListing, RealEstateContext dbContext) => {
            
            Listing listing = new()
            {
                Title = newListing.Title,
                Description = newListing.Description,
                Address = newListing.Address,
                City = newListing.City,
                State = newListing.State,
                Neighborhood = newListing.Neighborhood,
                Price = newListing.Price,
                HOA = newListing.HOA,
                PropertyTaxes = newListing.PropertyTaxes,
                AreaInSquareMeters = newListing.AreaInSquareMeters,
                Bedrooms = newListing.Bedrooms,
                Bathrooms = newListing.Bathrooms,
                LivingRoom = newListing.LivingRoom,
                ParkingSpaces = newListing.ParkingSpaces,
                PropertyType = newListing.PropertyType,
                Images = newListing.Images,
                ListingDate = DateTime.Now
            };

            dbContext.Listings.Add(listing);
            dbContext.SaveChanges();

            return Results.CreatedAtRoute(GetListingEndPointName, new { id = listing.Id }, listing);
        });

        // PUT /listings
        group.MapPut("/{id}", (int id, UpdateListingDto updatedListing) => {
            var index = listings.FindIndex(listing => listing.Id == id);

            if(index == -1) {
                return Results.NotFound();
            }

            var listing = new ListingDto(
                index,
                updatedListing.Title,
                updatedListing.Description,
                updatedListing.Address,
                updatedListing.City,
                updatedListing.State,
                updatedListing.Neighborhood,
                updatedListing.Price,
                updatedListing.HOA,
                updatedListing.PropertyTaxes,
                updatedListing.AreaInSquareMeters,
                updatedListing.Bedrooms,
                updatedListing.Bathrooms,
                updatedListing.LivingRoom,
                updatedListing.ParkingSpaces,
                updatedListing.PropertyType,
                updatedListing.Images,
                DateTime.Now
            );

            return Results.NoContent();
        });

        // DELETE /listings/1
        group.MapDelete("/{id}", (int id) => {
            listings.RemoveAll(game => game.Id == id);

            return Results.NoContent();
        });

        return group;
    }
}
