using RealEstateWebApp.Dtos;

namespace RealEstateWebApp.Endpoints;

public static class ListingsEndpoints
{
    const string GetListingEndPointName = "GetListing";

    private static readonly List<ListingDto> listings = [
        new ListingDto(
                    Id: 1,
                    Title: "Modern Family Home",
                    Description: "A beautiful family home with a large garden and pool.",
                    Address: "123 Maple Street",
                    City: "Springfield",
                    State: "Illinois",
                    Neighborhood: "Downtown",
                    Price: 350000.00M,
                    AreaInSquareMeters: 200.5,
                    Bedrooms: 4,
                    Bathrooms: 3,
                    ParkingSpaces: 2,
                    PropertyType: "House",
                    ListingDate: DateTime.Now
                ),
                new ListingDto(
                    Id: 2,
                    Title: "Luxury Apartment",
                    Description: "A stunning apartment with city views and modern amenities.",
                    Address: "456 Elm Avenue",
                    City: "Metropolis",
                    State: "New York",
                    Neighborhood: "Financial District",
                    Price: 850000.00M,
                    AreaInSquareMeters: 120.0,
                    Bedrooms: 3,
                    Bathrooms: 2,
                    ParkingSpaces: 1,
                    PropertyType: "Apartment",
                    ListingDate: DateTime.Now
                )
    ];

    public static RouteGroupBuilder MapListingsEndpoints(this WebApplication app) {
        var group = app.MapGroup("listings");

        // GET /listings
        group.MapGet("/", () => listings);

        // GET /listings/1
        group.MapGet("/{id}", (int id) => { 
            var listing = listings.Find(listing => listing.Id == id);
            return listing is null ? Results.NotFound() : Results.Ok(listing);
        }).WithName(GetListingEndPointName);

        // POST /listings
        group.MapPost("/", (CreateListingDto newListing) => {
            
            var listing = new ListingDto(
                listings.Count + 1,
                newListing.Title,
                newListing.Description,
                newListing.Address,
                newListing.City,
                newListing.State,
                newListing.Neighborhood,
                newListing.Price,
                newListing.AreaInSquareMeters,
                newListing.Bedrooms,
                newListing.Bathrooms,
                newListing.ParkingSpaces,
                newListing.PropertyType,
                DateTime.Now
            );

            listings.Add(listing);

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
                updatedListing.AreaInSquareMeters,
                updatedListing.Bedrooms,
                updatedListing.Bathrooms,
                updatedListing.ParkingSpaces,
                updatedListing.PropertyType,
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
