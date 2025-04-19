using WebApplication1;

namespace sqlCopyExample.Tests;
public class ProgramTests
{
    [Fact]
    public async Task Application_Should_Start_In_Test_Mode()
    {
        // Arrange
        var args = new[] { "test" };

        // Act
        var app = Program.Main(args);

        // Assert
        Assert.NotNull(app);
    }
}
