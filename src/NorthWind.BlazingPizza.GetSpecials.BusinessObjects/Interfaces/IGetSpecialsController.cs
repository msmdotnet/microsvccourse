namespace NorthWind.BlazingPizza.GetSpecials.BusinessObjects.Interfaces;
public interface IGetSpecialsController
{
    Task<IEnumerable<PizzaSpecialDto>> GetSpecialsAsync();
}
