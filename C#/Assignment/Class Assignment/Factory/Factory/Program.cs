 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory
{
    public interface IButton
    {
        void Paint();
    }

    public interface ICheckbox
    {
        void Paint();
    }

    public class WinButton : IButton
    {
        public void Paint()
        {
            Console.WriteLine("WinButton painted.");
        }
    }

    public class WinCheckbox : ICheckbox
    {
        public void Paint()
        {
            Console.WriteLine("WinCheckbox painted.");
        }
    }

    public class MacButton : IButton
    {
        public void Paint()
        {
            Console.WriteLine("MacButton painted.");
        }
    }

    public class MacCheckbox : ICheckbox
    {
        public void Paint()
        {
            Console.WriteLine("MacCheckbox painted.");
        }
    }

    public interface IGUIFactory
    {
        IButton CreateButton();
        ICheckbox CreateCheckbox();
    }

    public class WinFactory : IGUIFactory
    {
        public IButton CreateButton()
        {
            return new WinButton();
        }
        public ICheckbox CreateCheckbox()
        {
            return new WinCheckbox();
        }
    }

    public class MacFactory : IGUIFactory
    {
        public IButton CreateButton()
        {
            return new MacButton();
        }
        public ICheckbox CreateCheckbox()
        {
            return new MacCheckbox();
        }
    }

    public class Application
    {
        private readonly IButton _button;
        private readonly ICheckbox _checkbox;
        public Application(IGUIFactory factory)
        {
            _button = factory.CreateButton();
            _checkbox = factory.CreateCheckbox();
        }
        public void Paint()
        {
            _button.Paint();
            _checkbox.Paint();
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            IGUIFactory factory = null;
            string os = Environment.OSVersion.Platform.ToString();

            if (os.Contains("Win"))
            {
                factory = new WinFactory();
            }
            else if (os.Contains("Mac"))
            {
                factory = new MacFactory();
            }
            if (factory != null)
            {
                Application app = new Application(factory);
                app.Paint();
            }
            else
            {
                Console.WriteLine("Unsupported OS.");
            }
        }
    }
}
