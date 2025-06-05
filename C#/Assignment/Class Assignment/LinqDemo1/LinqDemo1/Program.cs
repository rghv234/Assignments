using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqDemo1
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }

        // Static product list
        public static List<Product> GetAllProducts()
        {
            return new List<Product>
            {
                new Product { ProductId = 1, Name = "Laptop", Price = 1000, Category = "iball" },
                new Product { ProductId = 2, Name = "Mouse", Price = 500, Category = "Hp" },
                new Product { ProductId = 3, Name = "Pen", Price = 20, Category = "Stationary" },
                new Product { ProductId = 4, Name = "Gaming Fan", Price = 700, Category = "Gamerz1" },
                new Product { ProductId = 5, Name = "Tablet", Price = 300, Category = "Hp" }
            };
        }

        public void Demo()
        {
            var products = GetAllProducts();

            Console.WriteLine("\nProducts with price > 5000:");
            var expensiveProducts = products.Where(p => p.Price > 5000);
            foreach (var p in expensiveProducts)
            {
                Console.WriteLine($"Product: {p.Name}, Price: {p.Price}");
            }

            Console.WriteLine("\nProducts ordered by name (descending):");
            var orderedProducts = products.OrderByDescending(p => p.Name);
            foreach (var p in orderedProducts)
            {
                Console.WriteLine($"Product: {p.Name}, Price: {p.Price}");
            }

            Console.WriteLine("\nGrouped products by category:");
            var grouped = products.GroupBy(p => p.Category);
            foreach (var group in grouped)
            {
                Console.WriteLine($"Category: {group.Key}");
                foreach (var p in group)
                {
                    Console.WriteLine($" - Product: {p.Name}, Price: {p.Price}");
                }
            }
        }
    }

    public class Supplier
    {
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string Category { get; set; }

        // Static supplier list
        public static List<Supplier> suppliers = new List<Supplier>
        {
            new Supplier { SupplierId = 1, SupplierName = "iball", Category = "iball" },
            new Supplier { SupplierId = 2, SupplierName = "Hp", Category = "Hp" },
            new Supplier { SupplierId = 3, SupplierName = "DOMS", Category = "Stationary" },
            new Supplier { SupplierId = 4, SupplierName = "cooling fan", Category = "Gamerz1" }
        };

        public void Demo()
        {
            var products = Product.GetAllProducts();

            // Inner join
            Console.WriteLine("\n--- INNER JOIN ---");
            var innerJoin = from p in products
                            join s in suppliers on p.Category equals s.Category
                            select new { p.Name, s.SupplierName, p.Category };

            foreach (var item in innerJoin)
            {
                Console.WriteLine($"{item.Name} \t {item.SupplierName} \t {item.Category}");
            }

            // Left join
            Console.WriteLine("\n--- LEFT JOIN ---");
            var leftJoin = from s in suppliers
                           join p in products on s.Category equals p.Category into prodGroup
                           from pg in prodGroup.DefaultIfEmpty()
                           select new
                           {
                               s.SupplierName,
                               ProductName = pg != null ? pg.Name : "No Products"
                           };

            foreach (var item in leftJoin)
            {
                Console.WriteLine($"{item.SupplierName} supplies {item.ProductName}");
            }

            // Group join
            Console.WriteLine("\n--- GROUP JOIN ---");
            var groupJoin = from s in suppliers
                            join p in products on s.Category equals p.Category into prodGroup
                            select new
                            {
                                s.SupplierName,
                                Products = prodGroup
                            };

            foreach (var group in groupJoin)
            {
                Console.WriteLine($"{group.SupplierName} supplies:");
                foreach (var p in group.Products)
                {
                    Console.WriteLine($" - {p.Name}");
                }
            }

            // Anonymous Types
            Console.WriteLine("\n--- Anonymous Types ---");
            var customJoin = from p in products
                             join s in suppliers on p.Category equals s.Category
                             where p.Price > 5000
                             select new
                             {
                                 Message = $"{s.SupplierName} provides {p.Name} for {p.Price}"
                             };

            foreach (var item in customJoin)
            {
                Console.WriteLine(item.Message);
            }

            // Filtered join
            Console.WriteLine("\n--- Filtered Products with Price > 5000 and Supplier 'Hp' ---");
            var filteredResult = from p in products
                                 join s in suppliers on p.Category equals s.Category
                                 where p.Price > 5000 && s.SupplierName == "Hp"
                                 select new { p.Name, p.Price, s.SupplierName };

            foreach (var item in filteredResult)
            {
                Console.WriteLine($"{item.Name} \t {item.SupplierName} \t {item.Price}");
            }

            // First, FirstOrDefault
            IList<int> intList = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            IList<string> stringList = new List<string> { "one", "two", "three", "four", "five" };
            IList<string> emptyList = new List<string>();

            Console.WriteLine($"First int: {intList.First()}");
            Console.WriteLine($"First even int: {intList.First(n => n % 2 == 0)}");
            Console.WriteLine($"First string: {stringList.First()}");
            // Console.WriteLine($"First of emptyList: {emptyList.First()}"); // Will throw

            Console.WriteLine($"FirstOrDefault of emptyList: {emptyList.FirstOrDefault()}");

            // Any and All
            Console.WriteLine($"\nAny Stationary Products? {products.Any(p => p.Category == "Stationary")}");
            Console.WriteLine($"All Products > 500? {products.All(p => p.Price > 500)}");

            // Aggregate Functions
            var totalPrice = products.Sum(p => p.Price);
            var totalCount = products.Count();
            var maxPrice = products.Max(p => p.Price);
            var minPrice = products.Min(p => p.Price);
            var avgPrice = products.Average(p => p.Price);

            Console.WriteLine($"\nTotal Price: {totalPrice}");
            Console.WriteLine($"Total Count: {totalCount}");
            Console.WriteLine($"Max Price: {maxPrice}");
            Console.WriteLine($"Min Price: {minPrice}");
            Console.WriteLine($"Average Price: {avgPrice}");

            // Single & SingleOrDefault
            var projector = products.SingleOrDefault(p => p.Name == "Projector");
            var scanner = products.SingleOrDefault(p => p.Name == "Scanner");

            Console.WriteLine($"\nProjector found? {(projector != null ? projector.Name : "No Projector")}");
            Console.WriteLine($"Scanner found? {(scanner != null ? scanner.Name : "No Scanner")}");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Even numbers using query syntax:");
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var evenNumbers = from n in numbers
                              where n % 2 == 0
                              select n;
            foreach (var number in evenNumbers)
            {
                Console.WriteLine(number);
            }

            Console.WriteLine("\nEven numbers using method syntax:");
            var evenNumbersMethod = numbers.Where(n => n % 2 == 0);
            foreach (var number in evenNumbersMethod)
            {
                Console.WriteLine(number);
            }

            Console.WriteLine("\n--- Product Demo ---");
            var product = new Product();
            product.Demo();

            Console.WriteLine("\n--- Supplier Demo ---");
            var supplier = new Supplier();
            supplier.Demo();

            var products = Product.GetAllProducts();
            var suppliers = Supplier.suppliers;

            // Distinct
            var uniqueCategories = products.Select(p => p.Category).Distinct();
            Console.WriteLine("\nUnique Categories:");
            foreach (var category in uniqueCategories)
            {
                Console.WriteLine(category);
            }

            // Intersect
            var commonCategories = products.Select(p => p.Category)
                                           .Intersect(suppliers.Select(s => s.Category));
            Console.WriteLine("\nCommon Categories (Intersect):");
            foreach (var item in commonCategories)
            {
                Console.WriteLine(item);
            }

            // Union
            var allCategories = products.Select(p => p.Category)
                                        .Union(suppliers.Select(s => s.Category));
            Console.WriteLine("\nAll Categories (Union):");
            foreach (var item in allCategories)
            {
                Console.WriteLine(item);
            }

            // Except
            var productOnlyCategories = products.Select(p => p.Category)
                                                .Except(suppliers.Select(s => s.Category));
            Console.WriteLine("\nProduct-Only Categories (Except):");
            foreach (var item in productOnlyCategories)
            {
                Console.WriteLine(item);
            }

            Console.ReadLine();
        }
    }
}
