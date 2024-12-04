using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Episode1
{
    public interface IBird
    {
        public void Eat();
        public void MakeSound();
        public void Fly();
    }
    public class Sparrow : IBird
    {
        public void Eat()
        {
            Console.WriteLine("Sparrow is eating..");
        }

        public void Fly()
        {
            Console.WriteLine("Sparrow is flying..");
        }

        public void MakeSound()
        {
            Console.WriteLine("Sparrow's sound");
        }
    }
    public class Penguin : IBird
    {
        public void Eat()
        {
            Console.WriteLine("Penguin is eating..");
        }
        public void Fly()
        {
            // throw new NotImplementedException();
        }
        public void MakeSound()
        {
            Console.WriteLine("Penguin's sound");
        }
    }

    //Liskov Substitution Principle' uygun olan kod
    public interface IBird1
    {
        void Eat();
        void MakeSound();
    }
    public interface IFlyableBird : IBird1
    {
        void Fly();
    }
    public class Sparrow1 : IFlyableBird
    {
        public void Eat()
        {
            Console.WriteLine("Sparrow is eating..");
        }

        public void Fly()
        {
            Console.WriteLine("Sparrow is flying..");
        }
        public void MakeSound()
        {
            Console.WriteLine("chirp..");
        }
    }
    public class Penguin1 : IBird1
    {
        public void Eat()
        {
            Console.WriteLine("Penguin is eating..");
        }

        public void MakeSound()
        {
            Console.WriteLine("Penguin's sound");
        }
    }
}
