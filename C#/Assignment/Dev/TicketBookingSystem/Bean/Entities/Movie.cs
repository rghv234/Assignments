using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.DAO;

namespace Bean.Entities
{
    public class Movie : Event
    {
        public string Genre { get; set; }
        public string ActorName { get; set; }
        public string ActressName { get; set; }

        public Movie() { }
        public Movie(string eventName, DateTime eventDate, TimeSpan eventTime, Venue venue, int totalSeats, decimal ticketPrice, string genre, string actorName, string actressName)
            : base(eventName, eventDate, eventTime, venue, totalSeats, ticketPrice, EventType.Movie)
        {
            Genre = genre; ActorName = actorName; ActressName = actressName;
        }

        public override string GetDetails() => base.GetDetails() + $", Genre: {Genre}, Actor: {ActorName}, Actress: {ActressName}";
    }
}
