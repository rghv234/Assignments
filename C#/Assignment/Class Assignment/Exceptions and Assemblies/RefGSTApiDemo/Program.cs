using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSTAPI;

namespace RefGSTApiDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GST gst = new GST(10000, 10);
            var gstAmt = gst.CalcGstAmount();
            var total = gst.CalcTotalAmount();
            Console.WriteLine($"GST Amount:{gstAmt}\tTotal Amount:{total}");
            var CompositGst = gst.CalcCompositGst();
            Console.WriteLine($"Composit GST:{CompositGst}");
        }
    }
}
