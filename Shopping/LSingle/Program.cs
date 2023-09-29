using LSingleLib;
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
