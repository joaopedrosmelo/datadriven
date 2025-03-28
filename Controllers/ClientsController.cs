using DataDrivenApi.Dtos;
using DataDrivenApi.Features.Clients.Commands;
using DataDrivenApi.Features.Clients.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DataDrivenApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ClientsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetAllClientsQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var query = new GetClientQuery(id);
        var result = await _mediator.Send(query);
        return result != null ? Ok(result) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateClientDto dto)
    {
        var command = new CreateClientCommand(dto);
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateClientDto dto)
    {
        var command = new UpdateClientCommand(id, dto);
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteClientCommand(id);
        await _mediator.Send(command);
        return NoContent();
    }
}