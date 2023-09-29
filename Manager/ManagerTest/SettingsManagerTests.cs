using Microsoft.VisualStudio.TestTools.UnitTesting;
using ManagerLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerLib.Tests
{
    [TestClass()]
    public class SettingsManagerTests
    {
        [TestMethod()]
        public void GetSettingTest()
        {
            // Arrange
            var settingsManager = SettingsManager.Instance;

            // Act
            var language = settingsManager.GetSetting("language");
            var theme = settingsManager.GetSetting("theme");

            // Assert
            Assert.AreEqual("en", language);
            Assert.AreEqual("light", theme);
        }

        [TestMethod()]
        public void LoadSettingsTest()
        {
            // Arrange
            var settingsManager = SettingsManager.Instance;

            // Act
            var result = settingsManager.GetSetting("nonExistingKey");

            // Assert
            Assert.IsNull(result);
        }
    }
}