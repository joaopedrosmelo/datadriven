using DataDrivenApi.Dtos;
using DataDrivenApi.Models;
using DataDrivenApi.Repositories;
using MediatR;

namespace DataDrivenApi.Features.Clients.Queries;

public record GetClientQuery(int Id) : IRequest<ClientDto?>;

public class GetClientQueryHandler : IRequestHandler<GetClientQuery, ClientDto?>
{
    private readonly IClientRepository _repository;

    public GetClientQueryHandler(IClientRepository repository)
    {
        _repository = repository;
    }

    public async Task<ClientDto?> Handle(GetClientQuery request, CancellationToken cancellationToken)
    {
        var client = await _repository.GetByIdAsync(request.Id);
        return client != null ? MapToDto(client) : null;
    }

    private static ClientDto MapToDto(Client client) =>
        new(client.Id, client.Name, client.Email, client.DateOfBirth, client.CreatedAt, client.UpdatedAt);
}