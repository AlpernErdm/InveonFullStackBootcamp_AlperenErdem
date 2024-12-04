using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Episode2
{
    public class SynchronousExample
    {
        public void PerformLongOperation()
        {
            Console.WriteLine("Starting long operation...");
            Thread.Sleep(5000); // 5 saniye bekle
            Console.WriteLine("Long operation completed.");
        }
    }
    public class AsynchronousExample
    {
        public async Task PerformLongOperationAsync()
        {
            Console.WriteLine("Starting long operation...");
            await Task.Delay(5000); // 5 saniye bekle
            Console.WriteLine("Long operation completed.");
        }
    }
}
