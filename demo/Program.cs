using demo.Data;
using demo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualBasic;

namespace demo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ApplicationDbContext dbcontext = new ApplicationDbContext();

            //1.Retrieve all categories from the production.categories table.
            var categories = dbcontext.Categories;
            foreach (var item in categories)
            {
                Console.WriteLine($"Category-Id:{item.CategoryId} , Category-Name:{item.CategoryName}");
            }

            //2.Retrieve the first product from the production.products table.
            var products1 = dbcontext.Products.FirstOrDefault();
            Console.WriteLine($"Product-Id:{products1.ProductId}, Product-Name:{products1.ProductName}, Brand-id:{products1.BrandId}, Category-Id:{products1.CategoryId}");

            //3.Retrieve a specific product from the production.products table by ID.
            var products2 = dbcontext.Products.Find(5);
            Console.WriteLine($"Product-Id:{products2.ProductId}, Product-Name:{products2.ProductName}, Brand-id:{products2.BrandId}, Category-Id:{products2.CategoryId}");

            //4.Retrieve all products from the production.products table with a certain model year.
            var products3 = dbcontext.Products.Where(e => e.ModelYear == 2016);
            foreach (var item in products3)
            {
                Console.WriteLine($"Product-Id:{item.ProductId}, Product-Name:{item.ProductName}, Brand-Id:{item.BrandId}, Category-Id:{item.CategoryId}, Model-Year:{item.ModelYear}");
            }

            //5.Retrieve a specific customer from the sales.customers table by ID.
            var customers = dbcontext.Customers.Find(2);
            Console.WriteLine($"Customer-Id:{customers.CustomerId}, Full-Name:{customers.FirstName} {customers.LastName}, Phone:{customers.Phone}, Email:{customers.Email}, Street:{customers.Street}, City:{customers.City}, ZipCode:{customers.ZipCode}");

            //6.Retrieve a list of product names and their corresponding brand names
            var products4 = dbcontext.Products.Include(e => e.Brand);
            foreach (var item in products4)
            {
                Console.WriteLine($"Brand-Name:{item.Brand.BrandName}, Product-Name:{item.ProductName}");
            }

            //7.Count the number of products in a specific category
            var products5 = dbcontext.Products.Include(e => e.Category).Count();
            Console.WriteLine(products5);

            //8.Calculate the total list price of all products in a specific category.
            var products6 = dbcontext.Products.Include(e => e.Category).Sum(e => e.ListPrice);
            Console.WriteLine($"Total sum of list price is {products6}");

            //9.Calculate the average list price of products.
            var products7 = dbcontext.Products.Average(e => e.ListPrice);
            Console.WriteLine($"Average of products is {products7}");

            //10.Retrieve orders that are completed.
            var orders=dbcontext.Orders.Where(e=>e.ShippedDate<=DateOnly.FromDateTime(DateTime.Now)&&e.ShippedDate!=null).ToList();
            foreach (var item in orders)
            {
                Console.WriteLine($"Order-Id:{item.OrderId}");
            }
        }
    }
}
