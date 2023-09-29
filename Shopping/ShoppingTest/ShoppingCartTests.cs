using Microsoft.VisualStudio.TestTools.UnitTesting;
using LSingleLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSingleLib.Tests
{
    [TestClass()]
    public class ShoppingCartTests
    {
        [TestMethod()]
        public void AddProductTest()
        {
            // Arrange
            var cart = ShoppingCart.Instance;
            var product = new Product { Name = "Phone", Price = 999.99m };

            // Act
            cart.AddProduct(product);
            var products = cart.GetProducts();

            // Assert
            Assert.IsTrue(products.Contains(product));
        }

        [TestMethod()]
        public void RemoveProductTest()
        {
            // Arrange
            var cart = ShoppingCart.Instance;
            var product = new Product { Name = "Laptop", Price = 1999.99m };
            cart.AddProduct(product);

            // Act
            cart.RemoveProduct(product);
            var products = cart.GetProducts();

            // Assert
            Assert.IsFalse(products.Contains(product));
        }

        [TestMethod()]
        public void GetProductsTest()
        {
            // Arrange
            var product = new Product { Name = "TV", Price = 1499.99m };

            // Assert
            Assert.AreEqual("TV", product.Name);
            Assert.AreEqual(1499.99m, product.Price);
        }
    }
}