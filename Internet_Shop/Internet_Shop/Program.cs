using Internet_Shop.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Internet_Shop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // добавление данных
            /*using (ApplicationContext db = new ApplicationContext())
            {
                Product product1 = new Product { Name = "Book", Price = 30, Quantity=10 };
                Product product2 = new Product { Name = "Toy car", Price = 100, Quantity = 10 };

                db.Products.AddRange(product1, product2);
                db.SaveChanges();
            }
            // получение данных
            using (ApplicationContext db = new ApplicationContext())
            {
                var products = db.Products.ToList();
                Console.WriteLine("Список объектов:");
                foreach (Product p in products)
                {
                    Console.WriteLine($"{p.Id}.{p.Name} - {p.Price}, {p.Quantity}");
                }
            }*/
            CreateHostBuilder(args).Build().Run();
            
            
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
