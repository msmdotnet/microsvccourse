﻿namespace NorthWind.BlazingPizza.GetSpecials.Repositories.Entities;
internal class PizzaSpecial
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double BasePrice { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
}
