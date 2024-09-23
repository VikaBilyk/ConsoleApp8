namespace lab1_coffee_van;

public class Van
{
    public double MaxVanVolume { get; set; }
    public List<Coffee> Coffees { get; set; }

    public Van(double maxVanVolume, List<Coffee> coffees)
    {
        MaxVanVolume = maxVanVolume;
        Coffees = coffees;
    }

    public void AddProducts(Coffee newCoffee)
    {
        double vanVolume = 0;
        foreach (var c in Coffees)
        {
            vanVolume += c.Volume;
        }
        double freeVanVolume = MaxVanVolume - vanVolume;
        if (freeVanVolume > newCoffee.Volume)
        {
            Coffees.Add(newCoffee);
        }
        else
        {
            Console.WriteLine("Van is too small");
        }
    }
}