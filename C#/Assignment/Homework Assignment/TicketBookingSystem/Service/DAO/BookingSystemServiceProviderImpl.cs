using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bean.Entities;
using Service.DAO;
using TicketBookingSystem.DAO;

namespace Service.DAO
{
    public class BookingSystemServiceProviderImpl : EventServiceProviderImpl, IBookingSystemServiceProvider
    {
        private IBookingSystemRepository repository = new BookingSystemRepositoryImpl();

        public decimal CalculateBookingCost(int numTickets, string eventName)
            => repository.CalculateBookingCost(numTickets, eventName);

        public Booking BookTickets(string eventName, int numTickets, Customer[] customers)
            => repository.BookTickets(eventName, numTickets, new List<Customer>(customers));

        public void CancelBooking(int bookingId)
            => repository.CancelBooking(bookingId);

        public string GetBookingDetails(int bookingId)
            => repository.GetBookingDetails(bookingId);
    }
}

