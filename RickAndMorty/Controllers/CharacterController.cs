using Microsoft.AspNetCore.Mvc;
using RickAndMorty.Extensions;
using RickAndMorty.Models;
using RickAndMorty.Service;
using System.Net.Http;
using System.Threading.Tasks;

namespace RickAndMorty.Controllers
{
    public class CharacterController : Controller
    {
        private IRickAndMortyService _rickAndMortyService;

        public CharacterController(IRickAndMortyService rickAndMortyService)
        {
            _rickAndMortyService = rickAndMortyService;
        }

        [HttpGet("api/character/{id}")]
        public async Task<IActionResult> GetCharacter(int id)
        {
            var response = await _rickAndMortyService.GetCharacterByIdAsync(id);

            if (response == null)
                return NotFound();

            return Ok(response);
        }

        [HttpGet("api/character/random")]
        public async Task<IActionResult> GetRandomCharacter()
        {
            var response = await _rickAndMortyService.GetRandomCharacterAsync();

            if (response == null)
                return NotFound();

            return Ok(response);
        }

        [HttpGet("api/character/morty")]
        public async Task<IActionResult> GetMorty([FromServices] IHttpClientFactory clientFactory)
        {
            var client = clientFactory.CreateClient("RickAndMortyClient");

            var result = await client.GetAsync($"character/2");

            if (!result.IsSuccessStatusCode)
                return NotFound();

            using (var stream = await result.Content.ReadAsStreamAsync())
            {
                return Ok(stream.Deserialize<Character>());
            }
        }
    }
}
