using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WeatherApp.Services
{
    public class PixabayService
    {
        private const string ApiKey = "47451236-c793e4ac060f56f6e64f9ab19";
        private const string BaseUrl = "https://pixabay.com/api/";

        public async Task<string> GetCityImageAsync(string cityName)
        {
            string requestUrl = $"{BaseUrl}?key={ApiKey}&q={cityName}&image_type=photo";

            using HttpClient client = new HttpClient();
            var response = await client.GetAsync(requestUrl);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                dynamic data = JsonConvert.DeserializeObject(json);

                if (data.hits.Count > 0)
                {
                    return data.hits[0].webformatURL; // URL
                }
                else
                {
                    return "No images found for the specified city.";
                }
            }
            else
            {
                throw new Exception($"Error fetching data: {response.ReasonPhrase}");
            }
        }
    }
}
