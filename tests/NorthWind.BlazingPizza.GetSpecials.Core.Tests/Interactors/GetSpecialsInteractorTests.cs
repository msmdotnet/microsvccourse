namespace NorthWind.BlazingPizza.GetSpecials.Core.Tests.Interactors;
public class GetSpecialsInteractorTests
{
    [Fact]
    public async Task GetSpecialsAsync__ShouldInvokeHandleResultAsync_WithPizzaSpecials()
    {
        // Arrange
        var Repository = Substitute.For<IGetSpecialsRepository>();
        var Presenter = Substitute.For<IGetSpecialsOutputPort>();
        var Cache = Substitute.For<IGetSpecialsCache>();

        var Interactor = new GetSpecialsInteractor(Repository, Presenter, Cache);

        var ExpectedSpecials = new List<PizzaSpecialDto>
        {
            new PizzaSpecialDto(3, "s3", 30, "d3", "i3"),
            new PizzaSpecialDto(2, "s2", 20, "d2", "i2"),
            new PizzaSpecialDto(1, "s1", 10, "d1", "i1"),
        };

        Cache.GetSpecialsAsync().Returns(ExpectedSpecials);

        Repository.GetSpecialsSortedByDescendingPriceAsync()
            .Returns(ExpectedSpecials);

        // Act
        await Interactor.GetSpecialsAsync();

        // Assert
        await Presenter.Received(1)
            .HandleResultAsync(Arg.Is<IEnumerable<PizzaSpecialDto>>(specials =>
            specials == ExpectedSpecials));
    }

    [Fact]
    public async Task GetSpecialsAsync_Should_GetFromCache_When_CacheHasData()
    {
        // Arrange
        var ExpectedSpecials = new List<PizzaSpecialDto>
        {
            new PizzaSpecialDto(3, "s3", 30, "d3", "i3"),
            new PizzaSpecialDto(2, "s2", 20, "d2", "i2"),
            new PizzaSpecialDto(1, "s1", 10, "d1", "i1"),
        };

        var Repository = Substitute.For<IGetSpecialsRepository>();
        var Presenter = Substitute.For<IGetSpecialsOutputPort>();
        var Cache = Substitute.For<IGetSpecialsCache>();

        Cache.GetSpecialsAsync().Returns(ExpectedSpecials);

        var Interactor = new GetSpecialsInteractor(Repository, Presenter, Cache);

        // Act
        await Interactor.GetSpecialsAsync();

        // Assert
        await Cache.Received(1).GetSpecialsAsync();
        await Repository.DidNotReceive()
            .GetSpecialsSortedByDescendingPriceAsync();
    }

    [Fact]
    public async Task GetSpecialsAsync_Should_GetFromRepository_When_CacheIsEmpty()
    {
        // Arrange
        var ExpectedSpecials = new List<PizzaSpecialDto>
        {
            new PizzaSpecialDto(3, "s3", 30, "d3", "i3"),
            new PizzaSpecialDto(2, "s2", 20, "d2", "i2"),
            new PizzaSpecialDto(1, "s1", 10, "d1", "i1"),
        };

        var Repository = Substitute.For<IGetSpecialsRepository>();
        var Presenter = Substitute.For<IGetSpecialsOutputPort>();
        var Cache = Substitute.For<IGetSpecialsCache>();

        Cache.GetSpecialsAsync().Returns((IEnumerable<PizzaSpecialDto>)null);
        Repository.GetSpecialsSortedByDescendingPriceAsync()
            .Returns(ExpectedSpecials);

        var Interactor = new GetSpecialsInteractor(Repository, Presenter, Cache);

        // Act
        await Interactor.GetSpecialsAsync();

        // Assert
        await Cache.Received(1).GetSpecialsAsync();
        await Repository.Received(1).GetSpecialsSortedByDescendingPriceAsync();
        await Cache.Received(1).SetSpecialsAsync(ExpectedSpecials);
    }
}
