using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnumDemo
{
    enum OrderStatus
    {
        Pending = 1,
        Processing = 2,
        Shipped = 3,
        Delivered = 4,
        Cancelled = 5
    }

    [Flags]
    enum FileAccess
    {
        None = 0,
        Read = 1,
        Write = 2,
        Execute = 4
    }

    internal class Order
    {
        public OrderStatus Status { get; set; }
        public int OrderId { get; set; }
        public string CustomerName { get; set; }
        public void PrintStatus()
        {
            Console.WriteLine($"Order status: {Status}");
        }
        public void DisplayOrder()
        {
            Console.WriteLine($"Order ID: {OrderId}, Customer Name: {CustomerName}, Status: {Status}");
        }
        public void UpdateStatus(OrderStatus newStatus)
        {
            if (Enum.IsDefined(typeof(OrderStatus), newStatus))
            {
                Status = newStatus;
                Console.WriteLine($"Order status updated to: {Status}");
            }
            else
            {
                Console.WriteLine("Invalid status update.");
            }
        }
    }
}


