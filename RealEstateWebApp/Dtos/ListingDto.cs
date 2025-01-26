using System.ComponentModel.DataAnnotations;

namespace RealEstateWebApp.Api.Dtos;

public record class ListingDto(
    [Required] int Id,
    [Required] string Title,
    [Required] string Description,
    [Required] string Address,
    [Required] string City,
    [Required] string State,
    string? Neighborhood,
    [Required] decimal Price,
    decimal? HOA,
    decimal? PropertyTaxes,
    [Required] double AreaInSquareMeters,
    int? Bedrooms,
    int? Bathrooms,
    int? LivingRoom,
    int? ParkingSpaces,
    [Required] string PropertyType,
    ICollection<string>? Images,
    [Required] DateTime ListingDate
);