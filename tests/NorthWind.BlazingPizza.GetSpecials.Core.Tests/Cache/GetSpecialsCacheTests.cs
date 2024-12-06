namespace NorthWind.BlazingPizza.GetSpecials.Core.Tests.Cache;

public class GetSpecialsCacheTests
{
    [Fact]
    public async Task SetSpecialsAsync_Should_Save_And_GetSpecialsAsync_Should_Return_Same_Value_From_Cache()
    {
        // Arrange
        var Specials = new List<PizzaSpecialDto>
        {
            new PizzaSpecialDto(3, "s3", 30, "d3", "i3"),
            new PizzaSpecialDto(2, "s2", 20, "d2", "i2"),
            new PizzaSpecialDto(1, "s1", 10, "d1", "i1"),
        };

        var CacheOptions = Options.Create(new MemoryDistributedCacheOptions());
        IDistributedCache Cache = new MemoryDistributedCache(CacheOptions);

        ILogger<GetSpecialsCache> Logger = new NullLogger<GetSpecialsCache>();

        var GetSpecialsCache = new GetSpecialsCache(Cache, Logger);

        // Act
        await GetSpecialsCache.SetSpecialsAsync(Specials);
        var Result = await GetSpecialsCache.GetSpecialsAsync();

        // Assert
        Assert.Equal(Specials.Count, Result.Count());

        var Pairs = Specials.Zip(Result, (expected, actual) =>
           new { expected, actual });

        Assert.All(Pairs, pair =>
            Assert.True(
                pair.expected.Id == pair.actual.Id &&
                pair.expected.Name == pair.actual.Name &&
                pair.expected.BasePrice == pair.actual.BasePrice &&
                pair.expected.Description == pair.actual.Description &&
                pair.expected.ImageUrl == pair.actual.ImageUrl
                ));
    }
}
