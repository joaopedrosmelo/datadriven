using DataDrivenApi.Data;
using DataDrivenApi.Dtos;
using DataDrivenApi.Features.Clients.Commands;
using DataDrivenApi.Features.Clients.Queries;
using DataDrivenApi.Models;
using DataDrivenApi.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace DataDrivenApi.Tests;

public class ClientTests
{
    private readonly DbContextOptions<AppDbContext> _dbContextOptions;

    public ClientTests()
    {
        _dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
    }

    [Fact]
    public async Task GetClientQuery_ShouldReturnClient_WhenClientExists()
    {
        // Arrange
        using var context = new AppDbContext(_dbContextOptions);
        var repository = new ClientRepository(context);

        var client = new Client { Name = "Test", Email = "test@test.com", DateOfBirth = DateTime.Now.AddYears(-30) };
        await repository.AddAsync(client);

        var handler = new GetClientQueryHandler(repository);
        var query = new GetClientQuery(client.Id);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result!.Id.Should().Be(client.Id);
    }

    [Fact]
    public async Task CreateClientCommand_ShouldCreateNewClient()
    {
        // Arrange
        using var context = new AppDbContext(_dbContextOptions);
        var repository = new ClientRepository(context);

        var handler = new CreateClientCommandHandler(repository);
        var command = new CreateClientCommand(
            new CreateClientDto("Test", "test@test.com", DateTime.Now.AddYears(-25)));

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().BeGreaterThan(0);
        var dbClient = await repository.GetByIdAsync(result.Id);
        dbClient.Should().NotBeNull();
    }
}