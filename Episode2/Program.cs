using Episode2;

//SynchronousExample example = new SynchronousExample();
//example.PerformLongOperation();

//AsynchronousExample example1 = new AsynchronousExample();
//await example1.PerformLongOperationAsync();

//TaskExamples examples = new TaskExamples();

//Console.WriteLine("Running RunTask example:");
//await examples.RunTask();
//Console.WriteLine();

//Console.WriteLine("Running DelayTask example:");
//await examples.DelayTask();
//Console.WriteLine();

//Console.WriteLine("Running WhenAllTasks example:");
//await examples.WhenAllTasks();
//Console.WriteLine();

//Console.WriteLine("Running WhenAnyTask example:");
//await examples.WhenAnyTask();
//Console.WriteLine();

//Console.WriteLine("Running FromResultTask example:");
//await examples.FromResultTask();
//Console.WriteLine();

//Console.WriteLine("Running YieldTask example:");
//await examples.YieldTask();
//Console.WriteLine();

AsyncAwaitExample example2 = new AsyncAwaitExample();

string url = "https://jsonplaceholder.typicode.com/posts";

string data = await example2.FetchDataFromUrlAsync(url);

if (data != null)
{
    Console.WriteLine($"Fetched Data: {data.Substring(0, 100)}..."); 
}
else
{
    Console.WriteLine("No data fetched.");
}