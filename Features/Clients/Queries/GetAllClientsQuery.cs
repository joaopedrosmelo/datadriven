using DataDrivenApi.Dtos;
using DataDrivenApi.Models;
using DataDrivenApi.Repositories;
using MediatR;

namespace DataDrivenApi.Features.Clients.Queries;

public record GetAllClientsQuery : IRequest<IEnumerable<ClientDto>>;

public class GetAllClientsQueryHandler : IRequestHandler<GetAllClientsQuery, IEnumerable<ClientDto>>
{
    private readonly IClientRepository _repository;

    public GetAllClientsQueryHandler(IClientRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ClientDto>> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
    {
        var clients = await _repository.GetAllAsync();
        return clients.Select(MapToDto);
    }

    private static ClientDto MapToDto(Client client) =>
        new(client.Id, client.Name, client.Email, client.DateOfBirth, client.CreatedAt, client.UpdatedAt);
}