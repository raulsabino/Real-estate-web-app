using System;

namespace RealEstateWebApp.Api.Entities;

public class Listing
{
    public int Id { get; set; }

    public required string Title { get; set; }

    public required string Description { get; set; }

    public required string Address { get; set; }

    public required string City { get; set; }

    public required string State { get; set; }

    public string? Neighborhood { get; set; }

    public required decimal Price { get; set; }

    public decimal? HOA { get; set; }

    public decimal? PropertyTaxes { get; set; }

    public required double AreaInSquareMeters { get; set; }

    public int? Bedrooms { get; set; }

    public int? Bathrooms { get; set; }

    public int? LivingRoom { get; set; }

    public int? ParkingSpaces { get; set; }
    
    public required string PropertyType { get; set; }

    public required DateTime ListingDate { get; set; }

    public ICollection<string>? Images { get; set; } = new List<string>();

    public ICollection<AdditionalProperty> AdditionalProperties { get; set; } = new List<AdditionalProperty>();
}
