namespace Ordering.Application.Dtos;

public record AddressDto(string FirstName, string LastName, string EmailAddress,string AddressLine,  string ShippingAddress, string Street, string City, string State, string Zip, string Country);