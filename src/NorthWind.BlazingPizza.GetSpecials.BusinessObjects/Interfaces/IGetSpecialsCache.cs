namespace NorthWind.BlazingPizza.GetSpecials.BusinessObjects.Interfaces;
public interface IGetSpecialsCache
{
    Task SetSpecialsAsync(IEnumerable<PizzaSpecialDto> pizzas);
    Task<IEnumerable<PizzaSpecialDto>> GetSpecialsAsync();
}
