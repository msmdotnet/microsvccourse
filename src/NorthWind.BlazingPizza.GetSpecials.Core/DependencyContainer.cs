using NorthWind.BlazingPizza.GetSpecials.Core.Cache;

namespace NorthWind.BlazingPizza.GetSpecials.Core;
public static class DependencyContainer
{
    public static IServiceCollection AddGetSpecialsCoreServices(
        this IServiceCollection services,
        Action<GetSpecialsOptions> configureGetSpecialsOptions)
    {
        services.AddScoped<IGetSpecialsInputPort,
            GetSpecialsInteractor>();

        services.AddScoped<IGetSpecialsOutputPort,
            GetSpecialsPresenter>();

        services.AddScoped<IGetSpecialsController,
            GetSpecialsController>();

        services.AddSingleton<IGetSpecialsCache, GetSpecialsCache>();

        services.Configure(configureGetSpecialsOptions);

        return services;
    }
}
