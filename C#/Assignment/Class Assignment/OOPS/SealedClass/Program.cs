using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SealedClass
{
    internal class Program
    {
        class Discount
        {
            public double ProAmount { get; set; }
            public double Per { get; set; }
            public Discount(double ProAmount, double Per)
            {
                this.ProAmount = ProAmount;
                this.Per = Per;
            }
            public double GetDiscount()
            {
                double discAmount = ProAmount / 100 * Per;
                return discAmount;
            }
            public virtual double GetProNetAmount()
            {
                double netAmount = ProAmount - GetDiscount();
                return netAmount;
            }
        }

        sealed class FlatDiscount : Discount
        {
            public FlatDiscount(double proAmount, double per) : base(proAmount, per)
            {
            }
            public sealed override double GetProNetAmount()
            {
                Per = 10;
                double discAmount = ProAmount / 100 * Per;
                double netAmount = ProAmount - discAmount;
                return netAmount;
            }
        }
        static void Main(string[] args)
        {
            FlatDiscount flatDiscount = new FlatDiscount(10000, 10);
            var netAmount = flatDiscount.GetProNetAmount();
            Console.WriteLine(netAmount);
        }
    }
}
