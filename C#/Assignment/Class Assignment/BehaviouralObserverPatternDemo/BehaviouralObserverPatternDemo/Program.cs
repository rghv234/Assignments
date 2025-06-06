using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BehaviouralObserverPatternDemo
{
    public interface IObserver
    {
        void Update(decimal price);
    }

    public interface ISubject
    {
        void RegisterObserver(IObserver observer);
        void RemoveObserver(IObserver observer);
        void NotifyObservers();
    }

    public class Stock : ISubject
    {
        private List<IObserver> observers = new List<IObserver>();
        private decimal price;
        
        public void NotifyObservers()
        {
            foreach (var observer in observers)
            {
                observer.Update(price);
            }
        }
        public void RegisterObserver(IObserver observer) => observers.Add(observer);
        public void RemoveObserver(IObserver observer) => observers.Remove(observer);

        public void SetPrice(decimal newPrice)
        {
            price = newPrice;
            NotifyObservers();
        }


    }

    public class WebApp:IObserver
    {
        public void Update(decimal price)
        {
            Console.WriteLine($"WebApp: Stock price updated to {price}");
        }
    }
    public class MobileApp : IObserver
    {
        public void Update(decimal price)
        {
            Console.WriteLine($"MobileApp: Stock price updated to {price}");
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Stock stock = new Stock();
            WebApp webApp = new WebApp();
            MobileApp mobileApp = new MobileApp();
            stock.RegisterObserver(webApp);
            stock.RegisterObserver(mobileApp);
            stock.SetPrice(100.50m);
            stock.SetPrice(101.75m);
            stock.RemoveObserver(webApp);
            stock.SetPrice(102.00m);
        }
    }


}
