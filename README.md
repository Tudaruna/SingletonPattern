# Паттерн проектирования "Одиночка" (Singleton)

  В данном репозитории содержатся примеры реализации паттерна "Одиночка" на языке программирования C#. 
Далее мы осветим следующие вопросы: описание паттерна, его назначение, принцип работы паттерна, его достоинства и недостатки.
После изучения теории следует реализация паттерна и программный код.

###   В папке каждого проекта содержатся:
* Реализация паттерна в библиотеке классов;
* Демонстрирование работы в консольном приложении;
* Тестирование методов классов и проверка корректности реализации паттерна.

## Оглавление

1. [Основная теория по паттерну проектирования "Одиночка" (Singleton)](#Singleton)
2. [Реализация паттерна на примере корзины товаров](#реализация-паттерна-на-примере-корзины-товаров)
3. [Реализация паттерна на примере менеджера настроек](#реализация-паттерна-на-примере-менеджера-настроек)
4. [Реализация паттерна на примере кэширования](#реализация-паттерна-на-примере-кэширования)

### Singleton

  ___Паттерн одиночка (Singleton)___ - это паттерн проектирования, который гарантирует, что класс имеет только один экземпляр, и предоставляет глобальную точку доступа к этому экземпляру.
Основная идея паттерна одиночка заключается в следующем:
* Скрыть конструктор класса и сделать его приватным, чтобы никто не мог создавать экземпляры класса напрямую.
* Создать статический метод, который будет вызывать конструктор класса только один раз и возвращать глобальную точку доступа к единственному экземпляру класса.
* Однако, следует быть осторожным с применением паттерна одиночка, так как он может снизить гибкость системы и усложнить тестирование кода, а также вызывать проблемы с многопоточностью.

>  Принцип работы паттерна одиночка основан на использовании статического метода и приватного конструктора. Класс паттерна имеет статическую переменную, которая хранит единственный экземпляр класса.
> Когда клиентский код запрашивает доступ к этому экземпляру, класс проверяет, существует ли экземпляр. Если экземпляр уже создан, то класс возвращает ссылку на него. Если экземпляр еще не создан, то класс создает его и сохраняет ссылку на него в статической переменной, а затем возвращает ссылку на экземпляр.

  Достоинства паттерна:
  + Гарантирует наличие единственного экземпляра класса.
  + Предоставляет к нему глобальную точку доступа.
  + Реализует отложенную инициализацию объекта-одиночки.

  Недостатки паттерна:
  - Нарушает принцип единственной ответственности класса.
  - Маскирует плохой дизайн.
  - Проблемы мультипоточности.
  - Требует постоянного создания Mock-объектов при юнит-тестировании.

### Реализация паттерна на примере корзины товаров

  В веб-приложениях электронной коммерции часто требуется иметь единственный экземпляр класса, отвечающего за управление корзиной товаров. Паттерн Одиночка позволяет создать такой класс, который будет отслеживать добавленные в корзину товары, их количество и другую связанную информацию. Таким образом, вы можете гарантировать, что только одна корзина товаров существует во всем приложении и что она доступна из любой части системы для работы с товарами, расчета общей суммы и оформления заказа.

  Создадим класс **ShoppingCart**. 
  В классе **ShoppingCart** объявлен **sealed**, чтобы предотвратить наследование и гарантировать целостность паттерна Singleton. 
Статическое поле **instance** хранит единственный экземпляр класса **ShoppingCart**. Приватный конструктор **ShoppingCart** предотвращает создание новых экземпляров класса через оператор **new**.
Статическое свойство **Instance** обеспечивает доступ к единственному экземпляру класса **ShoppingCart** и его создание при первом обращении.
```C#
namespace LSingleLib
{
    public sealed class ShoppingCart
    {
        private static ShoppingCart instance;
        private List<Product> products;

        private ShoppingCart()
        {
            products = new List<Product>();
        }

        public static ShoppingCart Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ShoppingCart();
                }
                return instance;
            }
        }
```

  Также в классе **ShoppingCart** определены методы **AddProduct()**, **RemoveProduct()** и **GetProducts()**, которые позволяют добавлять, удалять и получать список продуктов соответственно.
Класс Product представляет продукт со свойствами **Name** (название) и **Price** (цена). Свойства имеют публичный доступ для чтения и записи.
Оба класса находятся в пространстве имен **LSingleLib**, что означает, что они могут быть использованы в других частях программы, которые также импортируют пространство имен **LSingleLib**.
```C#
public void AddProduct(Product product)
        {
            products.Add(product);
        }

        public void RemoveProduct(Product product)
        {
            products.Remove(product);
        }

        public List<Product> GetProducts()
        {
            return products;
        }
    }

    public class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
```

  Теперь напишем консольное приложение для того чтобы убедится в правильности паттерна. 
  В **Main** мы создаем экземпляр **ShoppingCart** через **ShoppingCart.Instance**, добавляем товары и выводим их на консоль.
С помощью метода **GetProducts()** получается список продуктов из корзины. Затем с помощью цикла **foreach** каждый продукт выводится на консоль с указанием его имени и цены.
В конце программы вызывается метод **Console.ReadLine()** для ожидания ввода пользователя, чтобы консоль не закрылась сразу после вывода продуктов.
```C#
﻿using LSingleLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSingle
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ShoppingCart cart = ShoppingCart.Instance;

            Product product1 = new Product { Name = "Расчёска Единорожек", Price = 500 };
            Product product2 = new Product { Name = "Костюм Леди Баг", Price = 5000 };
            Product product3 = new Product { Name = "Носочки Хэллоуи Китти", Price = 250 };
            Product product4 = new Product { Name = "Ауф пакет", Price = 20 };
            Product product5 = new Product { Name = "Книга: В чём смысл Дос(а)?", Price = 750 };

            cart.AddProduct(product1);
            cart.AddProduct(product2);
            cart.AddProduct(product3);
            cart.AddProduct(product4);
            cart.AddProduct(product5);

            List<Product> products = cart.GetProducts();
            foreach (Product product in products)
            {
                Console.WriteLine("Товар: " + product.Name + ", Цена: " + product.Price);
            }
            Console.ReadLine();
        }
    }
}
```  

  Для того, чтобы убедиться в корректности работы нашего приложения, воспользуемся модульными тестами.
  Метод **AddProductTest** проверяет, что добавление продукта в корзину работает корректно. В этом методе создается экземпляр **ShoppingCart**, создается продукт, затем этот продукт добавляется в корзину. После добавления продукта, метод проверяет, что продукт действительно находится в списке продуктов корзины.
  Метод **RemoveProductTest** проверяет, что удаление продукта из корзины работает корректно. В этом методе также создается экземпляр **ShoppingCart**, создается продукт, затем этот продукт добавляется в корзину и после этого удаляется из нее. После удаления продукта, метод проверяет, что продукт больше не находится в списке продуктов корзины.
```C#
﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
``` 
### Реализация паттерна на примере менеджера настроек

  Если у вас есть класс, отвечающий за управление настройками приложения, вы можете реализовать его как Одиночку. Таким образом, вы будете иметь гарантию, что настройки доступны в единственном экземпляре и изменения, внесенные в одной части приложения, будут видны во всех остальных частях.

  В приведённом ниже примере класс **SettingsManager** реализует паттерн **Singleton**. Его конструктор является приватным, что не позволяет создавать экземпляры класса извне. 
  Статическое свойство **Instance** предоставляет глобальную точку доступа к единственному экземпляру класса **SettingsManager**. Если экземпляр еще не создан, то он создается при первом вызове **Instance**, иначе возвращается уже существующий экземпляр.
  **Private Dictionary<string, string> settings** - это приватное поле **settings** класса **SettingsManager**, которое представляет словарь (dictionary) для хранения настроек. Ключом в словаре будет строковый идентификатор, а значением - строка, представляющая настройку.
  **Private SettingsManager()** - это приватный конструктор класса **SettingsManager**. Он инициализирует поле **settings** и загружает настройки через метод **LoadSettings()**. Поскольку конструктор приватный, он предотвращает создание экземпляров класса извне, обеспечивая, что класс будет иметь только один экземпляр.
```C#
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerLib
{
    public class SettingsManager
    {
        private static SettingsManager instance;
        private Dictionary<string, string> settings;

        private SettingsManager()
        {
            settings = new Dictionary<string, string>();
            LoadSettings();
        }
```  
  
  **Public string GetSetting(string key)** - это публичный метод **GetSetting**, который принимает ключ (идентификатор настройки) и возвращает соответствующую настройку из словаря **settings**. Если ключ не существует в словаре, метод вернет значение **null**.
  **Private void LoadSettings()** - это приватный метод **LoadSettings**, который инициализирует словарь settings и добавляет в него начальные настройки. В данном случае, метод добавляет две настройки с ключами "language" и "theme" и соответствующими значениями "en" и "light".
  ```C#
 public static SettingsManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SettingsManager();
                }
                return instance;
            }
        }

        public string GetSetting(string key)
        {
            if (settings.ContainsKey(key))
            {
                return settings[key];
            }
            return null;
        }

        public void LoadSettings()
        {
            settings["language"] = "en";
            settings["theme"] = "light";
        }
    }

}
```  

  Таким образом, класс **SettingsManager** реализует паттерн **Singleton**, который гарантирует, что у нас будет только один экземпляр класса **SettingsManager** в рамках программы. Это позволяет нам получать доступ к настройкам приложения через один и тот же экземпляр класса из любой части программы.
  Теперь напишем консольное приложение для того чтобы убедится в правильности паттерна.
  В **Main** мы создаем экземпляр класса **SettingsManager**. В этом примере мы получаем экземпляр **SettingsManager** через его статическое свойство **Instance**. Затем мы используем метод **GetSetting**, чтобы получить различные настройки и вывести их на консоль.
  ```C#
﻿using ManagerLib;
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
```  

  Для того, чтобы убедиться в корректности работы нашего приложения, воспользуемся модульными тестами.
  В первом тесте **GetSettingTest** мы проверяем, что метод **GetSetting** успешно возвращает значение настройки по существующему ключу. Мы проверяем значения для ключей "language" и "theme", которые были установлены в методе **LoadSettings**.
  Во втором тесте **LoadSettingsTest** мы проверяем, что метод **GetSetting** возвращает **null** при попытке получить значение по несуществующему ключу. 
  В обоих тестах мы используем свойство **Instance** класса **SettingsManager**, чтобы получить экземпляр класса для тестирования.
   ```C#
﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
```  

### Реализация паттерна на примере кэширования

  Паттерн Одиночка может быть полезен при реализации кэширования данных. Вы можете создать класс кэша, который будет хранить результаты вычислений или запросов к базе данных. Благодаря Одиночке вы будете иметь доступ к кэшу из любой части приложения и управлять его содержимым централизованно.

  В этом примере класс **CacheManager** реализует паттерн **Singleton**. У него есть приватный конструктор, что не позволяет создавать экземпляры класса извне. Статическое свойство **Instance** предоставляет глобальную точку доступа к единственному экземпляру класса **CacheManager**. При первом вызове **Instance** создается новый экземпляр, а при последующих вызовах возвращается уже существующий экземпляр.
  **Private static CacheManager instance** - это статическое приватное поле **instance**, которое будет хранить единственный экземпляр класса **CacheManager**.
  **Private Dictionary<string, object> cache** - это приватное поле **cache**, которое представляет словарь (dictionary) для хранения кэшированных данных. Ключом в словаре является строковый идентификатор, а значением - объект данных.
  **Private CacheManager()** - это приватный конструктор класса **CacheManager**. Он инициализирует поле cache созданием нового экземпляра словаря. Поскольку конструктор приватный, он предотвращает создание экземпляров класса извне, обеспечивая, что класс будет иметь только один экземпляр.
  ```C#
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LcashLib
{
    public class CacheManager
    {
        private static CacheManager instance;
        private Dictionary<string, object> cache;

        private CacheManager()
        {
            cache = new Dictionary<string, object>();
        }
```

  **Public static CacheManager Instance** - это публичное статическое свойство **Instance**, которое является точкой доступа к единственному экземпляру класса **CacheManager**. Метод **get** этого свойства проверяет, если экземпляр еще не создан **(instance == null)**, то он создает новый экземпляр класса **CacheManager**. В конце метод возвращает текущий (или только что созданный) экземпляр.
  **Public void AddData(string key, object data)** - это публичный метод **AddData**, который принимает ключ (идентификатор данных) и объект данных, которые нужно добавить в кэш. Метод добавляет или обновляет соответствующую запись в словаре **cache**, используя переданный ключ и данные.
  **Public object GetData(string key)** - это публичный метод **GetData**, который принимает ключ (идентификатор данных) и возвращает соответствующий объект данных из словаря **cache**. Если ключ не существует в словаре, метод вернет значение **null**.
  ```C#
public static CacheManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CacheManager();
                }
                return instance;
            }
        }

        public void AddData(string key, object data)
        {
            cache[key] = data;
        }

        public object GetData(string key)
        {
            if (cache.ContainsKey(key))
            {
                return cache[key];
            }
            return null;
        }
    }
}
```

  Теперь напишем консольное приложение для того чтобы убедится в правильности паттерна.
  В **Main** мы создаем экземпляр класса **CacheManager**. В этом примере мы получаем экземпляр **CacheManager** через его статическое свойство **Instance**. Затем мы используем методы **AddDat**a для добавления данных в кэш и **GetData** для получения данных из кэша.
  ```C#
﻿using LcashLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lcash
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CacheManager cacheManager = CacheManager.Instance;

            cacheManager.AddData("userId", 123);
            cacheManager.AddData("username", "john_doe");

            int userId = (int)cacheManager.GetData("userId");
            string username = (string)cacheManager.GetData("username");

            Console.WriteLine("User ID: " + userId);
            Console.WriteLine("Username: " + username);

            Console.ReadKey();
        }
    }
}
```  
  
  Для того, чтобы убедиться в корректности работы нашего приложения, воспользуемся модульными тестами.
  В первом тесте **AddDataTest** мы проверяем, что метод **AddData** успешно добавляет данные в кэш. Затем мы используем метод **GetData** для получения данных по ключу и проверяем, что полученные данные соответствуют ожидаемым.
  Во втором тесте GetDataTest мы проверяем, что метод **GetData** успешно возвращает кэшированные данные по существующему ключу.
  Во всех тестах мы используем свойство **Instance** класса **CacheManager**, чтобы получить экземпляр класса для тестирования. 
  ```C#
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
```  
