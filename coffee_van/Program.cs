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
                WriteLine("Need exit -> type 3");

                int choice = int.Parse(ReadLine() ?? "0");
                switch (choice)
                {
                    case 1:
                        CoffeeManager.AddProduct(van, maxVolume);
                        break;
                    case 2:
                        CoffeeManager.SortCoffeeByPrice(van);
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
}
