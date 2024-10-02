
using System.Xml.Linq;
using static System.Console;

public static class CoffeeManager
{
    public static void AddProduct(XElement loadedCoffee, double maxVanVolume )
    {
        double currentVanVolume = 0;

        foreach (var c in loadedCoffee.Elements("Coffee"))
        {
            double volume = (double)(c.Element("Volume") ?? throw new InvalidOperationException());
            currentVanVolume += volume;
        }

        WriteLine($"Current loaded volume: {currentVanVolume} kilograms");
        
        XElement coffee = new XElement("Coffee");
        
        WriteLine("Enter name of product: ");
        string? input = ReadLine();
        coffee.Add(new XElement("Name", input));
        
        WriteLine("Enter type of coffee:");
        input = ReadLine();
        coffee.Add(new XElement("Type", input));
        
        WriteLine("Enter state of coffee:");
        input = ReadLine();
        coffee.Add(new XElement("State", input));
        
        WriteLine("Enter cost of coffee:");
        input = ReadLine();
        if (double.TryParse(input, out double cost))
        {
            coffee.Add(new XElement("Cost", cost));
        }

        WriteLine("Enter weight of coffee: ");
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
                // Перетворення значення "Price" з XML на число для порівняння
                double price;
                bool isPriceValid = double.TryParse(c.Element("Cost")?.Value, out price);
                return isPriceValid && price >= minPrice && price <= maxPrice;
            })
            .Select(c => new
            {
                Name = c.Element("Name")?.Value,
                Price = c.Element("Cost")?.Value
            });

        if (coffeeList.Any())
        {
            foreach (var coffee in coffeeList)
            {
                WriteLine("Name: " + coffee.Name);
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
        return price/weight;
    }
}