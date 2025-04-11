using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.DAO;

namespace Bean.Entities
{
    public class Event
    {
        public int EventId { get; set; }
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public TimeSpan EventTime { get; set; }
        public Venue Venue { get; set; }
        public int TotalSeats { get; set; }
        public int AvailableSeats { get; set; }
        public decimal TicketPrice { get; set; }
        public EventType EventType { get; set; }

        public Event() { Venue = new Venue(); }
        public Event(string eventName, DateTime eventDate, TimeSpan eventTime, Venue venue, int totalSeats, decimal ticketPrice, EventType eventType)
        {
            EventName = eventName; EventDate = eventDate; EventTime = eventTime; Venue = venue;
            TotalSeats = totalSeats; AvailableSeats = totalSeats; TicketPrice = ticketPrice; EventType = eventType;
        }

        public virtual string GetDetails()
        {
            string venueDetails = Venue != null ? $"Venue: {Venue.VenueName}, Address: {Venue.Address}" : "Venue: Unknown, Address: Unknown";
            return $"Event ID: {EventId}, Name: {EventName ?? "Unnamed"}, Date: {(EventDate != default ? EventDate.ToShortDateString() : "N/A")}, Time: {EventTime}, " +
                   $"{venueDetails}, Total Seats: {TotalSeats}, Available Seats: {AvailableSeats}, " +
                   $"Ticket Price: ${TicketPrice:F2}, Type: {EventType}";
        }
    }
}
