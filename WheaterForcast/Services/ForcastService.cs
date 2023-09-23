using Newtonsoft.Json;
using WheaterForcast.Interfaces;
using WheaterForcast.Models;

namespace WheaterForcast.Services
{
    public class ForcastService : IFrocastService
    {

        public async Task<string> ForcastTitle(string query)
        {
            var result = await this.GetForcastAsync(query);
            return result != null ? $"the weatehr in london is: temp {result.Current.TempC} condition {result?.Current?.Condition?.Text}" : string.Empty;
        }

        public async Task<ForcastLogicModel> GetForcastAsync(string query, int days = 3)
        {
            using var client = new HttpClient();

            try
            {
                var result = await client.GetAsync($"https://api.weatherapi.com/v1/forecast.json?key=39f8ecaf506c4f76b3f55139222906&q={query}&days={days}&aqi=yes&alerts=yes");
                if (result.IsSuccessStatusCode)
                {
                    var response = await result.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ForcastLogicModel>(response);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
    }
}
