using Episode1;
using static Episode1.Task1;


Shopping1 shopping = new Shopping1 { Price1 = 100.0 };

LimitControl limitControl = new LimitControl();
LimitCheck limitCheckDelegate = new LimitCheck(limitControl.HasLimit1);

CreateShopping createShopping = new CreateShopping();
ShoppingCompletion shoppingCompletionDelegate = new ShoppingCompletion(createShopping.CompleteShopping);

Console.WriteLine($"Shopping has limit: {limitCheckDelegate()}");
Console.WriteLine($"Shopping complete: {shoppingCompletionDelegate()}");
Console.WriteLine("************************");


Rectangle rectangle = new Rectangle { Width = 5, Height = 7 };
CalculateArea rectangleDelegate = new CalculateArea(rectangle.Calculate);

Square square = new Square { Height = 4 };
CalculateArea squareDelegate = new CalculateArea(square.Calculate);

Console.WriteLine($"Rectangle Area: {rectangleDelegate()}");
Console.WriteLine($"Square Perimeter: {squareDelegate()}");
Console.WriteLine("************************");

Sparrow1 sparrow = new Sparrow1();
sparrow.Fly();
Penguin1 penguin = new Penguin1();
penguin.MakeSound();
Console.WriteLine("************************");


IVehicle car = new Car();
car.Drive();
car.Navigate();

IVehicle airplane = new Airplane();
airplane.Fly();
airplane.Navigate();
Console.WriteLine("************************");


INotificationService emailService = new EmailService1();
OrderProcessing1 orderProcessing = new OrderProcessing1(emailService);
orderProcessing.ProcessOrder();