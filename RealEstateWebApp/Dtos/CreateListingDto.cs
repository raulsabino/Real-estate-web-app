namespace RealEstateWebApp.Dtos;

public record class CreateListingDto(
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
