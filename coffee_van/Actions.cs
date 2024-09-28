namespace coffee_van;

public class Actions
{
    public void LoadVan(Van van, List<Coffee> newCoffeeList)
    {
        double currentVanVolume = van.LoadedCoffee.Sum(coffee=>coffee.Volume);
        foreach (var coffee in newCoffeeList)
        {
            if (coffee.Volume + currentVanVolume <= van.MaxVanVolume)
            {
                van.LoadedCoffee.Add(coffee);
                currentVanVolume+=coffee.Volume;
            }
            else
            {
                // Console.WriteLine("Van is too small.");
                throw new Exception("Van is too small");
            }
        }
        //запам'ятати яка кількість кави не влізла у фургон
        
    }
}