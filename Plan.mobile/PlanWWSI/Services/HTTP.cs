using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PlanWWSI.Services
{
    public class HTTP
    {
        private HttpClient _httpClient;
        private static string URL = "http://10.0.2.2:60211";

        public HTTP()
        {
            _httpClient = new HttpClient();
        }

        public async Task<T> GetAsync<T>(string path)
        {
            var res = await _httpClient.GetAsync(new Uri($"{URL}{path}"));
            if (res.IsSuccessStatusCode)
            {
                string content = await res.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<T>(content);
                return result;
            }
            return default;
        }
    }
}
