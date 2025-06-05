using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceLibrary
{
    public class InvoiceCalculator
    {
        public decimal CalculateTotal(decimal amount, decimal taxRate)
        {
            return amount + (amount * taxRate);
        }

        public decimal ApplyDiscount(decimal total, decimal discountRate)
        {
            return total - (total * discountRate);
        }
        public string GenerateInvoiceNumber()
        {
            return Guid.NewGuid().ToString("N").Substring(0, 10).ToUpper();
        }
        public string GetInvoiceSummary(decimal total, decimal taxRate, decimal discountRate)
        {
            var taxAmount = total * taxRate;
            var discountAmount = total * discountRate;
            var finalTotal = total + taxAmount - discountAmount;
            return $"Total: {total:C}, Tax: {taxAmount:C}, Discount: {discountAmount:C}, Final Total: {finalTotal:C}";
        }
    }
}
