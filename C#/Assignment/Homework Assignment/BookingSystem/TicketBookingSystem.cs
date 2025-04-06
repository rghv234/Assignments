using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Service;

namespace Service
{
    public enum EventType { Movie, Concert, Sport }

    interface IEventServiceProvider
    {
        Bean.Event CreateEvent(string eventName, string date, string time, int totalSeats, float ticketPrice, string eventType, Bean.Venue venue);
        string[] GetEventDetails();
        int GetAvailableNoOfTickets(string eventName);
    }

    interface IBookingSystemServiceProvider
    {
        decimal CalculateBookingCost(int numTickets, string eventName);
        Bean.Booking BookTickets(string eventName, int numTickets, Bean.Customer[] customers);
        void CancelBooking(int bookingId);
        string GetBookingDetails(int bookingId);
    }

    interface IBookingSystemRepository
    {
        Bean.Event CreateEvent(string eventName, string date, string time, int totalSeats, float ticketPrice, string eventType, Bean.Venue venue);
        string[] GetEventDetails();
        int GetAvailableNoOfTickets(string eventName);
        decimal CalculateBookingCost(int numTickets, string eventName);
        Bean.Booking BookTickets(string eventName, int numTickets, List<Bean.Customer> customers);
        void CancelBooking(int bookingId);
        string GetBookingDetails(int bookingId);
    }
}

namespace Bean
{
    public class EventNotFoundException : Exception { public EventNotFoundException(string message) : base(message) { } }
    public class InvalidBookingIDException : Exception { public InvalidBookingIDException(string message) : base(message) { } }

    public class Venue
    {
        public int VenueId { get; set; }
        public string VenueName { get; set; }
        public string Address { get; set; }

        public Venue() { }
        public Venue(string venueName, string address) { VenueName = venueName; Address = address; }
    }

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

    public class Concert : Event
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

    public class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public Customer() { }
        public Customer(string customerName, string email, string phoneNumber)
        {
            CustomerName = customerName; Email = email; PhoneNumber = phoneNumber;
        }

        public string GetDetails() => $"ID: {CustomerId}, Name: {CustomerName}, Email: {Email}, Phone: {PhoneNumber}";
    }

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

    public class DBUtil
    {
        private static string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=ticketbookingsystem;Trusted_Connection=True;";

        public static SqlConnection GetDBConn()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            return conn;
        }
    }

    public class BookingSystemRepositoryImpl : Service.IBookingSystemRepository
    {
        public Event CreateEvent(string eventName, string date, string time, int totalSeats, float ticketPrice, string eventType, Venue venue)
        {
            using (SqlConnection conn = DBUtil.GetDBConn())
            {
                // Insert Venue if not exists
                string venueQuery = "IF NOT EXISTS (SELECT 1 FROM venue WHERE venue_name = @venueName) " +
                                    "INSERT INTO venue (venue_name, address) OUTPUT INSERTED.venue_id VALUES (@venueName, @address); " +
                                    "ELSE SELECT venue_id FROM venue WHERE venue_name = @venueName";
                SqlCommand venueCmd = new SqlCommand(venueQuery, conn);
                venueCmd.Parameters.AddWithValue("@venueName", venue.VenueName);
                venueCmd.Parameters.AddWithValue("@address", venue.Address);
                int venueId = (int)venueCmd.ExecuteScalar();
                venue.VenueId = venueId;

                // Insert Event
                string eventQuery = "INSERT INTO event (event_name, event_date, event_time, venue_id, total_seats, available_seats, ticket_price, event_type) " +
                                    "OUTPUT INSERTED.event_id VALUES (@eventName, @eventDate, @eventTime, @venueId, @totalSeats, @totalSeats, @ticketPrice, @eventType)";
                SqlCommand eventCmd = new SqlCommand(eventQuery, conn);
                eventCmd.Parameters.AddWithValue("@eventName", eventName);
                eventCmd.Parameters.AddWithValue("@eventDate", DateTime.Parse(date));
                eventCmd.Parameters.AddWithValue("@eventTime", TimeSpan.Parse(time));
                eventCmd.Parameters.AddWithValue("@venueId", venueId);
                eventCmd.Parameters.AddWithValue("@totalSeats", totalSeats);
                eventCmd.Parameters.AddWithValue("@ticketPrice", (decimal)ticketPrice);
                eventCmd.Parameters.AddWithValue("@eventType", eventType.ToLower());

                int eventId = (int)eventCmd.ExecuteScalar();

                Event newEvent;
                switch (eventType.ToLower())
                {
                    case "movie": newEvent = new Movie(eventName, DateTime.Parse(date), TimeSpan.Parse(time), venue, totalSeats, (decimal)ticketPrice, "Action", "Tom Cruise", "Scarlett Johansson"); break;
                    case "concert": newEvent = new Concert(eventName, DateTime.Parse(date), TimeSpan.Parse(time), venue, totalSeats, (decimal)ticketPrice, "Coldplay", "Rock"); break;
                    case "sport": newEvent = new Sport(eventName, DateTime.Parse(date), TimeSpan.Parse(time), venue, totalSeats, (decimal)ticketPrice, "Cricket", "India vs Pakistan"); break;
                    default: throw new ArgumentException("Invalid event type!");
                }
                newEvent.EventId = eventId;
                Console.WriteLine("Event created successfully!");
                return newEvent;
            }
        }

        public string[] GetEventDetails()
        {
            using (SqlConnection conn = DBUtil.GetDBConn())
            {
                string query = "SELECT e.event_id, e.event_name, e.event_date, e.event_time, e.total_seats, e.available_seats, e.ticket_price, e.event_type, " +
                               "v.venue_id, v.venue_name, v.address " +
                               "FROM event e LEFT JOIN venue v ON e.venue_id = v.venue_id";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                List<string> details = new List<string>();

                while (reader.Read())
                {
                    try
                    {
                        int? venueId = reader["venue_id"] != DBNull.Value ? (int?)reader["venue_id"] : null;
                        Venue venue = new Venue(
                            reader["venue_name"]?.ToString() ?? "Unknown Venue",
                            reader["address"]?.ToString() ?? "Unknown Address"
                        )
                        { VenueId = venueId ?? 0 };

                        Event ev;
                        string type = reader["event_type"]?.ToString().ToLower() ?? "unknown";
                        switch (type)
                        {
                            case "movie": ev = new Movie(reader["event_name"]?.ToString() ?? "Unnamed", (DateTime)reader["event_date"], (TimeSpan)reader["event_time"], venue, (int)reader["total_seats"], (decimal)reader["ticket_price"], "Action", "Tom Cruise", "Scarlett Johansson"); break;
                            case "concert": ev = new Concert(reader["event_name"]?.ToString() ?? "Unnamed", (DateTime)reader["event_date"], (TimeSpan)reader["event_time"], venue, (int)reader["total_seats"], (decimal)reader["ticket_price"], "Coldplay", "Rock"); break;
                            case "sport": ev = new Sport(reader["event_name"]?.ToString() ?? "Unnamed", (DateTime)reader["event_date"], (TimeSpan)reader["event_time"], venue, (int)reader["total_seats"], (decimal)reader["ticket_price"], "Cricket", "India vs Pakistan"); break;
                            default: ev = new Event(reader["event_name"]?.ToString() ?? "Unnamed", (DateTime)reader["event_date"], (TimeSpan)reader["event_time"], venue, (int)reader["total_seats"], (decimal)reader["ticket_price"], Service.EventType.Concert); break;
                        }
                        ev.EventId = reader["event_id"] != DBNull.Value ? (int)reader["event_id"] : 0;
                        ev.AvailableSeats = reader["available_seats"] != DBNull.Value ? (int)reader["available_seats"] : 0;
                        details.Add(ev.GetDetails());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error processing event: {ex.Message}\nStack Trace: {ex.StackTrace}");
                        continue; // Skip the problematic row
                    }
                }
                reader.Close();
                return details.ToArray();
            }
        }

        public int GetAvailableNoOfTickets(string eventName)
        {
            using (SqlConnection conn = DBUtil.GetDBConn())
            {
                string query = "SELECT available_seats FROM event WHERE event_name = @eventName";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@eventName", eventName);
                object result = cmd.ExecuteScalar();
                if (result == null) throw new EventNotFoundException($"Event '{eventName}' not found!");
                return (int)result;
            }
        }

        public decimal CalculateBookingCost(int numTickets, string eventName)
        {
            using (SqlConnection conn = DBUtil.GetDBConn())
            {
                string query = "SELECT ticket_price FROM event WHERE event_name = @eventName";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@eventName", eventName);
                object result = cmd.ExecuteScalar();
                if (result == null) throw new EventNotFoundException($"Event '{eventName}' not found!");
                return numTickets * (decimal)result;
            }
        }

        public Booking BookTickets(string eventName, int numTickets, List<Customer> customers)
        {
            if (numTickets != customers.Count) throw new ArgumentException("Number of tickets must match number of customers!");
            using (SqlConnection conn = DBUtil.GetDBConn())
            {
                SqlTransaction transaction = conn.BeginTransaction();
                try
                {
                    // Get Event ID and check availability with transaction locking
                    string eventQuery = "SELECT event_id, available_seats, ticket_price FROM event WITH (UPDLOCK, ROWLOCK) WHERE event_name = @eventName";
                    SqlCommand eventCmd = new SqlCommand(eventQuery, conn, transaction);
                    eventCmd.Parameters.AddWithValue("@eventName", eventName);
                    SqlDataReader eventReader = eventCmd.ExecuteReader();
                    if (!eventReader.Read()) throw new EventNotFoundException($"Event '{eventName}' not found!");
                    int eventId = (int)eventReader["event_id"];
                    int availableSeats = (int)eventReader["available_seats"];
                    decimal ticketPrice = (decimal)eventReader["ticket_price"];
                    eventReader.Close();

                    if (availableSeats < numTickets) throw new Exception("Not enough seats available!");

                    // Insert or Get Customer IDs
                    int customerId = -1;
                    foreach (Customer customer in customers)
                    {
                        string customerQuery = "IF NOT EXISTS (SELECT 1 FROM customer WHERE email = @email) " +
                                              "INSERT INTO customer (customer_name, email, phone_number) OUTPUT INSERTED.customer_id " +
                                              "VALUES (@customerName, @email, @phoneNumber) " +
                                              "ELSE SELECT customer_id FROM customer WHERE email = @email";
                        SqlCommand customerCmd = new SqlCommand(customerQuery, conn, transaction);
                        customerCmd.Parameters.AddWithValue("@customerName", customer.CustomerName);
                        customerCmd.Parameters.AddWithValue("@email", customer.Email);
                        customerCmd.Parameters.AddWithValue("@phoneNumber", customer.PhoneNumber);
                        customerId = (int)customerCmd.ExecuteScalar();
                        customer.CustomerId = customerId;
                    }

                    // Insert Booking
                    decimal totalCost = ticketPrice * numTickets;
                    string bookingQuery = "INSERT INTO booking (customer_id, event_id, num_tickets, total_cost) " +
                                          "OUTPUT INSERTED.booking_id VALUES (@customerId, @eventId, @numTickets, @totalCost)";
                    SqlCommand bookingCmd = new SqlCommand(bookingQuery, conn, transaction);
                    bookingCmd.Parameters.AddWithValue("@customerId", customerId);
                    bookingCmd.Parameters.AddWithValue("@eventId", eventId);
                    bookingCmd.Parameters.AddWithValue("@numTickets", numTickets);
                    bookingCmd.Parameters.AddWithValue("@totalCost", totalCost);
                    int bookingId = (int)bookingCmd.ExecuteScalar();

                    // Update Available Seats
                    string updateQuery = "UPDATE event SET available_seats = available_seats - @numTickets WHERE event_id = @eventId";
                    SqlCommand updateCmd = new SqlCommand(updateQuery, conn, transaction);
                    updateCmd.Parameters.AddWithValue("@numTickets", numTickets);
                    updateCmd.Parameters.AddWithValue("@eventId", eventId);
                    updateCmd.ExecuteNonQuery();

                    transaction.Commit();
                    Booking booking = new Booking(customerId, eventId, numTickets, totalCost) { BookingId = bookingId, Customers = customers };
                    Console.WriteLine($"Booking successful! Booking ID: {bookingId}");
                    return booking;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void CancelBooking(int bookingId)
        {
            using (SqlConnection conn = DBUtil.GetDBConn())
            {
                SqlTransaction transaction = conn.BeginTransaction();
                try
                {
                    // Get Booking Details
                    string bookingQuery = "SELECT event_id, num_tickets FROM booking WHERE booking_id = @bookingId";
                    SqlCommand bookingCmd = new SqlCommand(bookingQuery, conn, transaction);
                    bookingCmd.Parameters.AddWithValue("@bookingId", bookingId);
                    SqlDataReader reader = bookingCmd.ExecuteReader();
                    if (!reader.Read()) throw new InvalidBookingIDException($"Booking ID '{bookingId}' not found!");
                    int eventId = (int)reader["event_id"];
                    int numTickets = (int)reader["num_tickets"];
                    reader.Close();

                    // Update Available Seats
                    string updateQuery = "UPDATE event SET available_seats = available_seats + @numTickets WHERE event_id = @eventId";
                    SqlCommand updateCmd = new SqlCommand(updateQuery, conn, transaction);
                    updateCmd.Parameters.AddWithValue("@numTickets", numTickets);
                    updateCmd.Parameters.AddWithValue("@eventId", eventId);
                    updateCmd.ExecuteNonQuery();

                    // Delete Booking
                    string deleteQuery = "DELETE FROM booking WHERE booking_id = @bookingId";
                    SqlCommand deleteCmd = new SqlCommand(deleteQuery, conn, transaction);
                    deleteCmd.Parameters.AddWithValue("@bookingId", bookingId);
                    deleteCmd.ExecuteNonQuery();

                    transaction.Commit();
                    Console.WriteLine($"Booking {bookingId} cancelled successfully!");
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public string GetBookingDetails(int bookingId)
        {
            using (SqlConnection conn = DBUtil.GetDBConn())
            {
                string query = "SELECT b.booking_id, b.customer_id, b.event_id, b.num_tickets, b.total_cost, b.booking_date, " +
                               "c.customer_name, c.email, c.phone_number " +
                               "FROM booking b JOIN customer c ON b.customer_id = c.customer_id WHERE b.booking_id = @bookingId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@bookingId", bookingId);
                SqlDataReader reader = cmd.ExecuteReader();
                if (!reader.Read()) throw new InvalidBookingIDException($"Booking ID '{bookingId}' not found!");

                Booking booking = new Booking((int)reader["customer_id"], (int)reader["event_id"], (int)reader["num_tickets"], (decimal)reader["total_cost"])
                {
                    BookingId = (int)reader["booking_id"],
                    BookingDate = (DateTime)reader["booking_date"]
                };
                booking.Customers.Add(new Customer(reader["customer_name"].ToString(), reader["email"].ToString(), reader["phone_number"].ToString())
                {
                    CustomerId = (int)reader["customer_id"]
                });
                reader.Close();
                return booking.GetDetails();
            }
        }
    }

    public class EventServiceProviderImpl : Service.IEventServiceProvider
    {
        private Service.IBookingSystemRepository repository = new BookingSystemRepositoryImpl();

        public Event CreateEvent(string eventName, string date, string time, int totalSeats, float ticketPrice, string eventType, Venue venue)
            => repository.CreateEvent(eventName, date, time, totalSeats, ticketPrice, eventType, venue);

        public string[] GetEventDetails() => repository.GetEventDetails();

        public int GetAvailableNoOfTickets(string eventName) => repository.GetAvailableNoOfTickets(eventName);
    }

    public class BookingSystemServiceProviderImpl : EventServiceProviderImpl, Service.IBookingSystemServiceProvider
    {
        private Service.IBookingSystemRepository repository = new BookingSystemRepositoryImpl();

        public decimal CalculateBookingCost(int numTickets, string eventName) => repository.CalculateBookingCost(numTickets, eventName);

        public Booking BookTickets(string eventName, int numTickets, Customer[] customers)
            => repository.BookTickets(eventName, numTickets, new List<Customer>(customers));

        public void CancelBooking(int bookingId) => repository.CancelBooking(bookingId);

        public string GetBookingDetails(int bookingId) => repository.GetBookingDetails(bookingId);
    }
}

namespace App
{
    class TicketBookingSystem
    {
        static void Main(string[] args)
        {
            Bean.BookingSystemServiceProviderImpl system = new Bean.BookingSystemServiceProviderImpl();
            Console.WriteLine("Welcome to Ticket Booking System!");
            Console.WriteLine("Commands: create_event, book_tickets, cancel_tickets, get_available_seats, get_event_details, exit");

            while (true)
            {
                Console.Write("\nEnter command: ");
                string command = Console.ReadLine()?.ToLower();
                try
                {
                    switch (command)
                    {
                        case "create_event":
                            Console.Write("Event Name: "); string name = Console.ReadLine();
                            Console.Write("Date (MM/DD/YYYY): "); string date = Console.ReadLine();
                            Console.Write("Time (HH:MM): "); string time = Console.ReadLine();
                            Console.Write("Total Seats: "); int seats = int.Parse(Console.ReadLine());
                            Console.Write("Ticket Price: "); float price = float.Parse(Console.ReadLine());
                            Console.Write("Event Type (movie/concert/sport): "); string type = Console.ReadLine();
                            Console.Write("Venue Name: "); string vName = Console.ReadLine();
                            Console.Write("Venue Address: "); string vAddress = Console.ReadLine();
                            Bean.Venue venue = new Bean.Venue(vName, vAddress);
                            system.CreateEvent(name, date, time, seats, price, type, venue);
                            break;

                        case "book_tickets":
                            Console.Write("Event Name: "); string bookName = Console.ReadLine();
                            Console.Write("Number of Tickets: "); int numTickets = int.Parse(Console.ReadLine());
                            Bean.Customer[] customers = new Bean.Customer[numTickets];
                            for (int i = 0; i < numTickets; i++)
                            {
                                Console.WriteLine($"\nCustomer {i + 1}:");
                                Console.Write("Name: "); string cName = Console.ReadLine();
                                Console.Write("Email: "); string email = Console.ReadLine();
                                Console.Write("Phone: "); string phone = Console.ReadLine();
                                customers[i] = new Bean.Customer(cName, email, phone);
                            }
                            system.BookTickets(bookName, numTickets, customers);
                            break;

                        case "cancel_tickets":
                            Console.Write("Booking ID: "); int bookingId = int.Parse(Console.ReadLine());
                            system.CancelBooking(bookingId);
                            break;

                        case "get_available_seats":
                            Console.Write("Event Name: "); string seatsName = Console.ReadLine();
                            int available = system.GetAvailableNoOfTickets(seatsName);
                            Console.WriteLine($"Available Seats: {available}");
                            break;

                        case "get_event_details":
                            string[] details = system.GetEventDetails();
                            Console.WriteLine("\nEvent Details:");
                            foreach (string detail in details) Console.WriteLine(detail);
                            if (details.Length == 0) Console.WriteLine("No events available!");
                            break;

                        case "exit":
                            Console.WriteLine("Thank you for using the Ticket Booking System!");
                            return;

                        default:
                            Console.WriteLine("Invalid command!");
                            break;
                    }
                }
                catch (Bean.EventNotFoundException ex) { Console.WriteLine($"Error: {ex.Message}"); }
                catch (Bean.InvalidBookingIDException ex) { Console.WriteLine($"Error: {ex.Message}"); }
                catch (SqlException ex) { Console.WriteLine($"Database Error: {ex.Message}"); }
                catch (NullReferenceException ex) { Console.WriteLine($"Error: Null reference encountered - {ex.Message}"); }
                catch (Exception ex) { Console.WriteLine($"Error: {ex.Message}"); }
            }
        }
    }
}
