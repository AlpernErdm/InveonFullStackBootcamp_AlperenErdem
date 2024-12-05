using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Episode1
{
    public class Task1
    {
        //Single Responsibility Principle'a aykırı durum :
        public class Shopping()
        {
            public double Price { get; set; }

            public bool HasLimit()
            {
                return true;
            }
            public bool CompleteShopping()
            {
                return true;
            }
        }

        //Single Responsibility Principle'a uygun durum

        public delegate bool LimitCheck();
        public delegate bool ShoppingCompletion();

        public class Shopping1
        {
            public double Price1 { get; set; }
        }

        public class LimitControl
        {
            public bool HasLimit1()
            {
                return true;
            }
        }

        public class CreateShopping
        {
            public bool CompleteShopping()
            {
                return true;
            }
        }

    }
}
