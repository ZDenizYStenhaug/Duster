using Duster.Models;

namespace Duster;

public interface IEnterPathService
{
    public Task<Execution> ExecuteEnterPath(EnterPath enterPath);
}

public class EnterPathService(IExecutionRepository executionRepository) : IEnterPathService
{
    public async Task<Execution> ExecuteEnterPath(EnterPath enterPath)
    {
        var startTime = DateTime.Now;
        int result = enterPath.ExecuteCommands();
        var endTime = DateTime.Now;
        Execution execution = new Execution
        {
            Commands = enterPath.Commands.Count,
            Result = result,
            Duration = endTime.Subtract(startTime).TotalSeconds
        };
        return await executionRepository.InsertExecutionAsync(execution);
    }
    
}