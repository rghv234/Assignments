using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bean.Entities;

namespace Service.DAO
{
    public enum EventType { Movie, Concert, Sport }
    interface IEventServiceProvider
    {
        Event CreateEvent(string eventName, string date, string time, int totalSeats, float ticketPrice, string eventType, Venue venue);
        string[] GetEventDetails();
        int GetAvailableNoOfTickets(string eventName);
    }
}
