using Duster.Models;
using Microsoft.AspNetCore.Mvc;

namespace Duster;

[Route("tibber-developer-test/enter-path")]
[ApiController]
public class EnterPathController(IEnterPathService enterPathService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Execution>> ExecuteEnterPath([FromBody] EnterPath enterPath)
    {
        try
        {
            var execute = await enterPathService.ExecuteEnterPath(enterPath);
            return execute;
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ErrorResponse
            {
                Message = "An unexpected error occurred.",
                Details = ex.Message
            });
        }
    }
}