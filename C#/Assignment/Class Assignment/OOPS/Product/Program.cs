using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product
{
    internal class Program
    {
        public int ProCode { get; set; }
        public string ProName { get; set; }
        public DateTime ExpiryDate { get; set; }

        private string _ProCategory;

        public string MadeIn
        {
            get
            {
                return "India";
            }
        }
        public string ProCategory
        {
            get
            {
                return _ProCategory;
            }
            set
            {
                if (value != null)
                {
                    _ProCategory = value;
                }
                else
                {
                    _ProCategory = "Unknown";
                }
            }
        }
        static void Main(string[] args)
        {
            Program product1 = new Program { ProCode = 100, ProName = "Tooth Paste", ExpiryDate = DateTime.Parse("04-03-2023"), ProCategory = null };
            Console.WriteLine($"{product1.ProCode}\t{product1.ProName}\t{product1.ProCategory}");
        }
    }
}
