using Duster.Models;
using Microsoft.EntityFrameworkCore;

namespace Duster;

public interface IExecutionRepository
{
    Task<Execution> InsertExecutionAsync(Execution execution);
    Task<List<Execution>> GetExecutionsAsync();
}

public class ExecutionRepository(ExecutionDbContext context) : IExecutionRepository
{
    public async Task<Execution> InsertExecutionAsync(Execution execution)
    {
        await context.Set<Execution>().AddAsync(execution);
        await context.SaveChangesAsync();
        return execution;
    }

    public async Task<List<Execution>> GetExecutionsAsync()
    {
        return await context.Set<Execution>().ToListAsync();
    }
}