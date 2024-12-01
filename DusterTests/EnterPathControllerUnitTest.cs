using Duster;
using Duster.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Tests;

public class EnterPathControllerUnitTest
{
    private readonly EnterPathController _enterPathController;
    private readonly Mock<IEnterPathService> _enterPathServiceMock;

    public EnterPathControllerUnitTest()
    {
        _enterPathServiceMock = new Mock<IEnterPathService>();
        _enterPathController = new EnterPathController(_enterPathServiceMock.Object);
    }
    
    [Fact]
    public async Task ExecuteEnterPath_ShouldReturnExecution_WhenServiceReturnsExecution()
    {
        // Arrange
        var input = new EnterPath {
            Start = new Coordinate { X = 0, Y = 0 }, 
            Commands = new Commands()
        };
        var expectedExecution = new Execution { Id = 1 };

        _enterPathServiceMock.Setup(s => s.ExecuteEnterPath(input))
            .ReturnsAsync(expectedExecution);

        // Act
        var result = await _enterPathController.ExecuteEnterPath(input);

        // Assert
        var actionResult = Assert.IsType<ActionResult<Execution>>(result);
        var execution = Assert.IsType<Execution>(actionResult.Value);
        Assert.Equal(expectedExecution.Id, execution.Id);
    }
    
    [Fact]
    public async Task ExecuteEnterPath_ShouldReturn500_OnException()
    {
        // Arrange
        var mockService = new Mock<IEnterPathService>();
        var controller = new EnterPathController(mockService.Object);
        var input = new EnterPath {
            Start = new Coordinate { X = 0, Y = 0 },
            Commands = new Commands()
        };
        mockService.Setup(s => s.ExecuteEnterPath(input))
            .ThrowsAsync(new Exception("Service error"));

        // Act
        var result = await controller.ExecuteEnterPath(input);

        // Assert
        var actionResult = result.Result as ObjectResult;
        Assert.NotNull(actionResult);
        Assert.Equal(500, actionResult.StatusCode);

        var errorResponse = actionResult.Value as ErrorResponse;
        Assert.NotNull(errorResponse);
        Assert.Equal("An unexpected error occurred.", errorResponse.Message);
        Assert.NotNull(errorResponse.Details);
    }
}