using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class MobilePhone
    {
        public int ModelNo { get; set; } 
        public string ModelName { get; set; }
        public int Ram { get; set; }
        public int InternalStorage { get; set; }

        public MobilePhone()
        {
            Console.WriteLine("Default constructor of MobilePhone");
        }

        public MobilePhone(int ModelNo, string ModelName, int Ram, int InternalStorage)
        {
            this.ModelNo = ModelNo;
            this.ModelName = ModelName;
            this.Ram = Ram;
            this.InternalStorage = InternalStorage;
        }

        public string Calling()
        {
            return "Ring";
        }

        public string SendSMS()
        {
            return "Message Sent";
        }

        public string Details()
        {
            return "Mobile details";
        }
    }

    internal class Execute
    {
        static void Main()
        {
            //create obj using default constructor
            MobilePhone mobilePhone1 = new MobilePhone();

            mobilePhone1.ModelNo = 234;
            mobilePhone1.ModelName = "NOKIA1100";
            mobilePhone1.Ram = 4;
            mobilePhone1.InternalStorage = 4;


            Console.WriteLine(mobilePhone1.Calling());

            Console.WriteLine(mobilePhone1.SendSMS());

            //create obj using parameterized constructor
            MobilePhone mobilePhone2 = new MobilePhone(123, "NOKIA1100", 2, 4);
            Console.WriteLine(mobilePhone2.Details());
        }
    }
}
