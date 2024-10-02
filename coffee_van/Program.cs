using System.Xml;
using System.Xml.Linq;
using static System.Console;


//додати: аби товар додавався до фургону не лише за місткістю ваги, а й максимальної ціни
//додати функцію пошуку
//при додавані кави, якшо у фургоні вже міститься кава з такими ж властивостями, яка вже є у фурногі зробити додавання по вазі та масі
class Program
{
    static string vanFilePath = "/Users/viktoriyabilyk/RiderProjects/ConsoleApp8/coffee_van/Van.xml"; 
    
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
                WriteLine("Enter your action: ");
                WriteLine("Add coffee -> type 1");
                WriteLine("Sort coffee by price -> type 2");
                WriteLine("Find coffee -> type 3");
                WriteLine("Need exit -> type 4");

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
                        CoffeeManager.FindCoffee(van);
                        break;
                    case 4:
                        needExit = false;
                        break;
                }
            }
            van.Save(vanFilePath);
            WriteLine("Van data saved.");
        }
        else
        {
            WriteLine("Invalid input. Please enter a valid number.");
        }
    }
}
