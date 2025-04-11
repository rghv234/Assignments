using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Bean.Entities;
using Bean.Exception;
using Bean.Util;
using Service.DAO;
using TicketBookingSystem.DAO;

namespace Service.DAO
{
    public class BookingSystemRepositoryImpl : IBookingSystemRepository
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
                    case "movie":
                        newEvent = new Movie(eventName, DateTime.Parse(date), TimeSpan.Parse(time), venue, totalSeats, (decimal)ticketPrice, "Action", "Tom Cruise", "Scarlett Johansson");
                        break;
                    case "concert":
                        newEvent = new Concert(eventName, DateTime.Parse(date), TimeSpan.Parse(time), venue, totalSeats, (decimal)ticketPrice, "Coldplay", "Rock");
                        break;
                    case "sport":
                        newEvent = new Sport(eventName, DateTime.Parse(date), TimeSpan.Parse(time), venue, totalSeats, (decimal)ticketPrice, "Cricket", "India vs Pakistan");
                        break;
                    default:
                        throw new ArgumentException("Invalid event type!");
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
                            case "movie":
                                ev = new Movie(reader["event_name"]?.ToString() ?? "Unnamed", (DateTime)reader["event_date"], (TimeSpan)reader["event_time"], venue, (int)reader["total_seats"], (decimal)reader["ticket_price"], "Action", "Tom Cruise", "Scarlett Johansson");
                                break;
                            case "concert":
                                ev = new Concert(reader["event_name"]?.ToString() ?? "Unnamed", (DateTime)reader["event_date"], (TimeSpan)reader["event_time"], venue, (int)reader["total_seats"], (decimal)reader["ticket_price"], "Coldplay", "Rock");
                                break;
                            case "sport":
                                ev = new Sport(reader["event_name"]?.ToString() ?? "Unnamed", (DateTime)reader["event_date"], (TimeSpan)reader["event_time"], venue, (int)reader["total_seats"], (decimal)reader["ticket_price"], "Cricket", "India vs Pakistan");
                                break;
                            default:
                                ev = new Event(reader["event_name"]?.ToString() ?? "Unnamed", (DateTime)reader["event_date"], (TimeSpan)reader["event_time"], venue, (int)reader["total_seats"], (decimal)reader["ticket_price"], EventType.Concert);
                                break;
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
}