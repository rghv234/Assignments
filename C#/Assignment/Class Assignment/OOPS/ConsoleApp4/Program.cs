using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    internal class Program
    {
        public class Shape
        {
            public double Area { get; set; }

            public virtual void CalculateArea()
            {
                Console.WriteLine("area of shape: ");
            }
        }

        public class Rectangle : Shape
        {
            public double Width { get; set; }
            public double Height { get; set; }

            public override void CalculateArea()
            {
                Area = Width * Height;
                Console.WriteLine("calculating area of rectangle... ");
            }
        }

        public class Circle : Shape
        {
            public double Radius { get; set; }

            public override void CalculateArea()
            {
                Area = Math.PI * Radius * Radius;
                Console.WriteLine("calculating area of circle... ");
            }
        }

        public static void Main(string[] args)
        {
            Rectangle rec = new Rectangle();
            rec.Height = 100;   
            rec.Width = 200;
            rec.CalculateArea();
            Console.WriteLine("area of rectangle: " + rec.Area);

            Circle cir = new Circle();
            cir.Radius = 100;   
            cir.CalculateArea();
            Console.WriteLine("area of circle: " + cir.Area); 
        }
    }
}
