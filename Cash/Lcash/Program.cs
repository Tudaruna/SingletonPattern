using LcashLib;
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
