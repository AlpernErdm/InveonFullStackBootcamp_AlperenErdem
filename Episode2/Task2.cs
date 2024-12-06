using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Episode2
{
    public class TaskExamples
    {
        /*
            RunTask: Task.Run metodunu kullanarak bir iş parçacığında çalışması gereken işlemleri sıraya koyar ve çalıştırır.

            DelayTask: Task.Delay metodunu kullanarak belirli bir süre geçtikten sonra tamamlanan bir görevi simüle eder.

            WhenAllTasks: Task.WhenAll metodu, bir dizi görevin tamamlanmasını bekler.

            WhenAnyTask: Task.WhenAny metodu, bir dizi görevden herhangi birinin tamamlanmasını bekler.

            FromResultTask: Task.FromResult metodu, belirtilen sonucun döndüğü tamamlanmış bir görevi oluşturur.

            YieldTask: Task.Yield metodu, mevcut bağlamda işlemciyi başka görevlerin çalışmasına izin vermek için serbest bırakır
         */
        public async Task RunTask()
        {
            await Task.Run(() => {
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine($"Running Task {i}");
                    Task.Delay(1000).Wait();
            });
            Console.WriteLine("Task.Run completed.");
        }

        public async Task DelayTask()
        {
            await Task.Delay(3000);
            Console.WriteLine("3 seconds have passed.");
        }

        public async Task WhenAllTasks()
        {
            Task task1 = Task.Run(() => Task.Delay(2000)); 
            Task task2 = Task.Run(() => Task.Delay(3000)); 

            await Task.WhenAll(task1, task2);
            Console.WriteLine("All tasks completed.");
        }

        public async Task WhenAnyTask()
        {
            Task task1 = Task.Run(() => Task.Delay(2000)); 
            Task task2 = Task.Run(() => Task.Delay(3000)); 

            await Task.WhenAny(task1, task2);
            Console.WriteLine("At least one task completed.");
        }

        public async Task FromResultTask()
        {
            Task<int> task = Task.FromResult(42);
            int result = await task;
            Console.WriteLine($"Result: {result}");
        }

        public async Task YieldTask()
        {
            for (int i = 0; i < 3; i++)
            {
                await Task.Yield();
                Console.WriteLine($"Yielded i: {i}");
            }
        }

    }
}
