using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.DAO;

namespace Bean.Entities
{
    internal class Concert : Event
    {
        public string Artist { get; set; }
        public string Type { get; set; }

        public Concert() { }
        public Concert(string eventName, DateTime eventDate, TimeSpan eventTime, Venue venue, int totalSeats, decimal ticketPrice, string artist, string type)
            : base(eventName, eventDate, eventTime, venue, totalSeats, ticketPrice, EventType.Concert)
        {
            Artist = artist; Type = type;
        }

        public override string GetDetails() => base.GetDetails() + $", Artist: {Artist}, Type: {Type}";
    }
}
