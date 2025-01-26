using System;

namespace RealEstateWebApp.Api.Entities;

public class AdditionalProperty
{
    public int Id { get; set; }

    public int ListingId { get; set; }
    public Listing Listing { get; set; }

    public required string Key { get; set; }

    public required string Value { get; set; }
}
