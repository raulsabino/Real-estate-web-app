using System;
using RealEstateWebApp.Api.Dtos;
using RealEstateWebApp.Api.Entities;

namespace RealEstateWebApp.Api.Mapping;

public static class ListingMapping
{
    public static Listing ToEntity(this CreateListingDto listing) {
        return new Listing()
        {
            Title = listing.Title,
            Description = listing.Description,
            Address = listing.Address,
            City = listing.City,
            State = listing.State,
            Neighborhood = listing.Neighborhood,
            Price = listing.Price,
            HOA = listing.HOA,
            PropertyTaxes = listing.PropertyTaxes,
            AreaInSquareMeters = listing.AreaInSquareMeters,
            Bedrooms = listing.Bedrooms,
            Bathrooms = listing.Bathrooms,
            LivingRoom = listing.LivingRoom,
            ParkingSpaces = listing.ParkingSpaces,
            PropertyType = listing.PropertyType,
            Images = listing.Images,
            ListingDate = DateTime.Now
        };
    }

    public static ListingDto ToListingDto(this Listing listing)
    {
        return new(
            listing.Id,
            listing.Title,
            listing.Description,
            listing.Address,
            listing.City,
            listing.State,
            listing.Neighborhood,
            listing.Price,
            listing.HOA,
            listing.PropertyTaxes,
            listing.AreaInSquareMeters,
            listing.Bedrooms,
            listing.Bathrooms,
            listing.LivingRoom,
            listing.ParkingSpaces,
            listing.PropertyType,
            listing.Images,
            DateTime.Now
        );
    }

    public static Listing ToEntity(this UpdateListingDto listing, int id) {
        return new Listing()
        {
            Id = id,
            Title = listing.Title,
            Description = listing.Description,
            Address = listing.Address,
            City = listing.City,
            State = listing.State,
            Neighborhood = listing.Neighborhood,
            Price = listing.Price,
            HOA = listing.HOA,
            PropertyTaxes = listing.PropertyTaxes,
            AreaInSquareMeters = listing.AreaInSquareMeters,
            Bedrooms = listing.Bedrooms,
            Bathrooms = listing.Bathrooms,
            LivingRoom = listing.LivingRoom,
            ParkingSpaces = listing.ParkingSpaces,
            PropertyType = listing.PropertyType,
            Images = listing.Images,
            ListingDate = DateTime.Now
        };
    }
}
