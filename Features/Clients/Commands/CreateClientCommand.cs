using DataDrivenApi.Dtos;
using DataDrivenApi.Models;
using DataDrivenApi.Repositories;
using MediatR;

namespace DataDrivenApi.Features.Clients.Commands;

public record CreateClientCommand(CreateClientDto Dto) : IRequest<ClientDto>;

public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, ClientDto>
{
    private readonly IClientRepository _repository;

    public CreateClientCommandHandler(IClientRepository repository)
    {
        _repository = repository;
    }

    public async Task<ClientDto> Handle(CreateClientCommand request, CancellationToken cancellationToken)
    {
        var client = new Client
        {
            Name = request.Dto.Name,
            Email = request.Dto.Email,
            DateOfBirth = request.Dto.DateOfBirth
        };

        var createdClient = await _repository.AddAsync(client);
        return new ClientDto(
            createdClient.Id,
            createdClient.Name,
            createdClient.Email,
            createdClient.DateOfBirth,
            createdClient.CreatedAt,
            createdClient.UpdatedAt);
    }
}