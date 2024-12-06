namespace NorthWind.BlazingPizza.GetSpecials.IoC;
public static class DependencyContainer
{
    public static IServiceCollection AddGetSpecialsServices(
        this IServiceCollection services,
        Action<GetSpecialsOptions> configureGetSpecialsOptions,
         Action<GetSpecialsDBOptions> configureGetSpecialsDBOptions)
    {
        services.AddGetSpecialsCoreServices(configureGetSpecialsOptions);
        services.AddRepositories(configureGetSpecialsDBOptions);
        return services;
    }
}
