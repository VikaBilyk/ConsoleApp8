using System.Xml.Linq;
using coffee_van;
using static System.Console;

class Program
{
    static void Main()
    {
        WriteLine("Enter max valume of van: ");
        
        string? input = ReadLine();

        if (double.TryParse(input, out double maxVolume))
        {
            XElement van = new XElement("Van");
            XElement vanMaxVolume = new XElement("MaxVanVolume", maxVolume);
            
            van.Add(vanMaxVolume);
            WriteLine($"Max volume of van: {maxVolume} kilograms");
            
            WriteLine("Add coffee -> type 1");
            
            int choice = int.Parse(ReadLine() ?? "0");
            switch (choice)
            {
                case 1:
                    AddProduct(van);
                    break;
            }
        }
        else
        {
            WriteLine("Invalid input. Please enter a valid number.");
        }
        
    }

    static void AddProduct(XElement van)
    {
        XElement loadCoffee = new XElement("LoadCoffee");
        WriteLine("Enter name of product: ");
        string? input = ReadLine();
        XElement name = new XElement("Name", input);
        
        loadCoffee.Add(name);
        van.Add(loadCoffee);
        WriteLine($"Coffee: {loadCoffee.Value} ");

    }
}
