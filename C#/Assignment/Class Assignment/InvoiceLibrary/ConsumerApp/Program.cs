using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvoiceLibrary; 

namespace ConsumerApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            InvoiceCalculator calculator = new InvoiceCalculator();
            Console.WriteLine("Enter base amount:");
            decimal amount = Convert.ToDecimal(Console.ReadLine());

            Console.WriteLine("Enter tax rate (%) :");
            decimal tax = Convert.ToDecimal(Console.ReadLine());

            Console.WriteLine("Enter discount rate (%):");
            decimal discount = Convert.ToDecimal(Console.ReadLine());

            string invoiceNumber = calculator.GenerateInvoiceNumber();
            string summary = calculator.GetInvoiceSummary(amount, tax, discount);

            Console.WriteLine($"Invoice Number: {invoiceNumber}");
            Console.WriteLine(" ----- Invoice Summary ----- ");
            Console.WriteLine(summary);
            Console.ReadLine();
        }
    }
}
