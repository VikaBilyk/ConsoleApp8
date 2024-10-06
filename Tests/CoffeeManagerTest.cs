using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml.Linq;

namespace Tests
{
    [TestClass]
    public class CoffeeManagerTest
    {
        // Test: Valid Input, product should be added successfully
        [TestMethod]
        public void AddProduct_ValidInput_AddsProductToLoadedCoffee()
        {
            // Arrange
            var loadedCoffee = new XElement("Coffees");
            double maxVanVolume = 10.0;
            double maxVanPrice = 50.0;

            CoffeeManagerTestHelper.SetConsoleInput(new[] 
            {
                "1", // Select Coffee Name (Lavazza)
                "1", // Select Variety (Arabica)
                "2", // Select State (GroundCoffee)
                "15.5", // Enter Cost
                "3.0", // Enter Weight
                "9", // Enter Quality
                "2.0" // Enter Volume
            });

            // Act
            CoffeeManager.AddProduct(loadedCoffee, maxVanVolume, maxVanPrice);

            // Assert
            var addedCoffee = loadedCoffee.Element("Coffee");
            Assert.IsNotNull(addedCoffee, "Coffee was not added.");
            Assert.AreEqual("Lavazza", addedCoffee.Element("Name").Value, "Incorrect Coffee Name");
            Assert.AreEqual("Arabica", addedCoffee.Element("Variety").Value, "Incorrect Variety");
            Assert.AreEqual("GroundCoffee", addedCoffee.Element("State").Value, "Incorrect State");
            Assert.AreEqual("15.5", addedCoffee.Element("Cost").Value, "Incorrect Cost");
            Assert.AreEqual("3.0", addedCoffee.Element("Weight").Value, "Incorrect Weight");
            Assert.AreEqual("9", addedCoffee.Element("Quality").Value, "Incorrect Quality");
            Assert.AreEqual("2.0", addedCoffee.Element("Volume").Value, "Incorrect Volume");
        }

        // Test: Invalid input (cost), no product should be added
        [TestMethod]
        public void AddProduct_InvalidCost_DoesNotAddProduct()
        {
            // Arrange
            var loadedCoffee = new XElement("Coffees");
            double maxVanVolume = 10.0;
            double maxVanPrice = 50.0;

            CoffeeManagerTestHelper.SetConsoleInput(new[] 
            {
                "1", // Select Coffee Name (Lavazza)
                "1", // Select Variety (Arabica)
                "2", // Select State (GroundCoffee)
                "invalid", // Enter invalid Cost
                "3.0", // Enter Weight
                "9", // Enter Quality
                "2.0" // Enter Volume
            });

            // Act
            CoffeeManager.AddProduct(loadedCoffee, maxVanVolume, maxVanPrice);

            // Assert
            Assert.AreEqual(0, loadedCoffee.Elements("Coffee").Count(), "Product should not be added due to invalid cost.");
        }

        // Test: Exceeding van volume
        [TestMethod]
        public void AddProduct_ExceedsVanVolume_DoesNotAddProduct()
        {
            // Arrange
            var loadedCoffee = new XElement("Coffees",
                new XElement("Coffee", new XElement("Volume", 8.0))
            );
            double maxVanVolume = 10.0;
            double maxVanPrice = 50.0;

            CoffeeManagerTestHelper.SetConsoleInput(new[] 
            {
                "1", // Select Coffee Name (Lavazza)
                "1", // Select Variety (Arabica)
                "2", // Select State (GroundCoffee)
                "15.5", // Enter Cost
                "3.0", // Enter Weight
                "9", // Enter Quality
                "5.0" // Enter Volume (Exceeds remaining capacity)
            });

            // Act
            CoffeeManager.AddProduct(loadedCoffee, maxVanVolume, maxVanPrice);

            // Assert
            Assert.AreEqual(1, loadedCoffee.Elements("Coffee").Count(), "Product should not be added due to exceeding van volume.");
        }

        // Test: Exceeding van price
        [TestMethod]
        public void AddProduct_ExceedsVanPrice_DoesNotAddProduct()
        {
            // Arrange
            var loadedCoffee = new XElement("Coffees",
                new XElement("Coffee", new XElement("Cost", 45.0))
            );
            double maxVanVolume = 10.0;
            double maxVanPrice = 50.0;

            CoffeeManagerTestHelper.SetConsoleInput(new[] 
            {
                "1", // Select Coffee Name (Lavazza)
                "1", // Select Variety (Arabica)
                "2", // Select State (GroundCoffee)
                "10.0", // Enter Cost (Exceeds remaining price capacity)
                "3.0", // Enter Weight
                "9", // Enter Quality
                "2.0" // Enter Volume
            });

            // Act
            CoffeeManager.AddProduct(loadedCoffee, maxVanVolume, maxVanPrice);

            // Assert
            Assert.AreEqual(1, loadedCoffee.Elements("Coffee").Count(), "Product should not be added due to exceeding van price.");
        }

        // Test: Invalid quality input
        [TestMethod]
        public void AddProduct_InvalidQuality_DoesNotAddProduct()
        {
            // Arrange
            var loadedCoffee = new XElement("Coffees");
            double maxVanVolume = 10.0;
            double maxVanPrice = 50.0;

            CoffeeManagerTestHelper.SetConsoleInput(new[] 
            {
                "1", // Select Coffee Name (Lavazza)
                "1", // Select Variety (Arabica)
                "2", // Select State (GroundCoffee)
                "15.5", // Enter Cost
                "3.0", // Enter Weight
                "invalid", // Enter invalid Quality
                "2.0" // Enter Volume
            });

            // Act
            CoffeeManager.AddProduct(loadedCoffee, maxVanVolume, maxVanPrice);

            // Assert
            Assert.AreEqual(0, loadedCoffee.Elements("Coffee").Count(), "Product should not be added due to invalid quality input.");
        }
    }

    // Helper class to mock user input
    public static class CoffeeManagerTestHelper
    {
        private static Queue<string> _consoleInput;

        public static void SetConsoleInput(IEnumerable<string> inputs)
        {
            _consoleInput = new Queue<string>(inputs);
        }

        public static string? ReadLine()
        {
            return _consoleInput.Count > 0 ? _consoleInput.Dequeue() : null;
        }
    }
}
