using MiniApp.Data.Context;
using MiniApp.Data.Entities;
using MiniApp.DTOs;
using MiniApp.DTOs.DiningTableDto;
using MiniApp.DTOs.ReservationDate;
using MiniApp.DTOs.RestaurantDto;
using MiniApp.Migrations;
using MiniApp.Validation;

 var _context = new AppDbContext();

while (true)
{
    Console.WriteLine("===== MAIN MENU =====");
    Console.WriteLine("1. Restaurant");
    Console.WriteLine("2. DiningTable");
    Console.WriteLine("3. Reservation");
    Console.WriteLine("0. Exit");

    Console.Write("Secim: ");
    var mainChoice = Console.ReadLine();

    if (mainChoice == "0")
        break;

    // ================= RESTAURANT MENU =================
    if (mainChoice == "1")
    {
        Console.WriteLine("1. Add restaurant");
        Console.WriteLine("2. List restaurants");
        Console.WriteLine("3. Delete restaurant");

        var choice = Console.ReadLine();

        if (choice == "1")
        {
            #region Restaurant CREATE
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
                    Console.WriteLine(error.ErrorMessage);
                continue;
            }

            var restaurant = new Restaurant
            {
                Name = restaurantDto.Name,
                City = restaurantDto.City
            };

            _context.Restaurants.Add(restaurant);
            _context.SaveChanges();
            #endregion
        }

        else if (choice == "2")
        {
            #region Restaurant LIST
            var restaurants = _context.Restaurants.ToList();
            foreach (var item in restaurants)
                Console.WriteLine($"{item.Id} -- {item.Name} in {item.City}");
            #endregion
        }

        else if (choice == "3")
        {
            #region Restaurant DELETE
            Console.Write("Restaurant ID daxil edin: ");
            var restaurantId = int.Parse(Console.ReadLine()!);

            var removable = _context.Restaurants.Find(restaurantId);
            if (removable is null)
            {
                Console.WriteLine("Restaurant tapilmadi.");
                continue;
            }

            _context.Restaurants.Remove(removable);
            _context.SaveChanges();
            Console.WriteLine("Restaurant silindi.");
            #endregion
        }
    }

    // ================= DINING TABLE MENU =================
    else if (mainChoice == "2")
    {
        Console.WriteLine("1. Add DiningTable");
        Console.WriteLine("2. List DiningTables by restaurant");

        var choice = Console.ReadLine();

        if (choice == "1")
        {
            #region DiningTable CREATE
            var restaurants = _context.Restaurants.ToList();
            foreach (var r in restaurants)
                Console.WriteLine($"{r.Id}. {r.Name}");

            Console.Write("Restaurant Id secin: ");
            var restaurantId = int.Parse(Console.ReadLine()!);

            Console.Write("Masa nomresi: ");
            var tableNo = Console.ReadLine();

            Console.Write("Capacity: ");
            var capacity = int.Parse(Console.ReadLine()!);

            var dto = new CreateDiningTableRequest
            {
                RestaurantId = restaurantId,
                DiningTableNumber = tableNo!,
                Capacity = capacity
            };

            var validator = new CreateDiningTableRequestValidation(_context);
            if (!validator.Validate(dto).IsValid)
            {
                foreach (var e in validator.Validate(dto).Errors)
                    Console.WriteLine(e.ErrorMessage);
                continue;
            }

            var table = new DiningTable
            {
                RestaurantId = dto.RestaurantId,
                DiningTableNumber = dto.DiningTableNumber,
                Capacity = dto.Capacity
            };

            _context.DiningTables.Add(table);
            _context.SaveChanges();
            #endregion
        }

        else if (choice == "2")
        {
            #region DiningTable LIST
            Console.Write("Restaurant Id daxil edin: ");
            var id = int.Parse(Console.ReadLine()!);

            var tables = _context.DiningTables
                .Where(dt => dt.RestaurantId == id)
                .ToList();

            foreach (var t in tables)
                Console.WriteLine($"{t.Id} - {t.DiningTableNumber} ({t.Capacity})");
            #endregion
        }
    }

    // ================= RESERVATION MENU =================
    else if (mainChoice == "3")
    {
        Console.WriteLine("1. Create reservation");
        Console.WriteLine("2. List reservations by restaurant");
        Console.WriteLine("3. Update reservation");
        Console.WriteLine("4. Cancel reservation");

        var choice = Console.ReadLine();

        if (choice == "1")
        {
            #region CREATE RESERVATION
            Console.Write("Restaurant Id: ");
            var restaurantId = int.Parse(Console.ReadLine()!);

            Console.Write("DiningTable Id: ");
            var diningTableId = int.Parse(Console.ReadLine()!);

            Console.Write("Customer name: ");
            var customerName = Console.ReadLine();

            Console.Write("Guest count: ");
            var guestCount = int.Parse(Console.ReadLine()!);

            Console.Write("Reservation date: ");
            var dateInput = Console.ReadLine();


            if (!DateTime.TryParse(dateInput, out DateTime reservationDate))
            {
                Console.WriteLine("Reservation date bos ola bilmez ve duzgun formatda olmalidir.");
                return; // və ya continue (menu varsa)
            }

            var dto = new CreateReservationRequest
            {
                RestaurantId = restaurantId,
                DiningTableId = diningTableId,
                CustomerName = customerName!,
                GuestCount = guestCount,
                ReservationDate = reservationDate
            };

            var validator = new CreateReservationRequestValidation();
            if (!validator.Validate(dto).IsValid)
            {
                foreach (var e in validator.Validate(dto).Errors)
                    Console.WriteLine(e.ErrorMessage);
                continue;
            }

            _context.Reservations.Add(new Reservation
            {
                RestaurantId = dto.RestaurantId,
                DiningTableId = dto.DiningTableId,
                CustomerName = dto.CustomerName,
                GuestCount = dto.GuestCount,
                ReservationDate = dto.ReservationDate
            });

            _context.SaveChanges();
            #endregion
        }

        else if (choice == "2")
        {
            #region LIST RESERVATIONS
            Console.Write("Restaurant Id: ");
            var rid = int.Parse(Console.ReadLine()!);

            if (!_context.Restaurants.Any(r => r.Id == rid))
            {
                Console.WriteLine("Restaurant tapilmadi.");
                continue;
            }


            var reservations = _context.Reservations
                .Where(r => r.RestaurantId == rid)
                .ToList();

            if(!reservations.Any())
            {
                Console.WriteLine($"Bu restaurantda hec bir reservation yoxdur");
            }
            else
            {

                foreach (var r in reservations)
                    Console.WriteLine($" Id:{r.Id} , CustomerName: {r.CustomerName} , Date: {r.ReservationDate}");
            }
            #endregion
        }

        else if (choice == "3")
        {
            #region UPDATE RESERVATION
            Console.Write("Reservation Id: ");
            var id = int.Parse(Console.ReadLine()!);

            var res = _context.Reservations.Find(id);
            if (res == null)
            {
                Console.WriteLine("Tapilmadi.");
                continue;
            }

            Console.Write("Yeni tarix: ");
            res.ReservationDate = DateTime.Parse(Console.ReadLine()!);

            Console.Write("Yeni guest count: ");
            res.GuestCount = int.Parse(Console.ReadLine()!);

            _context.SaveChanges();
            #endregion
        }

        else if (choice == "4")
        {
            #region CANCEL RESERVATION
            Console.Write("Reservation Id: ");
            var id = int.Parse(Console.ReadLine()!);

            var res = _context.Reservations.Find(id);
            if (res == null)
            {
                Console.WriteLine("Tapilmadi.");
                continue;
            }

            _context.Reservations.Remove(res);
            _context.SaveChanges();
            #endregion
        }
    }

    Console.WriteLine();
}






