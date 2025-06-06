using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BehaviouralMediatorPatternDemo
{

    public interface IChatMediator
    {
        void SendMessage(string message, User user);
        void AddUser(User user);
    }
    public class User
    {
    public string Name { get; }
        private IChatMediator _chatMediator;
        public User(string name, IChatMediator chatMediator)
        {
            Name = name;
            _chatMediator = chatMediator;
        }
        public void Send(string message)
        {
            Console.WriteLine($"{Name} sends: {message}");
            _chatMediator.SendMessage(message, this);
        }
        public void Receive(string message)
        {
            Console.WriteLine($"{Name} receives: {message}");
        }
    }

    public class ChatRoom : IChatMediator
    {
        private List<User> _users = new List<User>();

        public void AddUser(User user)
        {
            _users.Add(user);
        }

        public void SendMessage(string message, User sender)
        {
            foreach (var user in _users)
            {
                if (user != sender)
                {
                    user.Receive(message);
                }
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            IChatMediator chatRoom = new ChatRoom();
            User tom = new User("Tom", chatRoom);
            User pam = new User("Pam", chatRoom);
            User fransy = new User("Fransy", chatRoom);

            chatRoom.AddUser(tom);
            chatRoom.AddUser(pam);
            chatRoom.AddUser(fransy);
            tom.Send("Hello Everyone");
            pam.Send("Hey from Pam");

            Console.ReadLine();
        }
    }
}
