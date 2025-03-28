using DataDrivenApi.Exceptions;
using DataDrivenApi.Repositories;
using MediatR;

namespace DataDrivenApi.Features.Clients.Commands;

public record DeleteClientCommand(int Id) : IRequest<Unit>;

public class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand, Unit>
{
    private readonly IClientRepository _repository;

    public DeleteClientCommandHandler(IClientRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
    {
        var client = await _repository.GetByIdAsync(request.Id);
        if (client == null)
        {
            throw new NotFoundException($"Client with ID {request.Id} not found");
        }

        await _repository.DeleteAsync(request.Id);
        return Unit.Value;
    }
}