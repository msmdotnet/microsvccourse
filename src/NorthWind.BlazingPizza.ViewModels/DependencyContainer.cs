namespace NorthWind.BlazingPizza.ViewModels;
public static class DependencyContainer
{
    public static IServiceCollection AddViewModels(
        this IServiceCollection services)
    {
        services.AddScoped<GetSpecialsViewModel>();
        return services;
    }
}
