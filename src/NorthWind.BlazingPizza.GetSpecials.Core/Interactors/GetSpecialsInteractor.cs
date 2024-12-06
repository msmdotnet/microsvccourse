namespace NorthWind.BlazingPizza.GetSpecials.Core.Interactors;
internal class GetSpecialsInteractor(
    IGetSpecialsRepository repository,
    IGetSpecialsOutputPort presenter,
    IGetSpecialsCache cache) : IGetSpecialsInputPort
{
    public async Task GetSpecialsAsync()
    {

        var Result = await cache.GetSpecialsAsync();
        if (Result == null) 
        {
            Result = await repository
            .GetSpecialsSortedByDescendingPriceAsync();
            await cache.SetSpecialsAsync(Result);
        }            

        await presenter.HandleResultAsync(Result);
    }
}
