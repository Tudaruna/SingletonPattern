# Паттерн проектирования "Одиночка" (Singleton)

  В данном репозитории содержатся примеры реализации паттерна "Одиночка" на языке программирования C#. 
Далее мы осветим следущие вопросы: описание паттерна, его назначение, принцип работы паттерна, его достоинства и недостатки.
После изучения теории следует реализация паттерна и программный код.

###   В папке каждого проекта содержатся:
* Реализация паттерна в библиотеке классов;
* Демострирование работы в консольном приложении;
* Тестирование методов классов и проверка корректности реализации паттерна.

## Оглавление

1. [Основная теория по паттерну проектирования "Одиночка" (Singleton)](#что-такое-паттерн-Singleton)
2. [Реализация паттерна на примере транспорта](#реализация-паттерна-на-примере-транспорта)
3. [Реализация паттерна на примере мебельного производства](#реализация-паттерна-на-примере-мебельного-производства)
4. [Реализация паттерна на примере продуктов](#реализация-паттерна-на-примере-продуктов)

### Singleton

  ___Паттерн одиночка (Singleton)___ - это паттерн проектирования, который гарантирует, что класс имеет только один экземпляр, и предоставляет глобальную точку доступа к этому экземпляру.
Основная идея паттерна одиночка заключается в следующем:
* Скрыть конструктор класса и сделать его приватным, чтобы никто не мог создавать экземпляры класса напрямую.
* Создать статический метод, который будет вызывать конструктор класса только один раз и возвращать глобальную точку доступа к единственному экземпляру класса.
* Однако, следует быть осторожным с применением паттерна одиночка, так как он может снизить гибкость системы и усложнить тестирование кода, а также вызывать проблемы с многопоточностью.

>  Принцип работы паттерна одиночка основан на использовании статического метода и приватного конструктора. Класс паттерна имеет статическую переменную, которая хранит единственный экземпляр класса. Когда клиентский код запрашивает доступ к этому экземпляру, класс проверяет, существует ли экземпляр. Если экземпляр уже создан, то класс возвращает ссылку на него. Если экземпляр еще не создан, то класс создает его и сохраняет ссылку на него в статической переменной, а затем возвращает ссылку на экземпляр.

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

> В веб-приложениях электронной коммерции часто требуется иметь единственный экземпляр класса, отвечающего за управление корзиной товаров. Паттерн Одиночка позволяет создать такой класс, который будет отслеживать добавленные в корзину товары, их количество и другую связанную информацию. Таким образом, вы можете гарантировать, что только одна корзина товаров существует во всем приложении и что она доступна из любой части системы для работы с товарами, расчета общей суммы и оформления заказа.







### Реализация паттерна на примере транспорта

Предположим, что у нас есть два вида транспорта спортивный 
и семейный. К данным видам может относится как мотоциклы так
и машины.

Для начала создадим два абстрактных класса: ___Car___ и ___Motorcycle___
В каждом из этих классов будет абстрактный метод, который будет реализован в
конкретном транспортном средстве: 

Абстрактный класс ___Car___
```C#
namespace abstractFactoryLib
{
    public abstract class Car
    {
        public abstract void Drive();
    }
}
```

Абстрактный класс ___Motorcycle___
```C#
namespace abstractFactoryLib
{
    public abstract class Motorcycle
    {
        public abstract void Ride();
    }
}
```

> В данном случае можно было сделать один абстрактный класс с методом
> Drive(), но это не критично для реализации паттерна

После этого нам необходимо создать классы наследники, которые будут
реализовывать методы абстрактных классов. В данном случае классами
наследниками абстрактного класса ___Car___ будут классы 
___FamilyCar___ и ___SportsCar___.

Класс FamilyCar
```C#
using System;

namespace abstractFactoryLib
{
    public class FamilyCar : Car
    {
        public override void Drive()
        {
            Console.WriteLine("Вы ведете семейный автомобиль");
        }
    }
}
```

Класс SportsCar
```C#
using System;

namespace abstractFactoryLib
{
    public class SportsCar : Car
    {
        public override void Drive()
        {
            Console.WriteLine("Вы ведете спортивный автомобиль");
        }
    }
}
```

Для абстрактного класса ___Motorcycle___ наследникаи будут классы
___FamilyMotorcycle___ и ___SportsMotorcycle___

Класс FamilyMotorcycle
```C#
using System;

namespace abstractFactoryLib
{
    public class FamilyMotorcycle : Motorcycle
    {
        public override void Ride()
        {
            Console.WriteLine("Вы катаетесь на семейном мотоцикле");
        }
    }
}
```

Класс SportsMotorcycle
```C#
using System;

namespace abstractFactoryLib
{
    public class SportsMotorcycle : Motorcycle
    {
        public override void Ride()
        {
            Console.WriteLine("Вы катаетесь на спортивном мотоцикле");
        }
    }
}
```

Теперь нам необходимо реализовать абстрактный класс фабрики
по созданию транспорта, в котором будет 2 метода: создание автомобиля
___CreateCar()___ и создание мотоцикла ___CreateMotorcycle()___

```C#
namespace abstractFactoryLib
{
    public abstract class VehicleFactory
    {
        public abstract Car CreateCar();
        public abstract Motorcycle CreateMotorcycle();
    }
}
```

После этого мы приступаем к реализации конкретной фабрики, которая 
будет создвать объекта одного семейства. В нашем случае будет две
фабрики, которые наследуют методы из главной фабриики: фабрика 
семейного транспорта ___FamilyVehicleFactory___ и фабрика спортивного
транспорта ___SportsVehicleFactory___.

Фабрика для создания семейного транпорта ___FamilyVehicleFactory___

```C#
namespace abstractFactoryLib
{
    public class FamilyVehicleFactory : VehicleFactory
    {
        public override Car CreateCar()
        {
            return new FamilyCar();
        }

        public override Motorcycle CreateMotorcycle()
        {
            return new FamilyMotorcycle();
        }
    }
}
```

Фабрика для создания спортивного транспорта транпорта ___SportsVehicleFactory___

```C#
using System;

namespace abstractFactoryLib
{
    public class SportsVehicleFactory: VehicleFactory
    {
        public override Car CreateCar()
        {
            return new SportsCar();
        }

        public override Motorcycle CreateMotorcycle()
        {
            return new SportsMotorcycle();
        }
    }
}
```

Теперь напишем консольное приложение для того чтобы убедится в
правильности паттерна. Сначала нам необходимо создать фабрики транспорта,
такие как SportsVehicleFactory и FamilyVehicleFactory.

После этого мы можем создать экземляр класса вызвав метод CreateCar() или
CreateMotorcycle() для создания автомобиля или мотоцикла определенного
семейства соответственно, а затем вызвать метод, который будет показывать на
чем мы едем.

```C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using abstractFactoryLib;


namespace abstractFactory
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Создаем фабрику для спортивных транспортных средств
            VehicleFactory sportsFactory = new SportsVehicleFactory();

            // Создаем спортивный автомобиль и мотоцикл
            Car sportsCar = sportsFactory.CreateCar();
            Motorcycle sportsMotorcycle = sportsFactory.CreateMotorcycle();

            sportsCar.Drive();
            sportsMotorcycle.Ride();

            // Создаем фабрику для семейных транспортных средств
            VehicleFactory familyFactory = new FamilyVehicleFactory();

            // Создаем семейный автомобиль и мотоцикл
            Car familyCar = familyFactory.CreateCar();
            Motorcycle familyMotorcycle = familyFactory.CreateMotorcycle();

            familyCar.Drive();
            familyMotorcycle.Ride();
            Console.ReadKey();
        }
    }
}
```

### Реализация паттерна на примере мебельного производства

Предположим, что у нас есть два стиля мебели: классическая и современна. К
данным видам относятся конкретные предметы мебели в нашем случае это будут
столы, стулья и диваны.

Для начала создадим абстрактный класс ___Furniture___. Данный класс будет
содержать абстрактный метод Create, который будет реализован в дочерних классах.

Абстрактный класс ___Furniture___

```C#
namespace FurnituryFactoryLib.AbstractModels
{
    public abstract class Furniture
    {
        public abstract void Create();
    }
}
```

После этого нам необходимо создать классы наследники, которые будут
реализовывать метод абстрактного класса ___Furniture___. В данном
случае классами наследниками будут классы: 

* ___ClassicChair___ - Классический стул
* ___ClassicSofa___ - Классический диван
* ___ClassicTable___ - Классический стол
* ___ModernChair___ - Современный стул
* ___ModernSofa___ - Современный диван
* ___ModernTable___ - Современный стол

Класс ___ClassicChair___

```C#
using FurnituryFactoryLib.AbstractModels;
using System;

namespace FurnituryFactoryLib.Models
{
    public class ClassicChair : Furniture
    {
        public override void Create()
        {
            Console.WriteLine("Вы создали классический стул");
        }
    }
}
```

Класс ___ClassicSofa___

```C#
using FurnituryFactoryLib.AbstractModels;
using System;

namespace FurnituryFactoryLib.Models
{
    public class ClassicSofa :Furniture
    {
        public override void Create()
        {
            Console.WriteLine("Вы создали классический диван");
        }
    }
}
```

Класс ___ClassicTable___

```C#
using FurnituryFactoryLib.AbstractModels;
using System;

namespace FurnituryFactoryLib.Models
{
    public class ClassicTable : Furniture
    {
        public override void Create()
        {
            Console.WriteLine("Вы создали классический стол");
        }
    }
}
```

Класс ___ModernChair___

```C#
using FurnituryFactoryLib.AbstractModels;
using System;

namespace FurnituryFactoryLib.Models
{
    public class ModernChair : Furniture
    {
        public override void Create()
        {
            Console.WriteLine("Вы создали современный стул");
        }
    }
}
```

Класс ___ModernSofa___

```C#
using FurnituryFactoryLib.AbstractModels;
using System;

namespace FurnituryFactoryLib.Models
{
    public class ModernSofa : Furniture
    {
        public override void Create()
        {
            Console.WriteLine("Вы создали современный диван");
        }
    }
}
```

Класс ___ModernTable___

```C#
using FurnituryFactoryLib.AbstractModels;
using System;

namespace FurnituryFactoryLib.Models
{
    public class ModernTable : Furniture
    {
        public override void Create()
        {
            Console.WriteLine("Вы создали современный стол");
        }
    }
}
```
Теперь нам необходимо реализовать абстрактный класс фабрики по 
созданию мебели ___FurnitureFactory___, в котором будет 3 
метода: создание стула CreateChair(), создание стола CreateTable()
и создание дивана CreateSofa().

Класс ___FurnitureFactory___

```C#
using FurnituryFactoryLib.AbstractModels;

namespace FurnituryFactoryLib.Factories
{
    public abstract class FurnitureFactory
    {
        public abstract Furniture CreateChair();
        public abstract Furniture CreateTable();
        public abstract Furniture CreateSofa();
    }
}
```
После этого мы приступаем к реализации конкретной фабрики, 
которая будет создвать объекта одного семейства. В нашем случае
будет две фабрики, которые наследуют методы из главной фабриики:
фабрика классической мебели ___ClassicFurnitureFactory___ и фабрика
современной мебели ___ModernFurnitureFacrtory___.

Фабрика для создания классической мебели ___ClassicFurnitureFactory___

```C#
using FurnituryFactoryLib.AbstractModels;
using FurnituryFactoryLib.Models;

namespace FurnituryFactoryLib.Factories
{
    public class ClassicFurnitureFactory : FurnitureFactory
    {
        public override Furniture CreateChair()
        {
            return new ClassicChair();
        }

        public override Furniture CreateSofa()
        {
            return new ClassicSofa();
        }

        public override Furniture CreateTable()
        {
            return new ClassicTable();
        }
    }
}
```

Фабрика для создания современной мебели ___ModernFurnitureFactory___

```C#
using FurnituryFactoryLib.AbstractModels;
using FurnituryFactoryLib.Models;

namespace FurnituryFactoryLib.Factories
{
    public class ModernFurnitureFacrtory : FurnitureFactory
    {
        public override Furniture CreateChair()
        {
            return new ModernChair();
        }

        public override Furniture CreateSofa()
        {
            return new ModernSofa();
        }

        public override Furniture CreateTable()
        {
            return new ModernTable();
        }
    }
}
```

Теперь напишем консольное приложение для того чтобы убедится в 
правильности реализации паттерна. Сначала нам необходимо создать фабрики 
мебели, такие как ___ModernFurnitureFactory___ и ___ModernFurnitureFactory___.

После этого мы можем создать экземляр класса вызвав метод 
CreateChair(), CreateSofa() или CreateTable() для создания стула, дивана или 
стола определенного семейства соответственно, а затем вызвать
метод Create() для создания определенного элемента мебели.

```C#
using FurnituryFactoryLib.AbstractModels;
using FurnituryFactoryLib.Factories;
using System;

namespace FurnitureFactory
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ModernFurnitureFacrtory modernFactory = new ModernFurnitureFacrtory();

            // Создаем современную мебель
            Furniture modernChair = modernFactory.CreateChair();
            Furniture modernTable = modernFactory.CreateTable();
            Furniture modernSofa = modernFactory.CreateSofa();

            // Создаем мебель с помощью метода Create
            modernChair.Create();
            modernTable.Create();
            modernSofa.Create();

            // Создаем фабрику для классической мебели
            ClassicFurnitureFactory classicFactory = new ClassicFurnitureFactory();

            // Создаем классическую мебель
            Furniture classicChair = classicFactory.CreateChair();
            Furniture classicTable = classicFactory.CreateTable();
            Furniture classicSofa = classicFactory.CreateSofa();

            // Создаем мебель с помощью метода Create
            classicChair.Create();
            classicTable.Create();
            classicSofa.Create();
            Console.ReadKey();
        }
    }
}
```

### Реализация паттерна на примере продуктов

Предположим, что у нас есть два вида продуктов: сладости и
молочная продукция. К сладостям будут относится конфеты и
безлактозный йогурт. К молочной продукции будет относится молоко и 
молочный шоколад.

Для начала необходимо создать абстрактный класс ___Food___. Данный класс
будет содержать абстрактный метод Display, который будет показывать
какой продукт выбран и будет реализован в дочерних классах.

Абстрактный класс ___Food___

```C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductFactoryLib.AbstractModels
{
    public abstract class Food
    {
        public abstract void Display();
    }
}
```

После этого нам необходимо создать классы наследники, которые
будут реализовывать метод абстрактного класса ___Food___. В данном 
случае классами наследниками будут классы:

* ___Candy___ - Конфеты
* ___Milk___ - Молоко
* ___MilkChocolate___ - Молочный шоколад
* ___Yogurt___ - безлактозный йогурт

Класс ___Candy___

```C#
using ProductFactoryLib.AbstractModels;
using System;

namespace ProductFactoryLib.Models
{
    public class Candy : Food
    {
        public override void Display()
        {
            Console.WriteLine("Конфеты");
        }
    }
}
```

Класс ___Milk___

```C#
using ProductFactoryLib.AbstractModels;
using System;

namespace ProductFactoryLib.Models
{
    public class Milk : Food
    {
        public override void Display()
        {
            Console.WriteLine("Молоко");
        }
    }
}
```

Класс ___MilkChocolate___

```C#
using ProductFactoryLib.AbstractModels;
using System;

namespace ProductFactoryLib.Models
{
    public class MilkChocolate : Food
    {
        public override void Display()
        {
            Console.WriteLine("Молочный шоколад");
        }
    }
}
```

Класс ___Yogurt___

```C#
﻿using ProductFactoryLib.AbstractModels;
using System;

namespace ProductFactoryLib.Models
{
    public class Yogurt : Food
    {
        public override void Display()
        {
            Console.WriteLine("Безлактозный йогурт");
        }
    }
}
```

Теперь нам необходимо реализовать абстрактный класс фабрики
по созданию мебели ___FoodFactory___, в котором будет 2 метода: 
создание молочного продукта CreateDairyProduct() и создание 
сладостей CreateConfectionary().

Абстрактный класс ___FoodFactory___

```C#
using ProductFactoryLib.AbstractModels;

namespace ProductFactoryLib.Factories
{
    public abstract class FoodFactory
    {
        public abstract Food CreateDairyProduct();
        public abstract Food CreateConfectionery();
    }
}
```

После этого мы приступаем к реализации конкретной фабрики, 
которая будет создвать объекта одного семейства. В нашем случае
будет две фабрики, которые наследуют методы из главной фабриики:
фабрика молочных продуктов ___MilkFactory___ и фабрика сладостей ___CandyFactory___.

Класс ___MilkFactory___

```C#
using ProductFactoryLib.Models;

namespace ProductFactoryLib.Factories
{
    public class MilkFactory : FoodFactory
    {
        public override Food CreateDairyProduct()
        {
            return new Milk();
        }

        public override Food CreateConfectionery()
        {
            return new MilkChocolate();
        }
    }
}
```

Класс ___CandyFactory___

```C#
﻿using ProductFactoryLib.AbstractModels;
using ProductFactoryLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductFactoryLib.Factories
{
    public class CandyFactory : FoodFactory
    {
        public override Food CreateDairyProduct()
        {
            return new Yogurt();
        }

        public override Food CreateConfectionery()
        {
            return new Candy();
        }
    }
}
```

Теперь напишем консольное приложение для того чтобы убедится в
правильности реализации паттерна. Сначала нам необходимо создать
фабрики продуктов, такие как MilkFactory и CandyFactory.

После этого мы можем создать экземляр класса вызвав метод
CreateDairyProduct() или CreateConfectionary() для создания
молочного продукта или сладостей соответственно, а затем вызвать
метод Display() для отображения имени продукта.

```C#
using ProductFactoryLib.AbstractModels;
using ProductFactoryLib.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductFactory
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FoodFactory milkFactory = new MilkFactory();
            Food milkProduct = milkFactory.CreateDairyProduct();
            Food milkConfectionery = milkFactory.CreateConfectionery();

            Console.WriteLine("Производим молочные продукты:");
            milkProduct.Display();
            milkConfectionery.Display();

            // Создаем фабрику для производства кондитерских изделий
            FoodFactory candyFactory = new CandyFactory();
            Food yogurtProduct = candyFactory.CreateDairyProduct();
            Food candyProduct = candyFactory.CreateConfectionery();

            Console.WriteLine("\nПроизводим кондитерские изделия:");
            yogurtProduct.Display();
            candyProduct.Display();

            Console.ReadKey();
        }
    }
}
```
