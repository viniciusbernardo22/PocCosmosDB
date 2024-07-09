using PocCosmosDb.BlazorServer.DTOs;
using PocCosmosDb.BlazorServer.Models;

namespace PocCosmosDb.BlazorServer.Services;

public interface IEnginnerService
{
    Task<List<Engineer>> GetEnginnersAsync();
    Task AddEnginnerAsync(Engineer enginner);
} 