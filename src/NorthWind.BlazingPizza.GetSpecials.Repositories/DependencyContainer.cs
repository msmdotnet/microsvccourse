namespace NorthWind.BlazingPizza.GetSpecials.Repositories;
public static class DependencyContainer
{
    public static IServiceCollection AddRepositories(
        this IServiceCollection services,
        Action<GetSpecialsDBOptions> configureGetSpecialsDBOptions)
    {
        services.AddDbContext<GetSpecialsContext>();
        services.AddScoped<IGetSpecialsRepository,
            GetSpecialsRepository>();

        services.Configure(configureGetSpecialsDBOptions);

        return services;
    }

    public static IHost InitializeDB(this IHost app)
    {
        using IServiceScope Scope = app.Services.CreateScope();

        var Context = Scope.ServiceProvider
            .GetRequiredService<GetSpecialsContext>();

        Context.Database.EnsureCreated();

        return app;
    }
}
