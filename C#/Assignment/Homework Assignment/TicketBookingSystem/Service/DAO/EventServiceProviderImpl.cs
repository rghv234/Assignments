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
    public class EventServiceProviderImpl : IEventServiceProvider
    {
        private IBookingSystemRepository repository = new BookingSystemRepositoryImpl();

        public Event CreateEvent(string eventName, string date, string time, int totalSeats, float ticketPrice, string eventType, Venue venue)
            => repository.CreateEvent(eventName, date, time, totalSeats, ticketPrice, eventType, venue);

        public string[] GetEventDetails() => repository.GetEventDetails();

        public int GetAvailableNoOfTickets(string eventName) => repository.GetAvailableNoOfTickets(eventName);
    }
}
