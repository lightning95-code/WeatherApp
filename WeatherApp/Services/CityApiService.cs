
public class CityApiService
{
    public Task<List<string>> GetCitiesAsync(string query)
    {
        var cities = new List<string>
        {
            "Tokyo", "Paris", "London", "Berlin", "Madrid", "Rome", "Kiev", "Washington",
            "Beijing", "Canberra", "Ottawa", "New Delhi", "Brasília", "Cairo", "Berlin", "Buenos Aires",
            "Seoul", "Mexico City", "Ottawa", "Jakarta", "Athens", "Helsinki", "Vienna", "Kuala Lumpur",
            "Riyadh", "Copenhagen", "Abuja", "Oslo", "Bucharest", "Warsaw", "Lima"
        };

     
        var filteredCities = cities.FindAll(city => city.ToLower().Contains(query.ToLower()));

        return Task.FromResult(filteredCities);
    }
}
