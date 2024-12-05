using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Episode1
{
    //Open Closed Principle'a aykırı durum :
    public class Shape
    {
        public string Type { get; set; } = default!;
        public double Widht { get; set; }
        public double Height { get; set; }

        public double Calculater()
        {
            if (Type == "Rectangle")
            {
                return Widht * Height;
            }
            else if (Type == "Circle")
            {
                return Math.PI * Math.Pow(Widht, 2);
            }
            //Yeni bir şekil eklendiğinde metodu değiştirmek zorundayız
            return 0;
        }
    }

    public delegate double CalculateArea();

    public interface IShape
    {
        public double Calculate();
    }

    public class Rectangle : IShape
    {
        public double Width { get; set; }
        public double Height { get; set; }

        public double Calculate()
        {
            return Width * Height;
        }
    }

    public class Square : IShape
    {
        public double Height { get; set; }

        public double Calculate()
        {
            return Height * 4;
        }
    }
}
