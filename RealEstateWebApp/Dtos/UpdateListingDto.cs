namespace RealEstateWebApp.Dtos;

public record class UpdateListingDto(
    int Id,
    string Title,
    string Description,
    string Address,
    string City,
    string State,
    string Neighborhood,
    decimal Price,
    double AreaInSquareMeters,
    int Bedrooms,
    int Bathrooms,
    int ParkingSpaces,
    string PropertyType,
    DateTime ListingDate
);