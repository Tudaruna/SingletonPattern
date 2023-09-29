# Паттерн проектирования "Одиночка" (Singleton)

  В данном репозитории содержатся примеры реализации паттерна "Одиночка" на языке программирования C#. 
Далее мы осветим следущие вопросы: описание паттерна, его назначение, принцип работы паттерна, его достоинства и недостатки.
После изучения теории следует реализация паттерна и программный код.

###   В папке каждого проекта содержатся:
* Реализация паттерна в библиотеке классов;
* Демострирование работы в консольном приложении;
* Тестирование методов классов и проверка корректности реализации паттерна.

## Оглавление

1. [Основная теория по паттерну проектирования "Одиночка" (Singleton)](#Singleton)
2. [Реализация паттерна на примере транспорта](#реализация-паттерна-на-примере-корзины-товаров)
3. [Реализация паттерна на примере мебельного производства](#реализация-паттерна-на-примере-менеджера-настроек)
4. [Реализация паттерна на примере продуктов](#реализация-паттерна-на-примере-кэширования)

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
  В классе **ShoppingCart** объявлен sealed, чтобы предотвратить наследование и гарантировать целостность паттерна Singleton. 
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

  Теперь напишем консольное приложение для того чтобы убедится в
правильности паттерна. В **Main** мы создаем экземпляр **ShoppingCart** через **ShoppingCart.Instance**, добавляем товары и выводим их на консоль.
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
