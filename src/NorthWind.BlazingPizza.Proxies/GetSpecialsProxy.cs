namespace NorthWind.BlazingPizza.Proxies;
public class GetSpecialsProxy(HttpClient client,
    ILogger<GetSpecialsProxy> logger)
{
    public async Task<IEnumerable<PizzaSpecialDto>> GetSpecialsAsync()
    {
        IEnumerable<PizzaSpecialDto> Specials = null;
       
        try
        {
            var Response = await client.GetAsync(Endpoints.GetSpecials);
            var ResponseText = await Response.Content.ReadAsStringAsync();

            logger.LogInformation("HTTP Status Code: {code}",
                Response.StatusCode);

            logger.LogInformation("HTTP Response Content: {content}",
                ResponseText);

            if (Response.IsSuccessStatusCode)
            {
                Specials = await Response.Content
                    .ReadFromJsonAsync<IEnumerable<PizzaSpecialDto>>();
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error during GetSpecialsAsync.");
        }

        return Specials ?? new List<PizzaSpecialDto>();
    }
}
