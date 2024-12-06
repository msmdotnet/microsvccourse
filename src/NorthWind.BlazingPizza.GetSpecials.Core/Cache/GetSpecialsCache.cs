using System.Text.Json;

namespace NorthWind.BlazingPizza.GetSpecials.Core.Cache;
internal class GetSpecialsCache(
    IDistributedCache cache,
    ILogger<GetSpecialsCache> logger) : IGetSpecialsCache
{
    const string CacheKey = "pizzaSpecials";
    public async Task<IEnumerable<PizzaSpecialDto>> GetSpecialsAsync()
    {
        IEnumerable<PizzaSpecialDto> Specials = null;

        try
        {
            string SpecialsJson = await cache.GetStringAsync(CacheKey);
            if (!string.IsNullOrEmpty(SpecialsJson))
            {
                Specials = JsonSerializer
                    .Deserialize<IEnumerable<PizzaSpecialDto>>(SpecialsJson);
                logger.LogInformation("Get Specials from cache");
            }
        }
        catch(Exception ex)
        {
            logger.LogError(ex.Message);
        }

        return Specials;
    }

    public async Task SetSpecialsAsync(IEnumerable<PizzaSpecialDto> pizzas)
    {
        try
        {
            string SpecialsJson = JsonSerializer.Serialize(pizzas);
            await cache.SetStringAsync(CacheKey, SpecialsJson);
            logger.LogInformation("Set Specials to cache");
        }
        catch(Exception ex)
        {
            logger.LogError(ex.Message);
        }
    }
}
