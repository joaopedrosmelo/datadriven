using DataDrivenApi.Dtos;
using DataDrivenApi.Exceptions;
using DataDrivenApi.Models;
using DataDrivenApi.Repositories;
using MediatR;

namespace DataDrivenApi.Features.Clients.Commands;

public record UpdateClientCommand(int Id, UpdateClientDto Dto) : IRequest<Unit>;

public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, Unit>
{
    private readonly IClientRepository _repository;

    public UpdateClientCommandHandler(IClientRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
    {
        var client = await _repository.GetByIdAsync(request.Id);
        if (client == null)
        {
            throw new NotFoundException($"Client with ID {request.Id} not found");
        }

        client.Name = request.Dto.Name;
        client.Email = request.Dto.Email;
        client.DateOfBirth = request.Dto.DateOfBirth;

        await _repository.UpdateAsync(client);
        return Unit.Value;
    }
}