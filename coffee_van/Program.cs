using System.Xml;
using System.Xml.Linq;
using static System.Console;

class Program
{
    static void Main()
    {
        string vanFilePath = "/Users/viktoriyabilyk/RiderProjects/ConsoleApp8/coffee_van/Van.xml";
        
        XDocument xDoc;
        
        if (File.Exists(vanFilePath))
        {
            xDoc = XDocument.Load(vanFilePath);
        }
        else
        {
            xDoc = new XDocument(new XElement("Van"));
        }

        XElement xRoot = xDoc.Element("Van");
        if (xRoot == null)
        {
            xRoot = new XElement("Van");
            xDoc.Add(xRoot);
        }
        
        XElement vanMaxVolumeElement = xRoot.Element("MaxVanVolume");
        double maxVolume;

        if (vanMaxVolumeElement != null)
        {
            maxVolume = (double)vanMaxVolumeElement;
            WriteLine($"Current max volume of van: {maxVolume} kilograms");

            WriteLine("Do you want to change the max volume? (yes/no): ");
            string? userChoice = ReadLine()?.ToLower();

            if (userChoice == "yes" || userChoice == "y")
            {
                WriteLine("Enter new max volume of van: ");
                string? input = ReadLine();

                if (double.TryParse(input, out maxVolume))
                {
                    vanMaxVolumeElement.Value = maxVolume.ToString();
                    WriteLine($"Max volume of van changed to: {maxVolume} cubic meters");
                }
                else
                {
                    WriteLine("Invalid input. Keeping the previous max volume.");
                }
            }
            else
            {
                WriteLine("Max volume of van remains unchanged.");
            }
        }
        else
        {
            WriteLine("Enter max volume of van: ");
            string? input = ReadLine();

            if (double.TryParse(input, out maxVolume))
            {
                vanMaxVolumeElement = new XElement("MaxVanVolume", maxVolume);
                xRoot.Add(vanMaxVolumeElement);
                WriteLine($"Max volume of van set to: {maxVolume} kilograms");
            }
            else
            {
                WriteLine("Invalid input. Exiting program.");
                return;
            }
        }

        bool needExit = true;

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
                    CoffeeManager.AddProduct(xRoot, maxVolume);
                    xDoc.Save(vanFilePath); // Зберігаємо зміни після додавання продукту
                    break;
                case 2:
                    CoffeeManager.SortCoffeeByPrice(xRoot);
                    break;
                case 3:
                    CoffeeManager.FindCoffee(xRoot);
                    break;
                case 4:
                    needExit = false;
                    break;
            }
        }
        xDoc.Save(vanFilePath);
        WriteLine("Data saved.");
    }
}
