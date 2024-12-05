using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Episode1
{
    //Interface Segregation Principle'e aykırı olan kod
    public interface IVehicle
    {
        void Drive();
        void Fly();
        void Navigate();
    }

    public class Car : IVehicle
    {
        public void Drive()
        {
            Console.WriteLine("Car is driving.");
        }

        public void Fly()
        {
            //   throw new NotImplementedException();
        }

        public void Navigate()
        {
            Console.WriteLine("Car is navigating.");
        }
    }

    public class Airplane : IVehicle
    {
        public void Drive()
        {
            //  throw new NotImplementedException();
        }

        public void Fly()
        {
            Console.WriteLine("Airplane is flying.");
        }

        public void Navigate()
        {
            Console.WriteLine("Airplane is navigating.");
        }
    }

    //Interface Segregation Principle'e uygun olan kod

    public interface IDrive
    {
        void Drive();
    }
    public interface IFly
    {
        void Fly();
    }
    public interface INavigate
    {
        void Navigate();
    }
    public class Car1 : IDrive, INavigate
    {
        public void Drive()
        {
            Console.WriteLine("Car is driving.");
        }

        public void Navigate()
        {
            Console.WriteLine("Car is navigating.");
        }
    }
    public class Airplane1 : IFly, INavigate
    {
        public void Fly()
        {
            Console.WriteLine("Airplane is flying.");
        }

        public void Navigate()
        {
            Console.WriteLine("Airplane is navigating.");
        }
    }

}
