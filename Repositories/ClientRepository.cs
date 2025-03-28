using DataDrivenApi.Data;
using DataDrivenApi.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace DataDrivenApi.Repositories;

public interface IClientRepository
{
    Task<Client?> GetByIdAsync(int id);
    Task<IEnumerable<Client>> GetAllAsync();
    Task<Client> AddAsync(Client client);
    Task UpdateAsync(Client client);
    Task DeleteAsync(int id);
}

public class ClientRepository : IClientRepository
{
    private readonly AppDbContext _context;

    public ClientRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Client?> GetByIdAsync(int id)
    {
        return await _context.Clients.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Client>> GetAllAsync()
    {
        return await _context.Clients.AsNoTracking().ToListAsync();
    }

    public async Task<Client> AddAsync(Client client)
    {
        await _context.Clients.AddAsync(client);
        await _context.SaveChangesAsync();
        return client;
    }

    public async Task UpdateAsync(Client client)
    {
        client.UpdatedAt = DateTime.UtcNow;
        _context.Clients.Update(client);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var client = await _context.Clients.FindAsync(id);
        if (client != null)
        {
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
        }
    }
}