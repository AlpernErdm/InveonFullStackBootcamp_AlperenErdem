using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Episode2
{
    public class AsyncAwaitExample
    {
        private readonly HttpClient _httpClient;

        public AsyncAwaitExample()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> FetchDataFromUrlAsync(string url)
        {
            try
            {
                Console.WriteLine("Fetching data from URL...");
                HttpResponseMessage response = await _httpClient.GetAsync(url);

                // Hatalı HTTP durum kodlarını kontrol et
                response.EnsureSuccessStatusCode();

                string responseData = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Data fetched successfully.");
                return responseData;
            }
            catch (HttpRequestException httpRequestException)
            {
                Console.WriteLine($"Error fetching data: {httpRequestException.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                return null;
            }
        }
    }
}
