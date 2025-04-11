using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.DAO;

namespace Bean.Entities
{
    public class Sport : Event
    {
        public string SportName { get; set; }
        public string TeamsName { get; set; }

        public Sport() { }
        public Sport(string eventName, DateTime eventDate, TimeSpan eventTime, Venue venue, int totalSeats, decimal ticketPrice, string sportName, string teamsName)
            : base(eventName, eventDate, eventTime, venue, totalSeats, ticketPrice, EventType.Sport)
        {
            SportName = sportName; TeamsName = teamsName;
        }

        public override string GetDetails() => base.GetDetails() + $", Sport: {SportName}, Teams: {TeamsName}";
    }

}
