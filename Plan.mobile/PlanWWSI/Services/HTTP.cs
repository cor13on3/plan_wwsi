using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PlanWWSI.Services
{
    public class HTTP
    {
        private HttpClient _httpClient;
        // private static string URL = "http://10.0.2.2:60211";
        private static string URL = "http://192.168.0.129:60211";
        

        public HTTP()
        {
            _httpClient = new HttpClient();
        }

        public async Task<T> GetAsync<T>(string path)
        {
            try
            {
                //var x = await _httpClient.GetAsync(new Uri($"https://jsonplaceholder.typicode.com/todos/1"));
                var res = await _httpClient.GetAsync(new Uri($"{URL}{path}"));
                if (res.IsSuccessStatusCode)
                {
                    string content = await res.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<T>(content);
                    return result;
                }
                else
                {
                    var x = res.StatusCode;
                }
                return default;
            }
            catch(Exception e)
            {
                return default;
            }
        }
    }
}
