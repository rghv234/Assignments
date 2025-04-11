using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bean.Entities
{
    public class Booking
    {
        public int BookingId { get; set; }
        public int CustomerId { get; set; }
        public int EventId { get; set; }
        public int NumTickets { get; set; }
        public decimal TotalCost { get; set; }
        public DateTime BookingDate { get; set; }
        public List<Customer> Customers { get; set; }

        public Booking() { Customers = new List<Customer>(); }
        public Booking(int customerId, int eventId, int numTickets, decimal totalCost)
        {
            CustomerId = customerId; EventId = eventId; NumTickets = numTickets; TotalCost = totalCost; BookingDate = DateTime.Now; Customers = new List<Customer>();
        }

        public string GetDetails() => $"Booking ID: {BookingId}, Customer ID: {CustomerId}, Event ID: {EventId}, Tickets: {NumTickets}, " +
                                     $"Total Cost: ${TotalCost:F2}, Date: {BookingDate}, Customers:\n{string.Join("\n", Customers.ConvertAll(c => c.GetDetails()))}";
    }
}
