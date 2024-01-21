using System.Linq;
using System.Security.Cryptography;
using FlightTicketMatic.Models;
using FlightTicketMatic.Services;
using Newtonsoft.Json;

namespace FlightTicketMatic
{
    class Program
    {

        public static int selected_action;

        public static int selected_country;
        public static string selected_country_string;

        public static int selected_city;
        public static string selected_city_string;

        public static int selected_airport;
        public static string selected_airport_string;

        public static int selected_airline;
        public static string selected_airline_string;

        public static int selected_flight;
        public static string selected_flight_string;

        public static int selected_seat;

        public static List<Country> countryList;
        public static List<City> cityList;
        public static List<Airport> airportList;
        public static List<Airline> airlineList;
        public static List<Flight> flightList;

        public static void Main(string[] args)
        {
            Reservation reservation = new Reservation();

            //FileService.AddFlight("Turkey", "İstanbul", "Sabiha Gökçen Havalimanı", "Pegasus", "1", "1", "01.01.2024", "01.00", "10");
            //FileService.AddFlight("Turkey", "İstanbul", "İstanbul Havalimanı", "Pegasus", "2", "2", "02.02.2024", "02.00", "20");


            //FileService.AddFlight("Turkey", "İstanbul", "Sabiha Gökçen Havalimanı", "THY", "3", "3", "03.03.2024", "03.00", "30");
            //FileService.AddFlight("Turkey", "İstanbul", "İstanbul Havalimanı", "THY", "4", "4", "04.04.2024", "04.00", "40");

            //FileService.AddFlight("Turkey", "İstanbul", "Ankara Esenboga Havalimanı", "Pegasus", "5", "5", "05.05.2024", "05.00", "50");
            //FileService.AddFlight("Turkey", "İstanbul", "Ankara Esenboga Havalimanı", "Pegasus", "6", "6", "06.06.2024", "06.00", "60");


            //FileService.AddFlight("Turkey", "Ankara", "Ankara Esenboga Havalimanı", "THY", "7", "7", "07.07.2024", "07.00", "70");
            //FileService.AddFlight("Turkey", "Ankara", "Ankara Esenboga Havalimanı", "THY", "8", "8", "08.08.2024", "08.00", "80");


            //FileService.AddFlight("USA", "New York", "John F. Kennedy International Airport", "THY", "9", "9", "09.09.2024", "09.00", "90");
            //FileService.AddFlight("USA", "New York", "John F. Kennedy International Airport", "THY", "10", "10", "10.10.2024", "10.00", "100");
            //FileService.AddFlight("USA", "New York", "John F. Kennedy International Airport", "THY", "11", "11", "11.11.2024", "11.00", "110");

            string filePath = FileService.GetFilePath("\\Flights.json");

            bool ok_action_selection = true;
            while (ok_action_selection)
            {
                try
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("1) Buy Ticket");
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("Type '-1' to shutdown the program");
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    selected_action = Convert.ToInt32(Console.ReadLine());
                    ok_action_selection = false;

                    if (selected_action == -1)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("Program is shutting down");
                        Environment.Exit(0);
                    }
                    else if (selected_action != 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("!!!!! Enter the number associated with the action !!!!!");
                        ok_action_selection = true;
                        continue;
                    }
                }
                catch (Exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("!!!!! You entered an invalid value !!!!!");
                    continue;
                }

                bool ok_country_selection = true;
                while (ok_country_selection)
                {
                    try
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;

                        countryList = FileService.LoadDataFromJsonFile<Country>(filePath);

                        if (countryList != null)
                        {
                            Console.WriteLine("Select the country you want to buy tickets for:");
                            Console.ForegroundColor = ConsoleColor.White;
                            for (int i = 0; i < countryList.Count; i++)
                            {
                                Console.WriteLine($"{i + 1}) {countryList[i].CountryName}");
                            }
                        }

                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("Type '0' to return to the action selection");
                        Console.WriteLine("Type '-1' to shutdown the program");
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        selected_country = Convert.ToInt32(Console.ReadLine());
                        ok_country_selection = false;

                        switch (selected_country)
                        {
                            case 0:
                                ok_action_selection = true;
                                continue;
                            case -1:
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                Console.WriteLine("Program is shutting down");
                                Environment.Exit(0);
                                break;
                        }

                        if (selected_country > countryList.Count)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("!!!!! Enter the number associated with the country !!!!!");
                            ok_country_selection = true;
                            continue;
                        }
                        else if (selected_country <= countryList.Count)
                        {
                            selected_country_string = selected_country.ToString();
                            selected_country_string = countryList[selected_country - 1].CountryName;
                        }
                    }
                    catch (Exception)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("!!!!! You entered an invalid value !!!!!");
                        continue;
                    }

                    bool ok_city_selection = true;
                    while (ok_city_selection)
                    {
                        try
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;

                            cityList = FileService.GetCitiesByCountry(selected_country_string, countryList);

                            if (cityList != null)
                            {
                                Console.WriteLine("Select the city you want to buy tickets for:");
                                Console.ForegroundColor = ConsoleColor.White;
                                for (int i = 0; i < cityList.Count; i++)
                                {
                                    Console.WriteLine($"{i + 1}) {cityList[i].CityName}");
                                }
                            }

                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("Type '0' to return to the country selection");
                            Console.WriteLine("Type '-1' to shutdown the program");
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            selected_city = Convert.ToInt32(Console.ReadLine());
                            ok_city_selection = false;

                            switch (selected_city)
                            {
                                case 0:
                                    ok_country_selection = true;
                                    continue;
                                case -1:
                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                    Console.WriteLine("Program is shutting down");
                                    Environment.Exit(0);
                                    break;
                            }

                            if (selected_city > cityList.Count)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("!!!!! Enter the number associated with the city !!!!!");
                                ok_city_selection = true;
                                continue;
                            }
                            else if (selected_city <= cityList.Count)
                            {
                                selected_city_string = selected_city.ToString();
                                selected_city_string = cityList[selected_city - 1].CityName;
                            }
                        }
                        catch (Exception)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("!!!!! You entered an invalid value !!!!!");
                            continue;
                        }

                        bool ok_airport_selection = true;
                        while (ok_airport_selection)
                        {
                            try
                            {
                                Console.ForegroundColor = ConsoleColor.Cyan;

                                airportList = FileService.GetAirportsByCity(selected_city_string, cityList);

                                if (airportList != null)
                                {
                                    Console.WriteLine("Select the airport you want to buy tickets for:");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    for (int i = 0; i < airportList.Count; i++)
                                    {
                                        Console.WriteLine($"{i + 1}) {airportList[i].AirportName}");
                                    }
                                }

                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                Console.WriteLine("Type '0' to return to the city selection");
                                Console.WriteLine("Type '-1' to shutdown the program");
                                Console.ForegroundColor = ConsoleColor.DarkCyan;
                                selected_airport = Convert.ToInt32(Console.ReadLine());
                                ok_airport_selection = false;

                                switch (selected_airport)
                                {
                                    case 0:
                                        ok_city_selection = true;
                                        continue;
                                    case -1:
                                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                                        Console.WriteLine("Program is shutting down");
                                        Environment.Exit(0);
                                        break;
                                }

                                if (selected_airport > airportList.Count)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("!!!!! Enter the number associated with the airport !!!!!");
                                    ok_airport_selection = true;
                                    continue;
                                }
                                else if (selected_airport <= airportList.Count)
                                {
                                    selected_airport_string = selected_airport.ToString();
                                    selected_airport_string = airportList[selected_airport - 1].AirportName;
                                }
                            }
                            catch (Exception)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("!!!!! You entered an invalid value !!!!!");
                                continue;
                            }

                            bool ok_airline_selection = true;
                            while (ok_airline_selection)
                            {
                                try
                                {
                                    Console.ForegroundColor = ConsoleColor.Cyan;

                                    airlineList = FileService.GetAirlinesByAirport(selected_airport_string, airportList);

                                    if (airlineList != null)
                                    {
                                        Console.WriteLine("Select the airline you want to buy tickets for:");
                                        Console.ForegroundColor = ConsoleColor.White;
                                        for (int i = 0; i < airlineList.Count; i++)
                                        {
                                            Console.WriteLine($"{i + 1}) {airlineList[i].AirlineName}");
                                        }
                                    }

                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                    Console.WriteLine("Type '0' to return to the airport selection");
                                    Console.WriteLine("Type '-1' to shutdown the program");
                                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                                    selected_airline = Convert.ToInt32(Console.ReadLine());
                                    ok_airline_selection = false;

                                    switch (selected_airline)
                                    {
                                        case 0:
                                            ok_airport_selection = true;
                                            continue;
                                        case -1:
                                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                                            Console.WriteLine("Program is shutting down");
                                            Environment.Exit(0);
                                            break;
                                    }

                                    if (selected_airline > airlineList.Count)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("!!!!! Enter the number associated with the airline !!!!!");
                                        ok_airline_selection = true;
                                        continue;
                                    }
                                    else if (selected_airline <= airlineList.Count)
                                    {
                                        selected_airline_string = selected_airline.ToString();
                                        selected_airline_string = airlineList[selected_airline - 1].AirlineName;
                                    }
                                }
                                catch (Exception)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("!!!!! You entered an invalid value !!!!!");
                                    continue;
                                }

                                bool ok_flight_selection = true;
                                while (ok_flight_selection)
                                {
                                    try
                                    {
                                        Console.ForegroundColor = ConsoleColor.Cyan;

                                        flightList = FileService.GetFlightsByAirline(selected_airline_string, airlineList);

                                        if (flightList != null)
                                        {
                                            Console.WriteLine("Select the flight you want to buy tickets for:");
                                            Console.ForegroundColor = ConsoleColor.White;
                                            for (int i = 0; i < flightList.Count; i++)
                                            {
                                                Console.WriteLine($"{i + 1})FlightId: {flightList[i].FlightId}");
                                                Console.WriteLine($"  PlaneNo: {flightList[i].PlaneNo}");
                                                Console.WriteLine($"  Date: {flightList[i].Date}");
                                                Console.WriteLine($"  Time: {flightList[i].Time}");
                                                Console.WriteLine($"  Capacity: {flightList[i].Capacity}\n");
                                            }
                                        }

                                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                                        Console.WriteLine("Type '0' to return to the airline selection");
                                        Console.WriteLine("Type '-1' to shutdown the program");
                                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                                        selected_flight = Convert.ToInt32(Console.ReadLine());
                                        ok_flight_selection = false;

                                        switch (selected_flight)
                                        {
                                            case 0:
                                                ok_airline_selection = true;
                                                continue;
                                            case -1:
                                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                                Console.WriteLine("Program is shutting down");
                                                Environment.Exit(0);
                                                break;
                                        }

                                        if (selected_flight > flightList.Count)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine("!!!!! Enter the number associated with the flight !!!!!");
                                            ok_flight_selection = true;
                                            continue;
                                        }
                                        else if (selected_flight <= flightList.Count)
                                        {
                                            selected_flight_string = selected_flight.ToString();
                                            selected_flight_string = flightList[selected_flight - 1].FlightId;
                                        }
                                    }
                                    catch (Exception)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("!!!!! You entered an invalid value !!!!!");
                                        continue;
                                    }

                                    bool ok_firstName_selection = true;
                                    while (ok_firstName_selection)
                                    {
                                        try
                                        {
                                            Console.ForegroundColor = ConsoleColor.Cyan;
                                            Console.WriteLine("Enter your First Name");
                                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                                            Console.WriteLine("Type '0' to return to the flight selection");
                                            Console.WriteLine("Type '-1' to shutdown the program");
                                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                                            reservation.FirstName = Console.ReadLine();
                                            ok_firstName_selection = false;

                                            switch (reservation.FirstName)
                                            {
                                                case "0":
                                                    ok_flight_selection = true;
                                                    continue;
                                                case "-1":
                                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                                    Console.WriteLine("Program is shutting down");
                                                    Environment.Exit(0);
                                                    break;
                                            }
                                        }
                                        catch (Exception)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine("!!!!! You entered an invalid value !!!!!");
                                            continue;
                                        }

                                        bool ok_lastName_selection = true;
                                        while (ok_lastName_selection)
                                        {
                                            try
                                            {
                                                Console.ForegroundColor = ConsoleColor.Cyan;
                                                Console.WriteLine("Enter your Last Name");
                                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                                Console.WriteLine("Type '0' to return to the First Name selection");
                                                Console.WriteLine("Type '-1' to shutdown the program");
                                                Console.ForegroundColor = ConsoleColor.DarkCyan;
                                                reservation.LastName = Console.ReadLine();
                                                ok_lastName_selection = false;

                                                switch (reservation.LastName)
                                                {
                                                    case "0":
                                                        ok_firstName_selection = true;
                                                        continue;
                                                    case "-1":
                                                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                                                        Console.WriteLine("Program is shutting down");
                                                        Environment.Exit(0);
                                                        break;
                                                }
                                            }
                                            catch (Exception)
                                            {
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.WriteLine("!!!!! You entered an invalid value !!!!!");
                                                continue;
                                            }

                                            bool ok_seat_selection = true;
                                            while (ok_seat_selection)
                                            {
                                                try
                                                {
                                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                                    Console.WriteLine("Which seat do you want to buy ?");
                                                    Console.WriteLine("Capacity of selected flight: " + flightList[selected_flight - 1].Capacity);
                                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                                    Console.WriteLine("Type '0' to return to the Last Name selection");
                                                    Console.WriteLine("Type '-1' to shutdown the program");
                                                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                                                    selected_seat = Convert.ToInt32(Console.ReadLine());
                                                    ok_seat_selection = false;

                                                    switch (selected_seat)
                                                    {
                                                        case 0:
                                                            ok_lastName_selection = true;
                                                            continue;
                                                        case -1:
                                                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                                                            Console.WriteLine("Program is shutting down");
                                                            Environment.Exit(0);
                                                            break;
                                                    }
                                                }
                                                catch (Exception)
                                                {
                                                    Console.ForegroundColor = ConsoleColor.Red;
                                                    Console.WriteLine("!!!!! You entered an invalid value !!!!!");
                                                    continue;
                                                }

                                                if (selected_seat > int.Parse(flightList[selected_flight - 1].Capacity))
                                                {
                                                    Console.ForegroundColor = ConsoleColor.Red;
                                                    Console.WriteLine("!!!!! There is no seat associated with that number !!!!!");
                                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                                    Console.WriteLine("Capacity of selected flight: " + flightList[selected_flight - 1].Capacity);
                                                    ok_seat_selection = true;
                                                    continue;
                                                }
                                                else if (selected_seat <= int.Parse(flightList[selected_flight - 1].Capacity))
                                                {
                                                    reservation.SeatNo = selected_seat;
                                                    reservation.flight.FlightId = flightList[selected_flight - 1].FlightId;
                                                    reservation.flight.PlaneNo = flightList[selected_flight - 1].PlaneNo;
                                                    reservation.flight.Date = flightList[selected_flight - 1].Date;
                                                    reservation.flight.Time = flightList[selected_flight - 1].Time;
                                                    int capacity = int.Parse(flightList[selected_flight - 1].Capacity) - 1;
                                                    reservation.flight.Capacity = capacity.ToString();

                                                    FileService.EditFlight(flightList[selected_flight - 1].FlightId);

                                                    FileService.SaveData(reservation, "reservations");
                                                    Console.ForegroundColor = ConsoleColor.Green;
                                                    Console.WriteLine(reservation);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}