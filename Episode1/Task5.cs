using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Episode1
{
    //Dependency Inversion Principle 'a uygun olmayan kod
    public class EmailService
    {
        public void Send(string message)
        {
            Console.WriteLine($"Email sent with message: {message}");
        }
    }

    public class OrderProcessing
    {
        private EmailService _emailService;

        public OrderProcessing(EmailService emailService)
        {
            _emailService =emailService;
        }

        public void ProcessOrder()
        {
            Console.WriteLine("Order processed.");
            _emailService.Send("Your order has been processed.");
        }
    }
    //Dependency Inversion Principle 'a uygun olan kod
    public interface INotificationService
    {
        void Send(string message);
    }
    public class EmailService1 : INotificationService
    {
        public void Send(string message)
        {
            Console.WriteLine($"Email sent with message: {message}");
        }
    }
    public class OrderProcessing1
    {
        private readonly INotificationService _notificationService;

        public OrderProcessing1(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public void ProcessOrder()
        {
            Console.WriteLine("Order processed.");
            _notificationService.Send("Your order has been processed.");
        }
    }
}

