using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Xml.Linq;
using System.IO;
using System.Linq;

[TestClass]
public class CoffeeManagerTests
{
    private XElement _coffeeVan;
    private const double MaxVolume = 100.0; // Example max volume for the van
    private const double MaxPrice = 500.0; // Example max price for the van

    [TestInitialize]
    public void Setup()
    {
        // Initialize the coffee van as an empty XML element for testing
        _coffeeVan = new XElement("CoffeeVan");
    }

    [TestMethod]
    public void AddProduct_ShouldAddCoffee_WhenInputsAreValid()
    {
        // Arrange
        var coffeeInputXml = @"
        <Coffee>
            <Volume>10</Volume>
            <Cost>100</Cost>
        </Coffee>";
        var loadedCoffee = XElement.Parse(coffeeInputXml);
        _coffeeVan.Add(loadedCoffee);

        // Simulate user input with correct values
        // Here, we assume: 
        // 1 = Lavazza
        // 1 = Arabica
        // 1 = CoffeeBeans
        // Cost = 200
        // Weight = 5
        // Quality = 10
        // Volume = 8
        var userInput = new StringReader("1\n1\n1\n200\n5\n10\n8\n");
        Console.SetIn(userInput);

        // Act
        CoffeeManager.AddProduct(_coffeeVan, MaxVolume, MaxPrice);

        // Assert
        Assert.AreEqual(2, _coffeeVan.Elements("Coffee").Count()); // Expecting two coffees now

        var addedCoffee = _coffeeVan.Elements("Coffee").Last();
        Assert.AreEqual("Lavazza", addedCoffee.Element("Name")?.Value);
        Assert.AreEqual("Arabica", addedCoffee.Element("Variety")?.Value);
        Assert.AreEqual("CoffeeBeans", addedCoffee.Element("State")?.Value);
        Assert.AreEqual("200", addedCoffee.Element("Cost")?.Value);
        Assert.AreEqual("5", addedCoffee.Element("Weight")?.Value);
        Assert.AreEqual("10", addedCoffee.Element("Quality")?.Value); // Change expected to 10
        Assert.AreEqual("8", addedCoffee.Element("Volume")?.Value); // Ensure the volume is correctly set
    }


    [TestMethod]
    public void SortCoffeeByPrice_ShouldSortCoffees_WhenCoffeesExist()
    {
        // Arrange
        var coffee1 = new XElement("Coffee",
            new XElement("Name", "Coffee1"),
            new XElement("Cost", "100"),
            new XElement("Weight", "10"));
        var coffee2 = new XElement("Coffee",
            new XElement("Name", "Coffee2"),
            new XElement("Cost", "200"),
            new XElement("Weight", "20"));
        _coffeeVan.Add(coffee1);
        _coffeeVan.Add(coffee2);

        // Act
        CoffeeManager.SortCoffeeByPrice(_coffeeVan);

        // Assert
        var sortedCoffees = _coffeeVan.Elements("Coffee").ToList();
        Assert.AreEqual("Coffee1", sortedCoffees[0].Element("Name")?.Value);
        Assert.AreEqual("Coffee2", sortedCoffees[1].Element("Name")?.Value);
    }

    [TestMethod]
    [ExpectedException(typeof(Exception), "Min price cannot be greater than max price")]
    public void FindCoffee_ShouldThrowException_WhenMinPriceGreaterThanMaxPrice()
    {
        // Act
        using (var reader = new StringReader("200\n100\n"))
        {
            Console.SetIn(reader);
            CoffeeManager.FindCoffee(_coffeeVan);
        }
    }
}
