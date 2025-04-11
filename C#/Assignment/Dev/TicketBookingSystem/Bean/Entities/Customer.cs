using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bean.Entities
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public Customer() { }
        public Customer(string customerName, string email, string phoneNumber)
        {
            CustomerName = customerName; Email = email; PhoneNumber = phoneNumber;
        }

        public string GetDetails() => $"ID: {CustomerId}, Name: {CustomerName}, Email: {Email}, Phone: {PhoneNumber}";
    }
}
