using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bean.Entities;

namespace Service.DAO
{
    interface IBookingSystemServiceProvider
    {
        decimal CalculateBookingCost(int numTickets, string eventName);
        Booking BookTickets(string eventName, int numTickets, Customer[] customers);
        void CancelBooking(int bookingId);
        string GetBookingDetails(int bookingId);
    }
}
