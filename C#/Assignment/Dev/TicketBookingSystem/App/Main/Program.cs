using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bean.Entities;
using Bean.Exception;
using Service.DAO;

namespace TicketBookingSystem
{
    public class Program
    {
        static void Main(string[] args)
        {
            BookingSystemServiceProviderImpl system = new BookingSystemServiceProviderImpl();
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
                            Venue venue = new Venue(vName, vAddress);
                            system.CreateEvent(name, date, time, seats, price, type, venue);
                            break;

                        case "book_tickets":
                            Console.Write("Event Name: "); string bookName = Console.ReadLine();
                            Console.Write("Number of Tickets: "); int numTickets = int.Parse(Console.ReadLine());
                            Customer[] customers = new Customer[numTickets];
                            for (int i = 0; i < numTickets; i++)
                            {
                                Console.WriteLine($"\nCustomer {i + 1}:");
                                Console.Write("Name: "); string cName = Console.ReadLine();
                                Console.Write("Email: "); string email = Console.ReadLine();
                                Console.Write("Phone: "); string phone = Console.ReadLine();
                                customers[i] = new Customer(cName, email, phone);
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
                catch (EventNotFoundException ex) { Console.WriteLine($"Error: {ex.Message}"); }
                catch (InvalidBookingIDException ex) { Console.WriteLine($"Error: {ex.Message}"); }
                catch (SqlException ex) { Console.WriteLine($"Database Error: {ex.Message}"); }
                catch (NullReferenceException ex) { Console.WriteLine($"Error: Null reference encountered - {ex.Message}"); }
                catch (Exception ex) { Console.WriteLine($"Error: {ex.Message}"); }
            }
        }
    }
}

