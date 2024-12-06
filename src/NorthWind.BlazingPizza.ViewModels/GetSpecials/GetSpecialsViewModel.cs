﻿namespace NorthWind.BlazingPizza.ViewModels.GetSpecials;
public class GetSpecialsViewModel(GetSpecialsProxy proxy)
{
    public IEnumerable<PizzaSpecialDto> Specials { get; private set; }
    public async Task GetSpecialsAsync() =>
        Specials = await proxy.GetSpecialsAsync();
}
