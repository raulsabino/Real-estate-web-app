using System.ComponentModel.DataAnnotations;

namespace RealEstateWebApp.Api.Dtos;

public record class AdditionalPropertyDto(
    [Required] int Id,
    [Required] int ListingId,
    [Required] string Key,
    [Required] string Value
);
