using Duster;
using Duster.Models;
using Moq;

namespace Tests;

public class EnterPathServiceUnitTest
{
    private readonly IEnterPathService _enterPathService;
    private readonly Mock<IExecutionRepository> _executionRepositoryMock;

    public EnterPathServiceUnitTest()
    {
        _executionRepositoryMock = new Mock<IExecutionRepository>();
        _enterPathService = new EnterPathService(_executionRepositoryMock.Object);
    }

    [Fact]
    public async void TestExecuteEnterPath_Success()
    {
        // Arrange
        var enterPath = new EnterPath {
            Start = new Coordinate { X = 0, Y = 0 }, 
            Commands = new Commands()
        };
        
        var expectedExecution = new Execution
        {
        };

        _executionRepositoryMock.Setup(x => x.InsertExecutionAsync(It.IsAny<Execution>()))
            .ReturnsAsync(expectedExecution);

        // Act
        _ = await _enterPathService.ExecuteEnterPath(enterPath);

        // Assert
        _executionRepositoryMock.Verify(repo => repo.InsertExecutionAsync(It.IsAny<Execution>()), Times.Once);
    }

}