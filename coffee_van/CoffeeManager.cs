using System.Xml.Linq;
using static System.Console;
using InvalidOperationException = System.InvalidOperationException;

//переписати цикли while

public class CoffeeManager
{
    private static bool _falseAnswerA = true;
    private static bool _falseAnswerB = true;
    
    public static void AddProduct(XElement loadedCoffee, double maxVanVolume )
    {
        double currentVanVolume = 0;

        foreach (var c in loadedCoffee.Elements("Coffee"))
        {
            double volume = (double)(c.Element("Volume") ?? throw new InvalidOperationException());
            currentVanVolume += volume;
        }

        WriteLine($"Current loaded volume: {currentVanVolume} kilograms");
        WriteLine($"Current free volume: {maxVanVolume - currentVanVolume} kilograms");
        
        XElement coffee = new XElement("Coffee");

        while (_falseAnswerA)
        {
            WriteLine("Enter name of product: ");
            WriteLine(
                "Lavazza - 1\nIlly - 2\nStarbucks - 3\nNespresso - 4\nBlue_Bottle_Coffee - 5\nCaribou_Coffee - 6 ");
            int? inputP = Int32.Parse(ReadLine());

            CoffeeNames selectedCoffee;

            switch (inputP)
            {
                case 1:
                    selectedCoffee = CoffeeNames.Lavazza;
                    break;
                case 2:
                    selectedCoffee = CoffeeNames.Illy;
                    break;
                case 3:
                    selectedCoffee = CoffeeNames.Starbucks;
                    break;
                case 4:
                    selectedCoffee = CoffeeNames.Nespresso;
                    break;
                case 5:
                    selectedCoffee = CoffeeNames.Blue_Bottle_Coffee;
                    break;
                case 6:
                    selectedCoffee = CoffeeNames.Caribou_Coffee;
                    break;
                default:
                    WriteLine("Invalid input, please try again.");
                    continue;
            }

            coffee.Add(new XElement("Name", selectedCoffee));
            _falseAnswerA = false;
        }
        
        while (_falseAnswerB)
        {
            WriteLine("Enter variety of coffee:");
            WriteLine("Arabica - 1\nRobusta - 2\nKona - 3");
            
            CoffeeVarieties selectedCoffee;
            int? inputP = Int32.Parse(ReadLine());
            switch (inputP)
            {
                case 1:
                    selectedCoffee = CoffeeVarieties.Arabica;
                    break;
                case 2:
                    selectedCoffee = CoffeeVarieties.Robusta;
                    break;
                case 3:
                    selectedCoffee = CoffeeVarieties.Kona;
                    break;
                default:
                    WriteLine("Invalid input, please try again.");
                    continue;
            }
            coffee.Add(new XElement("Variety", selectedCoffee));
            _falseAnswerB = false;
        }

        WriteLine("Enter state of coffee:");
        string? input = ReadLine();
        coffee.Add(new XElement("State", input));
        
        WriteLine("Enter cost of coffee:");
        input = ReadLine();
        if (double.TryParse(input, out double cost))
        {
            coffee.Add(new XElement("Cost", cost));
        }

        WriteLine("Enter weight of coffee in kilograms: ");
        input = ReadLine();
        if (double.TryParse(input, out double weight))
        {
            coffee.Add(new XElement("Weight", weight));
        }
        
        WriteLine("Enter quality of coffee: ");
        input = ReadLine();
        if (double.TryParse(input, out double quality))
        {
            coffee.Add(new XElement("Quality", quality));
        }
        else
        {
            WriteLine("Invalid quality input. Please enter a valid number.");
        }
        
        WriteLine("Enter volume of coffee:");
        input = ReadLine();
        if (double.TryParse(input, out double volumeInput))
        {
            if (volumeInput + currentVanVolume <= maxVanVolume)
            {
                coffee.Add(new XElement("Volume", volumeInput));
                loadedCoffee?.Add(coffee);
                WriteLine($"Name: {coffee.Element("Name")?.Value}\nType: {coffee.Element("Type")?.Value}\nState: {coffee.Element("State")?.Value}\nCost: {coffee.Element("Cost")?.Value}\nWeight: {coffee.Element("Weight")?.Value}\nQuality: {coffee.Element("Quality")?.Value}\nVolume: {coffee.Element("Volume")?.Value} ");
            }
            else
                WriteLine("Error: Not enough space in the van.");
        }
        else
            WriteLine("Invalid volume input.");
    }
    
    public static void SortCoffeeByPrice(XElement van)
    {
        List<XElement> coffeeList = new List<XElement>(van.Elements("Coffee"));
        if (coffeeList.Count == 0)
        {
            WriteLine("No coffee products to sort.");
            return;
        }
        coffeeList.Sort(new CoffeeComparer());
        WriteLine("Sorted coffee by price per weight:");

        foreach (var c in coffeeList)
        {
            WriteLine($"Name: {c.Element("Name")?.Value}\nPrice per weight: {PricePerWeight(c)}");
        }
    }
   
    public static void FindCoffee(XElement van)
    {
        WriteLine("Enter min price for coffee: ");
        double minPrice = double.Parse(ReadLine()!);
        
        WriteLine("Enter max price for coffee: ");
        double maxPrice = double.Parse(ReadLine()!);
        
        if(minPrice > maxPrice)
            throw new Exception("Min price cannot be greater than max price");

        var coffeeList = van.Elements("Coffee")
            .Where(c => 
            {
                double pricePerWeight = PricePerWeight(c);
                return pricePerWeight >= minPrice && pricePerWeight <= maxPrice;
            })
            .Select(c => new
            {
                Name = c.Element("Name")?.Value,
                Price = PricePerWeight(c),
                Cost = c.Element("Cost")?.Value,
            });
        WriteLine("List of coffee in selected range: ");
        if (coffeeList.Any())
        {
            foreach (var coffee in coffeeList)
            {
                WriteLine($"Name: {coffee.Name} - {coffee.Cost} - {coffee.Price}");
            }
        }
        else
        {
            WriteLine("No coffee found in the given price range.");
        }

    }
    public static double PricePerWeight(XElement coffee)
    {
        double price = double.Parse(coffee.Element("Cost")?.Value ?? "0");
        double weight = double.Parse(coffee.Element("Weight")?.Value ?? "1");
        return price/weight*0.01;
    }
    
}

public enum CoffeeNames
{
    Lavazza,
    Illy,
    Starbucks, 
    Nespresso,
    Blue_Bottle_Coffee,
    Caribou_Coffee
}

public enum CoffeeVarieties
{
    Arabica,
    Robusta,
    Kona
}