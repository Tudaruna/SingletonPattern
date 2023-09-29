using Microsoft.VisualStudio.TestTools.UnitTesting;
using LcashLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LcashLib.Tests
{
    [TestClass()]
    public class CacheManagerTests
    {
        [TestMethod()]
        public void AddDataTest()
        {
            // Arrange
            var cacheManager = CacheManager.Instance;
            var key = "key";
            var data = "data";

            // Act
            cacheManager.AddData(key, data);
            var result = cacheManager.GetData(key);

            // Assert
            Assert.AreEqual(data, result);
        }

        [TestMethod()]
        public void GetDataTest()
        {
            // Arrange
            var cacheManager = CacheManager.Instance;
            var key = "key";
            var data = "data";
            cacheManager.AddData(key, data);

            // Act
            var result = cacheManager.GetData(key);

            // Assert
            Assert.AreEqual(data, result);
        }
    }
}