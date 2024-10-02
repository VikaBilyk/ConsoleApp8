using System.Xml.Linq;
using static System.Console;
using InvalidOperationException = System.InvalidOperationException;

//переписати цикли while

public class CoffeeManager
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
        WriteLine($"Current free volume: {maxVanVolume - currentVanVolume} kilograms");
        WriteLine();
        
        XElement coffee = new XElement("Coffee");
        
        string productNameOptions =
        "Lavazza - 1\nIlly - 2\nStarbucks - 3\nNespresso - 4\nBlue_Bottle_Coffee - 5\nCaribou_Coffee - 6 ";
        
        WriteLine("Enter name of product: ");
        WriteLine();
        CoffeeNames selectedCoffeeName = SelectCoffeeNameFromUserInput(GetInfoFromUserInput(productNameOptions, 6));
        coffee.Add(new XElement("Name", selectedCoffeeName));
        
        string productVarietyOptions = "Arabica - 1\nRobusta - 2\nKona - 3";
        
        WriteLine("Enter variety of coffee:");
        WriteLine();
        CoffeeVarieties selectedCoffeeVariety = SelectCoffeeVarietyFromUserInput(GetInfoFromUserInput(productVarietyOptions, 3));
        coffee.Add(new XElement("Variety", selectedCoffeeVariety));

        string productStateOptions = "CoffeeBeans - 1\nGround_Coffee - 2\nInstant_Coffee - 3";
        
        WriteLine("Enter state of coffee:");
        WriteLine();
        CoffeeStates selectedCoffeeState = SelectCoffeeStateFromUserInput(GetInfoFromUserInput(productStateOptions, 3));
        coffee.Add(new XElement("State", selectedCoffeeState));
        
        WriteLine("Enter cost of coffee:");
        WriteLine();
        string? input = ReadLine();
        if (double.TryParse(input, out double cost))
        {
            coffee.Add(new XElement("Cost", cost));
        }

        WriteLine("Enter weight of coffee in kilograms: ");
        WriteLine();
        input = ReadLine();
        if (double.TryParse(input, out double weight))
        {
            coffee.Add(new XElement("Weight", weight));
        }
        
        WriteLine("Enter quality of coffee: ");
        WriteLine();
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
        WriteLine();
        input = ReadLine();
        if (double.TryParse(input, out double volumeInput))
        {
            if (volumeInput + currentVanVolume <= maxVanVolume)
            {
                coffee.Add(new XElement("Volume", volumeInput));
                loadedCoffee?.Add(coffee);
                WriteLine($"Name: {coffee.Element("Name")?.Value}\nVariety: {coffee.Element("Variety")?.Value}\nState: {coffee.Element("State")?.Value}\nCost: {coffee.Element("Cost")?.Value}\nWeight: {coffee.Element("Weight")?.Value}\nQuality: {coffee.Element("Quality")?.Value}\nVolume: {coffee.Element("Volume")?.Value} ");
                WriteLine("___________________________________");
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
        WriteLine();

        foreach (var c in coffeeList)
        {
            WriteLine($"Name: {c.Element("Name")?.Value}\nPrice per weight: {PricePerWeight(c)}");
            WriteLine("___________________________________");
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
        WriteLine();
        if (coffeeList.Any())
        {
            foreach (var coffee in coffeeList)
            {
                WriteLine($"Name: {coffee.Name} - {coffee.Cost} - {coffee.Price}");
                WriteLine("___________________________________");
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

    private static int GetInfoFromUserInput(string input, int max)
    {
        bool exit = false;
        int value = 0;
        while (!exit)
        {
            WriteLine(input);
            WriteLine("___________________________________");
            value = int.Parse(ReadLine());
            if (value <= max && value >= 1)
            {
                exit = true;
            }
            WriteLine("___________________________________");
        }

        return value;
    }

    private static CoffeeNames SelectCoffeeNameFromUserInput(int value)
    {
        switch (value)
        {
            case 1:
                return CoffeeNames.Lavazza;
            case 2:
                return CoffeeNames.Illy;
            case 3:
                return CoffeeNames.Starbucks;
            case 4:
                return CoffeeNames.Nespresso;
            case 5:
                return CoffeeNames.Blue_Bottle_Coffee;
            case 6:
                return CoffeeNames.Caribou_Coffee;
            default:
                throw new Exception("Invalid input /SelectCoffeeFromUserInput");
        }
    }
    
    private static CoffeeVarieties SelectCoffeeVarietyFromUserInput(int value)
    {
        switch (value)
        {
            case 1:
                return CoffeeVarieties.Arabica;
            case 2:
                return CoffeeVarieties.Robusta;
            case 3:
                return CoffeeVarieties.Kona;
            default:
                throw new Exception("Invalid input /SelectCoffeeVarietyFromUserInput");
        }
    }

    private static CoffeeStates SelectCoffeeStateFromUserInput(int value)
    {
        switch (value)
        {
            case 1:
                return CoffeeStates.Coffee_Beans;
            case 2:
                return CoffeeStates.Ground_Coffee;
            case 3:
                return CoffeeStates.Instant_Coffee;
            default:
                throw new Exception("Invalid input /SelectCoffeeStateFromUserInput");
        }
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

public enum CoffeeStates
{
    Coffee_Beans,
    Ground_Coffee,
    Instant_Coffee
}