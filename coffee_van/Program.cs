using System.Xml;
using System.Xml.Linq;
using static System.Console;

//Фургон кави.
//Завантажити фургон певного об'єму вантажем на певну суму з різних сортів кави,
//які знаходяться в різних фізичних станах (зерно, мелене, розчинне в банках і пакетиках).
//Урахувати об'єм кави разом з упаковкою. Провести сортування товарів на основі співвідношення ціни та ваги.
//Знайти товар у фургоні, що відповідає заданому діапазону параметрів якості.


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
        
        XElement vanMaxVolumeElement = xRoot.Element("MaxVanVolume") ?? throw new InvalidOperationException();
        double maxVolume;

        XElement vanMaxPriceElement = xRoot.Element("MaxVanPrice") ?? throw new InvalidOperationException();
        double maxPrice;

        if (vanMaxVolumeElement != null)
        {
            maxVolume = (double)vanMaxVolumeElement;
            WriteLine($"Current max volume of van: {maxVolume} cubic meters");

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
                WriteLine($"Max volume of van set to: {maxVolume} cubic meters");
            }
            else
            {
                WriteLine("Invalid input. Exiting program.");
                return;
            }
        }
        
        if (vanMaxPriceElement != null)
        {
            maxPrice = (double)vanMaxPriceElement;
            WriteLine($"Current max price of van: {maxPrice} i.u.");
            WriteLine();

            WriteLine("Do you want to change the max price? (yes/no): ");
            string? userChoice = ReadLine()?.ToLower();

            if (userChoice == "yes" || userChoice == "y")
            {
                WriteLine("Enter new max price of van: ");
                string? input = ReadLine();

                if (double.TryParse(input, out maxPrice))
                {
                    vanMaxPriceElement.Value = maxPrice.ToString();
                    WriteLine($"Max price of van changed to: {maxPrice} i.u.");
                    WriteLine();
                }
                else
                {
                    WriteLine("Invalid input. Keeping the previous max price.");
                }
            }
            else
            {
                WriteLine("Max price of van remains unchanged.");
            }
        }
        
        else
        {
            WriteLine("Enter max price of van: ");
            string? input = ReadLine();

            if (double.TryParse(input, out maxPrice))
            {
                vanMaxPriceElement = new XElement("MaxVanPrice", maxPrice);
                xRoot.Add(vanMaxPriceElement);
                WriteLine($"Max price of van set to: {maxPrice} i.u.");
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
                    CoffeeManager.AddProduct(xRoot, maxVolume, maxPrice);
                    // xDoc.Save(vanFilePath);
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
