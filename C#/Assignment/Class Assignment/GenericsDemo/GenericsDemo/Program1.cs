using System;

namespace GenericsDemo
{
    class GenericClass<T, T1>
    {
        T a;
        T1 b;

        public GenericClass(T a, T1 b)
        {
            this.a = a;
            this.b = b;
        }

        public void Display()
        {
            Console.WriteLine("The First value: {0}", a);
            Console.WriteLine("The Second value: {0}", b);
        }
    }

    class Program1
    {
        static void Main(string[] args)
        {
            GenericClass<int, string> iobj = new GenericClass<int, string>(10, "Hi");
            GenericClass<string, char> sobj = new GenericClass<string, char>("Geetha", 'S');
            iobj.Display();
            sobj.Display();
            Console.ReadKey();
        }
    }
}
