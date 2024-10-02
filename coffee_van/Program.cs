using System.Xml;
using System.Xml.Linq;
using static System.Console;

class Program
{
    static void Main()
    {
        // Шлях до XML-файлу
        string vanFilePath = "/Users/viktoriyabilyk/RiderProjects/ConsoleApp8/coffee_van/Van.xml";

        // Завантаження існуючого файлу XML
        XDocument xDoc;
        if (File.Exists(vanFilePath))
        {
            xDoc = XDocument.Load(vanFilePath);
        }
        else
        {
            xDoc = new XDocument(new XElement("Van")); // Якщо файл не існує, створюємо новий кореневий елемент
        }

        XElement xRoot = xDoc.Element("Van");
        if (xRoot == null)
        {
            xRoot = new XElement("Van");
            xDoc.Add(xRoot);
        }

        // Отримання поточної максимальної ваги
        XElement vanMaxVolumeElement = xRoot.Element("MaxVanVolume");
        double maxVolume;

        if (vanMaxVolumeElement != null)
        {
            // Якщо вже є максимальна вага, запитуємо чи змінити її
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
                    WriteLine($"Max volume of van changed to: {maxVolume} kilograms");
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
            // Якщо максимальна вага ще не задана, просимо користувача ввести її
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

        // Зберігаємо зміни після завершення роботи
        xDoc.Save(vanFilePath);
        WriteLine("Data saved.");
    }
}
