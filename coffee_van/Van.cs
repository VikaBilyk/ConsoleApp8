
namespace coffee_van;
    public class Van
    {
        public double MaxVanVolume { get; set; }
        public List<Coffee> LoadedCoffee { get; set; }

        Van(double maxVanVolume, List<Coffee> loadedCoffee)
        {
            MaxVanVolume = maxVanVolume;
            LoadedCoffee = loadedCoffee;
        }

    }
