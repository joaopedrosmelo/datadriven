namespace DataDrivenApi.Dtos;

public record ClientDto(
    int Id,
    string Name,
    string Email,
    DateTime DateOfBirth,
    DateTime CreatedAt,
    DateTime? UpdatedAt);

public record CreateClientDto(
    string Name,
    string Email,
    DateTime DateOfBirth);

public record UpdateClientDto(
    string Name,
    string Email,
    DateTime DateOfBirth);