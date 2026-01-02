using MiniApp.Data.Context;
using MiniApp.Data.Entities;
using MiniApp.DTOs;
using MiniApp.Validation;


Console.WriteLine("Restaurant + Table + Reservation yaradilir");

// ===== RESTAURANT =====
Console.Write("Restaurant adi: ");
var rName = Console.ReadLine();

Console.Write("Seher: ");
var rCity = Console.ReadLine();

var restaurantDto = new CreateRestaurantRequest
{
    Name = rName!,
    City = rCity!
};

var rValidator = new CreateRestaurantRequestValidation();
if (!rValidator.Validate(restaurantDto).IsValid)
{
    foreach (var error in rValidator.Validate(restaurantDto).Errors)
    {
        Console.WriteLine(error.ErrorMessage);
    }
    return;
}

var context = new AppDbContext();
var restaurant = new Restaurant { Name = restaurantDto.Name, City = restaurantDto.City };
context.Restaurants.Add(restaurant);
context.SaveChanges();

// ===== DINING TABLE =====
Console.Write("Masa nomresi: ");
var tableNo = Console.ReadLine();

Console.Write("Capacity: ");
var capacity = int.Parse(Console.ReadLine()!);

var tableDto = new CreateDiningTableRequest
{
    RestaurantId = restaurant.Id,
    DiningTableNumber = tableNo!,
    Capacity = capacity
};

var tValidator = new CreateDiningTableRequestValidation();
if (!tValidator.Validate(tableDto).IsValid)
{
    foreach (var error in tValidator.Validate(tableDto).Errors)
    {
        Console.WriteLine(error.ErrorMessage);
    }
    return;
}

var table = new DiningTable
{
    RestaurantId = restaurant.Id,
    DiningTableNumber = tableDto.DiningTableNumber,
    Capacity = tableDto.Capacity
};
context.DiningTables.Add(table);
context.SaveChanges();

// ===== RESERVATION =====
Console.Write("Customer adi: ");
var customer = Console.ReadLine();

Console.Write("Guest count: ");
var guests = int.Parse(Console.ReadLine()!);

Console.Write("Reservation date : ");
var date = DateTime.Parse(Console.ReadLine()!);

var reservationDto = new CreateReservationRequest
{
    RestaurantId = restaurant.Id,
    DiningTableId = table.Id,
    CustomerName = customer!,
    GuestCount = guests,
    ReservationDate = date
};

var resValidator = new CreateReservationRequestValidation();
if (!resValidator.Validate(reservationDto).IsValid)
{
    foreach (var error in resValidator.Validate(reservationDto).Errors)
    {
        Console.WriteLine(error.ErrorMessage);
    }
    return;
}

var reservation = new Reservation
{
    RestaurantId = restaurant.Id,
    DiningTableId = table.Id,
    CustomerName = reservationDto.CustomerName,
    GuestCount = reservationDto.GuestCount,
    ReservationDate = reservationDto.ReservationDate
};

context.Reservations.Add(reservation);
context.SaveChanges();

Console.WriteLine("Her 3-u ugurla yaradildi.");
