using ManagerLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lmanager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SettingsManager settingsManager = SettingsManager.Instance;

            // Получение настройки по ключу
            string language = settingsManager.GetSetting("language");
            Console.WriteLine("Current language: " + language);

            string theme = settingsManager.GetSetting("theme");
            Console.WriteLine("Current theme: " + theme);

            Console.ReadKey();
        }

    }
}
