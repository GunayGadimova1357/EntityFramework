using ef2.Data;
using ef2.Models;
using Microsoft.EntityFrameworkCore;


using var context = new AppDbContextFactory().CreateDbContext(args);

var dealer = new Dealer { Name = "AutoLux", Location = "Moscow" };

if (!context.Cars.Any(c => c.Make == "BMW" && c.Model == "X5"))
{
    var car = new Car { Make = "BMW", Model = "X5", Year = 2023, Dealer = dealer };
    var customer = new Customer { FullName = "Ivan Ivanov" };
    context.AddRange(dealer, car, customer);
    context.SaveChanges();
}


using var transaction = context.Database.BeginTransaction();
try
{
    var car2 = new Car { Make = "Audi", Model = "A6", Year = 2022, Dealer = dealer };
    var customer = context.Customers.First();
    var order = new CarOrder { Car = car2, Customer = customer };
    context.AddRange(car2, order);
    context.SaveChanges();
    transaction.Commit();
}
catch
{
    transaction.Rollback();
    Console.WriteLine("Error executing transaction");
}

var dealers = context.Dealers.Include(d => d.Cars).ToList();
foreach (var d in dealers)
    Console.WriteLine($"Dealer: {d.Name} | Cars: {string.Join(", ", d.Cars.Select(c => c.Make + " " + c.Model))}");

var oneCar = context.Cars.First();
context.Entry(oneCar).Reference(c => c.Dealer).Load();
Console.WriteLine($"[Explicit] Car: {oneCar.Make} | Dealer: {oneCar.Dealer.Name}");

var lazyCar = context.Cars.First();
Console.WriteLine($"[Lazy] Car: {lazyCar.Make} | Dealer: {lazyCar.Dealer.Name}");

var bmwCars = context.Cars.Where(c => c.Make == "BMW").ToList();
Console.WriteLine($"BMW found: {bmwCars.Count}");

void AddCar() =>
    context.Cars.Add(new Car { Make = "Ford", Model = "Focus", Year = 2020, Dealer = dealer });

void UpdateCar()
{
    var c = context.Cars.First();
    c.Color = "Red";
}

void DeleteCarSoft(int id)
{
    var car = context.Cars.Find(id);
    if (car != null)
        car.IsDeleted = true;
}

void ListCars() =>
    context.Cars.ToList().ForEach(c =>
        Console.WriteLine($"[{c.Id}] {c.Make} {c.Model} ({c.Year})"));

AddCar();
UpdateCar();
DeleteCarSoft(oneCar.Id);
ListCars();

context.SaveChanges();