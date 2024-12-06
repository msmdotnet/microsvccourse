namespace NorthWind.BlazingPizza.Proxies;
public static class DependencyContainer
{
    public static IServiceCollection AddProxies(
        this IServiceCollection services,
        Action<HttpClient> configureGetSpecialsProxy)
    {
        services.AddHttpClient<GetSpecialsProxy>(configureGetSpecialsProxy);
        return services;
    }
}
