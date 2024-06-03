using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using CraftExercise.Models;

namespace CraftExercise.Services
{
    public class GitHubService
    {
        private readonly HttpClient _httpClient;

        public GitHubService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<GitHubUser> GetGitHubUser(string username)
        {
            var response = await _httpClient.GetAsync($"https://api.github.com/users/{username}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"GitHub API request failed with status code: {response.StatusCode}");
            }

            using var contentStream = await response.Content.ReadAsStreamAsync();
            var user = await JsonSerializer.DeserializeAsync<GitHubUser>(contentStream);
            return user;
        }
    }
}