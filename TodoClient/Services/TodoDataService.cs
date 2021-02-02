using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TodoClient.Models;

namespace TodoClient.Services
{
    public class TodoDataService : ITodoDataService
    {
        private readonly HttpClient _httpClient;

        public TodoDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ItemData> AddItem(ItemData item)
        {
            try
            {
                var itemJson = new StringContent(JsonSerializer.Serialize(item), Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("api/todo", itemJson);

                if(response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStreamAsync();

                    return await JsonSerializer.DeserializeAsync<ItemData>(responseBody, new JsonSerializerOptions {PropertyNameCaseInsensitive = true});
                }

                return null;    
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            
        }

        public async Task DeleteItem(int id)
        {
            try
            {
                await _httpClient.DeleteAsync($"api/todo/{id}");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }  
        }

        public async Task<IEnumerable<ItemData>> GetAllItems()
        {
           var apiResponse = await _httpClient.GetStreamAsync($"api/todo");
           return await JsonSerializer.DeserializeAsync<IEnumerable<ItemData>>
                    (apiResponse, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<ItemData> GetItemDetails(int id)
        {
            var apiResponse = await _httpClient.GetStreamAsync($"api/todo/{id}");
            return await JsonSerializer.DeserializeAsync<ItemData>
                    (apiResponse, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task UpdateItem(ItemData item)
        {
            try
            {
                var itemJson = new StringContent(JsonSerializer.Serialize(item), Encoding.UTF8, "application/json");

                var url = $"api/todo/{item.Id}";

                var response = await _httpClient.PutAsync(url, itemJson);

                if(response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Success");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}