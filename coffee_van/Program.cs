using System.Xml.Linq;
using static System.Console;

class Program
{
    static void Main()
    {
        bool needExit = true;
        WriteLine("Enter max volume of van: ");
        
        string? input = ReadLine();

        if (double.TryParse(input, out double maxVolume))
        {
            XElement van = new XElement("Van");
            XElement vanMaxVolume = new XElement("MaxVanVolume", maxVolume);
            
            van.Add(vanMaxVolume);
            WriteLine($"Max volume of van: {maxVolume} kilograms");
            
            while (needExit)
            {
                WriteLine("Add coffee -> type 1");
                WriteLine("Sort coffee by price -> type 2");
                WriteLine("Finish -> type 3");

                int choice = int.Parse(ReadLine() ?? "0");
                switch (choice)
                {
                    case 1:
                        AddProduct(van, maxVolume);
                        break;
                    case 2:
                        SortCoffeeByPrice(van);
                        break;
                    case 3:
                        needExit = false;
                        break;
                }
            }
        }
        else
        {
            WriteLine("Invalid input. Please enter a valid number.");
        }
        
    }

    static void AddProduct(XElement loadedCoffee, double maxVanVolume )
    {
        double currentVanVolume = 0;
        
        if (loadedCoffee != null)
        {
            foreach (var c in loadedCoffee.Elements("Coffee"))
            {
                double volume = (double)c.Element("Volume");
                currentVanVolume += volume;
            }
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

    static double PricePerWeight(XElement coffee)
    {
        double price = double.Parse(coffee.Element("Cost")?.Value ?? "0");
        double weight = double.Parse(coffee.Element("Weight")?.Value ?? "1");
        return price/weight;
    }

    static void SortCoffeeByPrice(XElement van)
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
    class CoffeeComparer : IComparer<XElement>
    {
        public int Compare(XElement? x, XElement? y)
        {
            if(x==null || y==null)
                throw new InvalidOperationException();
            return PricePerWeight(x).CompareTo(PricePerWeight(y));
        }
    }
}
