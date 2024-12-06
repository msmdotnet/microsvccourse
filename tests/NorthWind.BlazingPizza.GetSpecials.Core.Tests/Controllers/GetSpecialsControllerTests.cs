namespace NorthWind.BlazingPizza.GetSpecials.Core.Tests.Controllers;
public class GetSpecialsControllerTests
{
    [Fact]
    public async Task GetSpecialsAsync_ShouldInvokeInputPortAndReturnPizzaSpecials()
    {
        // Arrange
        var InputPort = Substitute.For<IGetSpecialsInputPort>();
        var OutputPort = Substitute.For<IGetSpecialsOutputPort>();

        var ExpectedSpecials = new List<PizzaSpecialDto>
        {
            new PizzaSpecialDto(3, "s3", 30, "d3", "i3"),
            new PizzaSpecialDto(2, "s2", 20, "d2", "i2"),
            new PizzaSpecialDto(1, "s1", 10, "d1", "i1"),
        };

        OutputPort.PizzaSpecials.Returns(ExpectedSpecials);
        InputPort.GetSpecialsAsync().Returns(Task.CompletedTask);

        var Controller = new GetSpecialsController(InputPort, OutputPort);

        // Act
        var ReturnedSpecials = await Controller.GetSpecialsAsync();

        // Assert
        await InputPort.Received(1).GetSpecialsAsync();
        Assert.Equal(ExpectedSpecials, ReturnedSpecials);
    }
}
