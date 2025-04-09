using System;

class BookingSystem
{
    static void Main(string[] args)
    {
        while (true)
        {
            // Display ticket options
            Console.WriteLine("\nAvailable Ticket Categories:");
            Console.WriteLine("1. Silver - $50 per ticket");
            Console.WriteLine("2. Gold - $75 per ticket");
            Console.WriteLine("3. Diamond - $100 per ticket");
            Console.WriteLine("Type 'Exit' to quit booking");
            
            // Get ticket category
            Console.Write("\nEnter ticket category (Silver/Gold/Diamond) or 'Exit': ");
            string ticketType = Console.ReadLine().ToLower();
            
            // Check for exit condition
            if (ticketType == "exit")
            {
                Console.WriteLine("Thank you for using the booking system. Goodbye!");
                break;
            }
            
            // Get number of tickets
            Console.Write("Enter number of tickets to book: ");
            string ticketInput = Console.ReadLine();
            int noOfTickets;
            
            // Validate number input
            if (!int.TryParse(ticketInput, out noOfTickets))
            {
                Console.WriteLine("Invalid number of tickets! Please enter a valid number.");
                continue;
            }
            
            // Variables for calculation
            double basePrice = 0;
            string category = "";
            bool validCategory = true;
            
            // Determine base price
            if (ticketType == "silver")
            {
                basePrice = 50;
                category = "Silver";
            }
            else if (ticketType == "gold")
            {
                basePrice = 75;
                category = "Gold";
            }
            else if (ticketType == "diamond")
            {
                basePrice = 100;
                category = "Diamond";
            }
            else
            {
                validCategory = false;
                Console.WriteLine("Invalid ticket category entered!");
            }
            
            // Calculate and display cost if valid
            if (validCategory)
            {
                if (noOfTickets > 0)
                {
                    double totalCost = basePrice * noOfTickets;
                    Console.WriteLine($"\nBooking Details:");
                    Console.WriteLine($"Category: {category}");
                    Console.WriteLine($"Number of Tickets: {noOfTickets}");
                    Console.WriteLine($"Total Cost: ${totalCost:F2}");
                }
                else
                {
                    Console.WriteLine("Number of tickets must be greater than 0!");
                }
            }
        }
    }
}