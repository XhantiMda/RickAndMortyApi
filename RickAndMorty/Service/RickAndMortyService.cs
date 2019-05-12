using System;
using System.Net.Http;
using System.Threading.Tasks;
using RickAndMorty.Extensions;
using RickAndMorty.Models;

namespace RickAndMorty.Service
{
    public class RickAndMortyService : IRickAndMortyService
    {
        private HttpClient _client;
        private const string _baseUrl = "https://rickandmortyapi.com/api/";

        public RickAndMortyService(HttpClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri(_baseUrl);
        }

        public async Task<Character> GetCharacterByIdAsync(int id)
        {
            var result = await _client.GetAsync($"character/{id}");

            if (!result.IsSuccessStatusCode)
                return default(Character);

            using (var stream = await result.Content.ReadAsStreamAsync())
            {
                return stream.Deserialize<Character>();
            }
        }

        public async Task<Character> GetRandomCharacterAsync()
        {
            var random = new Random();

            return await GetCharacterByIdAsync(random.Next(20));
        }
    }
}
