using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CraftExercise.Models;



namespace CraftExercise.Services
{
    public class FreshdeskService
    {
        private readonly HttpClient _httpClient;

        public FreshdeskService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> CreateOrUpdateContact(FreshdeskContact contact, string subdomain, string apiKey)
        {
            var json = JsonSerializer.Serialize(contact);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Basic {Convert.ToBase64String(Encoding.ASCII.GetBytes($"{apiKey}:X"))}");

            var response = await _httpClient.PostAsync($"https://{subdomain}.freshdesk.com/api/v2/contacts", data);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Freshdesk API request failed with status code: {response.StatusCode}");
            }

            return response.IsSuccessStatusCode;
        }
    }
}
