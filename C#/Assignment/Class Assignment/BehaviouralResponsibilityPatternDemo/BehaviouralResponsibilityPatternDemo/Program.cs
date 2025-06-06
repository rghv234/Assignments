using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BehaviouralObserverPatternDemo
{
    public abstract class SupportHandler
    {
        protected SupportHandler nextHandler;
        public void SetNext(SupportHandler handler)
        {
            nextHandler = handler;
        }
        public abstract void HandleRequest(string request);
    }
    public class Level1Support : SupportHandler
    {
        public override void HandleRequest(string request)
        {
            if (request == "Level 1")
            {
                Console.WriteLine("Level 1 Support handling the request.");
            }
            else if (nextHandler != null)
            {
                nextHandler.HandleRequest(request);
            }
        }
    }

    public class Level2Support : SupportHandler
    {
        public override void HandleRequest(string request)
        {
            if (request == "Level 2")
            {
                Console.WriteLine("Level 2 Support handling the request.");
            }
            else if (nextHandler != null)
            {
                nextHandler.HandleRequest(request);
            }
        }
    }

    public class ManagerSupport : SupportHandler
    {
        public override void HandleRequest(string request)
        {
            if (request == "Manager")
            {
                Console.WriteLine("Manager handling the request.");
            }
            else if (nextHandler != null)
            {
                nextHandler.HandleRequest(request);
            }
            else
            {
                Console.WriteLine("Request could not be handled.");
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // Create support handlers
            var level1 = new Level1Support();
            var level2 = new Level2Support();
            var manager = new ManagerSupport();
            // Set up the chain of responsibility
            level1.SetNext(level2);
            level2.SetNext(manager);
            // Handle requests
            level1.HandleRequest("Level 1");
            level1.HandleRequest("Level 2");
            level1.HandleRequest("Manager");
        }
    }
}


