using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSTAPI
{
    public class GST
    {
        public double ProAmount { get; set; }
        public double Per { get; set; }
        public GST(double ProAmount, double Per)
        {
            this.ProAmount = ProAmount;
            this.Per = Per;
        }
        public double CalcGstAmount()
        {
            var gstAmt = this.ProAmount / 100 *
           this.Per;
            return gstAmt;
        }

        public double CalcTotalAmount()
        {
            var total = this.ProAmount + CalcGstAmount();
            return total;
        }
        public double CalcCompositGst()
        {
            var gstAmt = this.ProAmount / 100 * 1;
            var total = gstAmt + this.ProAmount;
            return total;
        }
    }
}
