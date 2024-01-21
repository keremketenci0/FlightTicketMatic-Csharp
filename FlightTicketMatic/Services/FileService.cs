using System;
using System.IO;
using System.Linq;
using System.Diagnostics.Metrics;
using FlightTicketMatic.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Xml;


namespace FlightTicketMatic.Services
{
    public class FileService
    {
        public static bool IsFileEmpty(string filePath)
        {
            return new FileInfo(filePath).Length == 0;
        }

        public static string CreateFolder(string folderName)
        {
            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string projectDirectory = Directory.GetParent(currentDirectory).Parent.Parent.Parent.FullName;
            string folderPath = Path.Combine(projectDirectory, folderName);

            Directory.CreateDirectory(folderPath);

            return folderPath;
        }

        public static string GetFolderPath(string folderName)
        {
            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string projectDirectory = Directory.GetParent(currentDirectory).Parent.Parent.Parent.FullName;
            string folderPath = Path.Combine(projectDirectory, folderName);

            return folderPath;
        }

        public static string GetFilePath(string fileName)
        {
            string folderPath = GetFolderPath("Data");
            string filePath = folderPath + fileName;

            return filePath;
        }

        public static void SaveData(object data, string x_fileName)
        {
            string folderPath = CreateFolder("Data");

            string fileName = x_fileName + ".json";
            string filePath = Path.Combine(folderPath, fileName);

            List<object> dataList;

            if (File.Exists(filePath) && IsFileEmpty(filePath))
            {
                dataList = new List<object>();
                string existingData = "[\n" +
                    "]";
                dataList = JsonConvert.DeserializeObject<List<object>>(existingData);
            }

            else if (File.Exists(filePath))
            {
                string existingData = File.ReadAllText(filePath);
                dataList = JsonConvert.DeserializeObject<List<object>>(existingData);
            }
            else
            {
                dataList = new List<object>();
            }

            dataList.Add(data);

            string jsonData = JsonConvert.SerializeObject(dataList, Newtonsoft.Json.Formatting.Indented);

            File.WriteAllText(filePath, jsonData);
        }

        static void CreateJsonFile(string filePath)
        {
            File.WriteAllText(filePath, "[]");
            Console.WriteLine("Yeni JSON dosyası oluşturuldu: " + filePath);
        }

        public static void AddFlight(string countryName, string cityName, string airportName, string airlineName, string flightId, string planeNo, string date, string time, string capacity)
        {
            string jsonFilePath = GetFilePath("\\Flights.json");

            // Eğer dosya mevcut değilse oluştur
            if (!File.Exists(jsonFilePath))
            {
                CreateJsonFile(jsonFilePath);
            }

            // JSON dosyasını oku
            string jsonContent = File.ReadAllText(jsonFilePath);

            // JSON içeriğini deserializasyon
            List<Country> locations = JsonConvert.DeserializeObject<List<Country>>(jsonContent) ?? new List<Country>();

            // Belirtilen ülke var mı kontrol et, yoksa ekleyerek devam et
            Country country = locations.Find(c => c.CountryName == countryName);
            if (country == null)
            {
                country = new Country { CountryName = countryName, Cities = new List<City>() };
                locations.Add(country);
            }

            // Belirtilen şehir var mı kontrol et, yoksa ekleyerek devam et
            City city = country.Cities?.Find(c => c.CityName == cityName);
            if (city == null)
            {
                city = new City { CityName = cityName, Airports = new List<Airport>() };
                country.Cities.Add(city);
            }

            // Belirtilen havaalanı var mı kontrol et, yoksa ekleyerek devam et
            Airport airport = city.Airports?.Find(a => a.AirportName == airportName);
            if (airport == null)
            {
                airport = new Airport { AirportName = airportName, Airlines = new List<Airline>() };
                city.Airports.Add(airport);
            }

            // Belirtilen havayolu var mı kontrol et, yoksa ekleyerek devam et
            Airline airline = airport.Airlines?.Find(al => al.AirlineName == airlineName);
            if (airline == null)
            {
                airline = new Airline { AirlineName = airlineName, Flights = new List<Flight>() };
                airport.Airlines.Add(airline);
            }

            // Belirtilen uçuşu ekleyerek devam et
            airline.Flights.Add(new Flight
            {
                FlightId = flightId,
                PlaneNo = planeNo,
                Date = date,
                Time = time,
                Capacity = capacity
            });

            // Verileri JSON formatına çevir
            string updatedJsonContent = JsonConvert.SerializeObject(locations, Newtonsoft.Json.Formatting.Indented);

            // JSON dosyasını güncelle
            File.WriteAllText(jsonFilePath, updatedJsonContent);

            Console.WriteLine("Uçuş başarıyla eklendi ve JSON dosyası güncellendi.");
        }

        public static void EditFlight(string targetFlightId)
        {
            string jsonFilePath = GetFilePath("\\Flights.json");

            string jsonContent = File.ReadAllText(jsonFilePath);

            JArray jsonArray = JArray.Parse(jsonContent);

            List<JToken> flights = new List<JToken>();

            foreach (var country in jsonArray)
            {
                foreach (var city in country["Cities"])
                {
                    foreach (var airport in city["Airports"])
                    {
                        foreach (var airline in airport["Airlines"])
                        {
                            foreach (var flight in airline["Flights"])
                            {
                                if (flight["FlightId"].ToString() == targetFlightId)
                                {
                                    int currentCapacity = int.Parse(flight["Capacity"].ToString());
                                    flight["Capacity"] = (currentCapacity - 1).ToString();
                                }
                            }
                        }
                    }
                }
            }
            File.WriteAllText(jsonFilePath, jsonArray.ToString());
        }

        public static List<T> LoadDataFromJsonFile<T>(string filePath)
        {
            try
            {
                string jsonData = File.ReadAllText(filePath);
                List<T> models = JsonConvert.DeserializeObject<List<T>>(jsonData);
                return models;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading JSON file: {ex.Message}");
                return null;
            }
        }

        public static List<City> GetCitiesByCountry(string selectedCountry, List<Country> countries)
        {
            Country selectedCountryObject = countries.Find(country => country.CountryName.Equals(selectedCountry, StringComparison.OrdinalIgnoreCase));

            return selectedCountryObject?.Cities ?? new List<City>();
        }

        public static List<Airport> GetAirportsByCity(string selectedCity, List<City> cities)
        {
            City selectedCityObject = cities.Find(city => city.CityName.Equals(selectedCity, StringComparison.OrdinalIgnoreCase));

            return selectedCityObject?.Airports ?? new List<Airport>();
        }

        public static List<Airline> GetAirlinesByAirport(string selectedAirport, List<Airport> airports)
        {
            Airport selectedAirportObject = airports.Find(airport => airport.AirportName.Equals(selectedAirport, StringComparison.OrdinalIgnoreCase));

            return selectedAirportObject?.Airlines ?? new List<Airline>();
        }

        public static List<Flight> GetFlightsByAirline(string selectedAirline, List<Airline> airlines)
        {
            Airline selectedAirlineObject = airlines.Find(airline => airline.AirlineName.Equals(selectedAirline, StringComparison.OrdinalIgnoreCase));

            return selectedAirlineObject?.Flights ?? new List<Flight>();
        }
    }
}
