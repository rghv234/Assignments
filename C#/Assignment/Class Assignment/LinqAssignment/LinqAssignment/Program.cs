using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqAssignment
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public int Age { get; set; }
    }
    public class Insurance
    {
        public int PolicyId { get; set; }
        public int CustomerId { get; set; }
        public string PolicyType { get; set; }
        public decimal PremiumAmount { get; set; }
        public bool IsActive { get; set; }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            IQueryable<Customer> customers = new List<Customer>
            {
                new Customer { CustomerId = 1, Name = "Alice", City = "New York", Age = 30 },
                new Customer { CustomerId = 2, Name = "Bob", City = "Los Angeles", Age = 25 },
                new Customer { CustomerId = 3, Name = "Charlie", City = "Chicago", Age = 35 },
                new Customer { CustomerId = 4, Name = "David", City = "Houston", Age = 35 },
                new Customer { CustomerId = 5, Name = "Eve", City = "Dallas", Age = 40 }
            }.AsQueryable();

            IQueryable<Insurance> insurances = new List<Insurance>
            {
                new Insurance {PolicyId = 101, CustomerId = 1, PolicyType = "Health", PremiumAmount = 5000, IsActive = true},
                new Insurance {PolicyId = 102, CustomerId = 2, PolicyType = "Life", PremiumAmount = 3500, IsActive = false},
                new Insurance {PolicyId = 103, CustomerId = 3, PolicyType = "Auto", PremiumAmount = 4500, IsActive=true},
                new Insurance {PolicyId = 104, CustomerId = 4, PolicyType = "Home", PremiumAmount = 2000, IsActive=true}
            }.AsQueryable();

            var city = "New York";
            var customersFromCity = customers.Where(c => c.City == city);

            var activeHighPremiumPolicies = insurances.Where(i => i.IsActive && i.PremiumAmount > 4000);

            var customerPolicies = from c in customers
                                   join i in insurances on c.CustomerId equals i.CustomerId
                                   select new
                                   {
                                       CustomerName = c.Name,
                                       i.PolicyType,
                                       i.PremiumAmount
                                   };

            var totalPremiumPolicyTypes = insurances
                .GroupBy(i => i.PolicyType)
                .Select(g => new
                {
                    PolicyType = g.Key,
                    TotalPremium = g.Sum(i => i.PremiumAmount)
                });

            var firstCustomerinDallas = customers
                .Where(c => c.City == "Dallas")
                .OrderBy(c => c.CustomerId)
                .FirstOrDefault();

            bool anyPolicyInactive = insurances.Any(i => i.IsActive);

            bool allPremiumAbove1000 = insurances.All(i => i.PremiumAmount > 1000);
            var uniquePolicyTypes = insurances.Select(i => i.PolicyType).Distinct();
            var skipTwoTakeThree = insurances.Skip(2).Take(3);
            var totalPremiumAmount = insurances.Sum(i => i.PremiumAmount);
            var customersWithPolicies = customers
                .Where(c => insurances.Any(i => i.CustomerId == c.CustomerId))
                .Select(c => new
                {
                    c.Name,
                    PolicyCount = insurances.Count(i => i.CustomerId == c.CustomerId)
                });
        }
    }
}
