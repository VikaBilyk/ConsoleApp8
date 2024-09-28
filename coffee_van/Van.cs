namespace coffee_van;

public class Van
{
    public double MaxVanVolume { get; set; }
    public List<Coffee> LoadedCoffee { get; set; }

    public Van(double maxVanVolume, List<Coffee> loadedCoffee)
    {
        MaxVanVolume = maxVanVolume;
        LoadedCoffee = loadedCoffee;
    }

    public void LoadVan(List<Coffee> newCoffeeList)
    {
        double currentVanVolume = 0;
        foreach (var coffee in newCoffeeList)
        {
            if (coffee.Volume + currentVanVolume <= MaxVanVolume)
            {
                LoadedCoffee.Add(coffee);
                currentVanVolume+=coffee.Volume;
            }
            
        }
        
    }
}