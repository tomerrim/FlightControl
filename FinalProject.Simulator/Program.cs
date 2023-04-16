using FinalProject.Simulator.DTO;
using System.Net.Http.Json;
using Timer = System.Timers.Timer;

var client = new HttpClient { BaseAddress = new Uri("http://localhost:5059") };

string[] brands = { "Arkia", "Brussels Airlines", "Emirates", "El-Al" };

var timer = new Timer(10000);
timer.Elapsed += async (s, e) => await CreateFlight();
timer.Start();

Console.ReadLine();

async Task CreateFlight()
{
    var flight = new FlightDto { Brand = GetRandomBrand(brands), PassengersCount = GetRandomPassengers() };
    var response = await client.PostAsJsonAsync("api/flights", flight);
    if (response.IsSuccessStatusCode)
        await Console.Out.WriteLineAsync($"{flight.Brand} Airplane with {flight.PassengersCount} passengers took off from Leg 4 on time: {DateTime.Now}");
}

static string GetRandomBrand(string[] brands)
{
    Random random = new Random();
    int randomIndex = random.Next(brands.Length);
    return brands[randomIndex];
}

static int GetRandomPassengers()
{
    Random random = new Random();
    int randomIndex = random.Next(100, 301);
    return randomIndex;
}
