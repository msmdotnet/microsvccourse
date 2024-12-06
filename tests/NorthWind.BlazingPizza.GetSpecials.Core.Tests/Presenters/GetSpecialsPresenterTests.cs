namespace NorthWind.BlazingPizza.GetSpecials.Core.Tests.Presenters;
public class GetSpecialsPresenterTests
{
    [Fact]
    public async Task HandlerResultAsync_Should_Set_PizzaSpecials()
    {
        // Arrange
        IOptions<GetSpecialsOptions> GetSpecialsOptions =
            Options.Create<GetSpecialsOptions>(
                new GetSpecialsOptions()
                {
                    ImageUrlBase = "https://test"
                });

        var Presenter = new GetSpecialsPresenter(GetSpecialsOptions);

        var Specials = new List<PizzaSpecialDto>
        {
            new PizzaSpecialDto(3, "s3", 30, "d3", "i3"),
            new PizzaSpecialDto(2, "s2", 20, "d2", "i2"),
            new PizzaSpecialDto(1, "s1", 10, "d1", "i1"),
        };

        // Act
        await Presenter.HandleResultAsync(Specials);

        // Assert
        Assert.Equal(Specials.Count, Presenter.PizzaSpecials.Count());

        for (int i = 0; i < Specials.Count; i++)
        {
            var ExpectedImageUrl =
                GetSpecialsOptions.Value.ImageUrlBase + "/" +
                Specials[i].ImageUrl;

            Assert.Equal(ExpectedImageUrl,
                Presenter.PizzaSpecials.ElementAt(i).ImageUrl);
        }
    }
}
