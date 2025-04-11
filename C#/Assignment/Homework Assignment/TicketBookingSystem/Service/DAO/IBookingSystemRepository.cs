using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bean.Entities;

namespace TicketBookingSystem.DAO
{
    interface IBookingSystemRepository
    {
        Event CreateEvent(string eventName, string date, string time, int totalSeats, float ticketPrice, string eventType, Venue venue);
        string[] GetEventDetails();
        int GetAvailableNoOfTickets(string eventName);
        decimal CalculateBookingCost(int numTickets, string eventName);
        Booking BookTickets(string eventName, int numTickets, List<Customer> customers);
        void CancelBooking(int bookingId);
        string GetBookingDetails(int bookingId);
    }
}
